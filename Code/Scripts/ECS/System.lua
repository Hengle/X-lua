local type = type
local assert = assert
local error = error
local format = string.format
local concat = table.concat
local sort = table.sort
local byte = string.byte
local sub = string.sub
local Class = Class

local function Hash(str)
    local seed = 131
    local hash = 0

    for i = 1, #str do
        hash = hash * seed + byte(sub(str, i, i))
    end
    return (hash & 0x7FFFFFFF)
end
-- 为filter 筛选条件表唯一标识
local function GenUnique(system)
    local filter = system:GetFilter()
    if not filter then
        error(format('System:%s undefined \'GetFilter\' function', system.__class))
        return nil
    end
    return system.unique or Hash(concat(sort(filter), '.'))
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
    ---使用filter表生成filter唯一标识,及GroupID
    self.groupId = GenUnique(self)
end

function System:FilterEntity(entity)
    return Filter(entity, self:GetFilter())
end

return System