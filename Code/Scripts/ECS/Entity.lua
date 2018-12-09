local assert = assert
local error = error
local pairs = pairs
local format = string.format
local Class = Class
local Component = require('ECS.Component')
local EventMgr = GameEvent.ECSEvent

---@class Entity
local Entity = Class:new('Entity')

---- 实体初始化也是数据驱动?
function Entity:ctor(id)
    self.id = id
    self.enable = true
    self.isModify = false
end

function Entity:Reset(id)
    self.id = id
    self.enable = true
    self.isModify = false
end

function Entity:Destroy()
    if not self.enable then
        error(format('Entity:%d has been destroyed.', self.id))
    end

    self:OnDestroyEntity()
    self.enable = false
    self:RemoveAllComponents()

    self.OnComponentAdded = nil
    self.OnComponentRemoved = nil
    self.OnComponentUpdated = nil
    self.OnDestroyEntity = nil
end

function Entity:RemoveAllComponents()
    for k, v in pairs(self) do
        if k ~= 'id' and k ~= 'enable' and k ~= 'isModify' then
            self[k] = nil
        end
    end
end

function Entity:HasComponent(name)
    return self[name] ~= nil
end

function Entity:AddComponent(name)
    assert(self[name] == nil, "Entity: Trying to add Component '" .. name .. "', but it's already existing.")
    local comp = Component.Create(name)
    self[name] = comp
    self:OnComponentChanged(comp)
end
function Entity:RemoveComponent(name)
    assert(self[name] == nil, "Entity: Trying to remove non-existent component " .. name .. " from Entity. Please fix this")
    local comp = self[name]
    self[name] = nil
    self:OnComponentChanged(comp)
end

return Entity