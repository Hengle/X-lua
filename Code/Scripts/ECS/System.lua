local printcolor = printcolor
local printyellow = printyellow
local assert = assert
local format = string.format
local concat = table.concat
local sort = table.sort
local error = error
local Filter = require('Filter')

---@class System
local System = {}

function System:ctor(world, name)
    self.world = world
    self.name = name
    self.enable = false
    self.group = nil
    self.singleton = nil  -- 单例类
    ---使用filter表生成filter唯一标识
    if not self.GetFilter then
        error(format('System:%s undefined \'GetFilter\' function', self.__class))
    else
        local comps, func = self:GetFilter()
        self.Filter = func
        self.filterName = Filter.GenUnique(comps)
    end
    assert(self.OnUpdate or self.OnNotify, 'System:' .. name .. 'No Define OnUpdate or OnNotify')
end

function System:GetFilter(system)
    printcolor('red', 'System:The system will traverses all entities')
    return {}
end

return System