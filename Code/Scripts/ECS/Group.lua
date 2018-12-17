local type = type
local assert = assert
local error = error
local clear = table.clear
local insert = table.insert
local Class = Class

---@class Group
local Group = Class:new('Group')

function Group:ctor(filter)
    self.filter = filter
    self.entities = {}
    self.systems = {}
end

function Group:Filter(entity)
    local filter = self.filter
    assert(type(filter) == 'table', "Filter can only be table type.")
    for i = 1, #filter do
        local comp = filter[i]
        if entity[comp] == nil then
            return false
        end
    end
    insert(self.entities, entity)
    return true
end

---检查是否还有所关注的系统,如果无则清空数据
function Group:CheckEmpty()
    local r = #self.systems == 0
    if r and #self.entities > 0 then
        clear(self.entities)
    end
    return r
end
function Group:AddSystem(name)
    insert(self.systems, name)
end
function Group:RemoveSystem(name)
    local length = #self.systems
    for i = 1, length do
        local sys = self.systems[i]
        if sys.name == name then
            local last = self.systems[length]
            self.systems[i] = last
            break
        end
    end
end

return Group