local printyellow = printyellow
local assert = assert
local format = string.format
local concat = table.concat
local sort = table.sort
local byte = string.byte
local sub = string.sub
local error = error

---@class System
local System = {}

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
    if not system.GetFilter then
        error(format('System:%s undefined \'GetFilter\' function', system.__class))
        return nil
    end
    local filter = system:GetFilter()
    return system.filterName or Hash(concat(sort(filter), '.'))
end

function System:Extend(world, name)
    self.world = world
    self.name = name
    self.enable = false
    self.group = nil
    self.singleton = nil  -- 单例类
    ---使用filter表生成filter唯一标识
    self.filterName = GenUnique(self)
    self.GetFilter = function(system)
        printyellow('System:The system will traverses all entities')
        return {}
    end
    assert(self.OnUpdate or self.OnNotify, 'System:' .. name .. 'No Define OnUpdate or OnNotify')
end

return System