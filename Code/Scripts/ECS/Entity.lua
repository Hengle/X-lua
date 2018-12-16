local assert = assert
local error = error
local pairs = pairs
local type = type
local format = string.format
local Class = Class
local ComponentMgr = require('ComponentManager')

---@class Entity
local Entity = Class:new('Entity')

---- 实体初始化也是数据驱动?
function Entity:ctor(world, id)
    self.id = id
    self.world = world
    self.enable = true
    self.linkedSys = {}
end

function Entity:Reset(world, id)
    self:ctor(world, id)
end

function Entity:Destroy()
    if not self.enable then
        error(format('Entity:%d has been destroyed.', self.id))
    end

    self:OnDestroyEntity()
    self.enable = false
    self:RemoveAllComponents()
end

function Entity:RemoveAllComponents()
    for k, v in pairs(self) do
        if type(k) == 'number' then
            self:RemoveComponent(k)
        end
    end
end

function Entity:HasComponent(index)
    return self[index] ~= nil
end

function Entity:AddComponent(index)
    if not self.enable then
        error(format('Entity:%d has been destroyed.', self.id))
    end
    assert(self[index] == nil, "Entity: Trying to add Component '" .. name .. "', but it's already existing.")
    local comp = ComponentMgr.Create(index)
    self[index] = comp
    self.world:OnComponentChanged(self)
end
function Entity:RemoveComponent(index)
    if not self.enable then
        error(format('Entity:%d has been destroyed.', self.id))
    end
    assert(self[index] == nil, "Entity: Trying to remove non-existent component " .. name .. " from Entity. Please fix this")
    ComponentMgr.Release(index, self[index])
    self[index] = nil
    self.world:OnComponentChanged(self)
end
function Entity:Modify(index)
    assert(self[index] == nil, "Entity: Trying to remove non-existent component " .. name .. " from Entity. Please fix this")
    self.world:OnComponentModify(self)
end

return Entity