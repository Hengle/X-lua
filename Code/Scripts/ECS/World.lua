local require = require
local Component = require('ECS.Component')
local Entity = require('ECS.Enity')
local System = require('ECS.System')

local format = string.format
local insert = table.insert
local clear = table.clear
local error = error
local assert = assert
local type = type
local select = select
local EventMgr = GameEvent.ECSEvent
local List = List
local Class = Class
local printyellow = printyellow
local LogError = LogError

local reusedEntities = List:new()
local compPool = {}--k:组件类型;v:组件池

---@class World
local World = Class:new("World")

local OnComponentChanged
local OnComponentUpdated
local OnCreateEntity
local OnDestroyEntity

local removeChangeEvt
local removeUpdateEvt
local removeDestroyEvt

----可能需要细分世界
function World:Init(name, entityID)
    self.name = name or "World"
    self.entityID = entityID or 1
    self.enities = {}   ---array mix hash
    self.systems = {}  -- 数组,便于遍历查询
    self.systemList = List:new()      -- 链表,便于list中节点修改
    self.groups = {}    --实体分组,系统各引用一个组[array mix hash]
end

function World:Update(dt)
    ----上一帧执行的添加移除更新操作,在下一帧开始时执行,顺序如下
    ----1.添加
    ----2.移除
    ----3.更新
    ----4.处理前[可选]
    ----5.处理中
    ----6.处理后[可选]

    for i = 1, #self.systems do
        ---@type System
        local system = self.systems[i]
        if system and system.enable and system.OnUpdate then
            system:OnUpdate(dt)
        end
    end
end

function World:Reset()
    self:DestroyAllEntities()
    self.entityID = 0
end

function World:CreateEnity()
    local entity = nil
    if reusedEntities.length > 0 then
        entity = reusedEntities:Shift()
        entity:Reset(self.entityID)
    else
        entity = Entity:new(self.entityID)
    end

    return entity
end

function World:AddEntity(entity)
    insert(self.enities, entity)
    self.enities[entity] = #self.enities

    entity.OnComponentAdded = OnComponentChanged
    entity.OnComponentRemoved = OnComponentChanged
    entity.OnComponentUpdated = OnComponentUpdated
    entity.OnDestroyEntity = OnDestroyEntity

    OnCreateEntity(entity)
    self.entityID = self.entityID + 1
end

function World:RemoveEnity(entity)
    if not entity then
        LogError('World:%s remove entity fail.', self.name)
        return
    end

    local entities = self.enities
    local index = entities[entity]
    if index then
        local lastEntity = entities[#entities]
        entities[index] = lastEntity
        entities[#entities] = nil
        entities[lastEntity] = index
        entities[entity] = nil
    end
    OnDestroyEntity(entity)
    reusedEntities:Push(entity)
end

function World:DestroyAllEntities()
    for i = 1, #self.enities do
        local entity = self.enities[i]
        self.enities[entity] = nil
        OnDestroyEntity(entity)
        reusedEntities:Push(entity)
    end
end

---------------------------------------------
---------------System控制函数
---------------------------------------------
local SetSystemEnable = function(enable, ...)
    local length = select('#', ...)
    if length > 0 then
        for i = 1, length do
            local name = select(i, ...)
            self.systems[name].enable = enable
        end
    else
        for i = 1, #self.systems do
            self.systems[i].enable = enable
        end
        printyellow('World:Set all systems', enable)
    end
end
function World:StartSystems(...)
    SetSystemEnable(true, ...)
end
function World:StopSystems(...)
    SetSystemEnable(false, ...)
end
-----系统执行顺序以添加顺序为准,顺序可动态调整
---@param ... string
function World:RegisterSystem(...)
    local length = select('#', ...)
    if length == 0 then
        return
    end

    for i = 1, length do
        local name = select(i, ...)
        local sys = self.systems[name]
        if not sys then
            ---@type System
            local system = require('ECS.Systems.' .. name)
            system = System.Extend(system, self, name)
            insert(self.systems, system)
            self.systems[name] = system
            self.systemList:Push(system)
            local group = {}
            for i = 1, #self.enities do
                local entity = self.enities[i]
                if system:Filter(entity) then
                    insert(group, entity)
                end
            end
            self.groups[system.filterName] = group
            system.group = group
        else
            printyellow('World:Repeat registration system ' .. name)
        end
    end
end
---@param ... string
function World:RemoveSystem(...)
    local length = select('#', ...)
    if length == 0 then
        return
    end

    local names = { ... }
    local count = 0
    local node = self.systemList:Head()
    while node ~= nil and length ~= count do
        local del = nil
        local system = node.value
        for i = 1, length do
            if system.name == names[i] then
                count = count + 1
                del = node
                break
            end
        end
        node = self.systemList:Next(node)
        if del then
            self.systemList:Remove(del)
            clear(self.groups[system.filterName])
        end
    end
end
----在大量添加或者移除系统后,再进行系统调整.此函数不适合重复调用
function World:AdjustSystem()
    self.systems = self.systemList:ToTable()
    for i = 1, #self.systems do
        local sys = self.systems[i]
        self.systems[sys.name] = sys
    end
end
---实体组件叠加修改,如何处理?
---@param name string 系统名称,ECS.Systems相对路径名称
function World:NotifySystem(name, component)
    local system = self.systems[name]
    if system then
        --system:Notify(component)---缓存通知,下一帧统一执行
    else
        printyellow('World:Notify System ' .. name .. ' does not exist.')
    end
end

------------------------------------
---------------事件函数
------------------------------------
OnComponentChanged = function(entity, component)

end
OnComponentUpdated = function(entity, component)

end
OnCreateEntity = function(entity)

end
OnDestroyEntity = function(entity)

end

return World