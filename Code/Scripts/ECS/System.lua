local type = type
local assert = assert
local error = error
local format = string.format
local Class = Class

-- 为filter 筛选条件表唯一标识
local function GenUnique(system)
    if not system.GetFilter then
        error(format('System:%s undefined \'GetFilter\' function', system.__class))
        return nil
    end
    local filter = system:GetFilter()
    assert(type(filter) == 'table', "Filter can only be table type.")
    ----生产唯一标识
    --TODO
    return ""
end
local function Filter(entity, filter)
    assert(type(filter) == 'table', "Filter can only be table type.")
    for i = 1, #filter do
        local comp = filter[i]
        if entity[comp] == nil then
            return false
        end
    end
    return true
end

---@class System
local System = Class:new("System")

function System:ctor(world)
    self.world = world
    self.enable = false
    ---filter条件在GetFilter中定义
    self.filterName = GenUnique(self)
end

function System:FilterEntity(entity)
    return Filter(entity, self:GetFilter())
end

return System