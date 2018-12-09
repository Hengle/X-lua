local Component = require('ECS.Component')
local Entity = require('ECS.Enity')
local System = require('ECS.System')

local format = string.format
local error = error
local insert = table.insert
local EventMgr = GameEvent.ECSEvent
local Queue = Queue
local Class = Class
local LogError = LogError

---@type Queue
local entityQueue = Queue:new()
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
    self.enities = {}
    self.grouphash = {}--实体分组,系统各引用一个组
    self.systems = {}
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
        local system= self.systems[i]
        if system and system.enable and system.Update then
            system:Excute(dt)
        end
    end
end

function World:Reset()
    self:DestroyAllEntities()
    self.entityID = 0
end

function World:CreateEnity()
    local entity = nil
    if entityQueue:Count() > 0 then
        entity = entityQueue:Dequeue()
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
end

function World:DestroyAllEntities()
    for i = 1, #self.enities do
        local entity = self.enities[i]
        self.enities[entity] = nil
        OnDestroyEntity(entity)
    end
end

function World:StartSystems()
    for i = 1, #self.systems do
        self.systems[i].enable = true
    end
end
function World:StopSystems()
    for i = 1, #self.systems do
        self.systems[i].enable = false
    end
end

function World:GetGroup(match)
    local group = self.grouphash[match]
    if not group then
        -----添加Group
    end
    return group
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