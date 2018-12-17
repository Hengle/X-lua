local printcolor = printcolor
local printyellow = printyellow
local type = type
local assert = assert
local format = string.format
local sub = string.sub
local byte = string.byte
local concat = table.concat
local sort = table.sort
local clear = table.clear
local error = error
local Class = Class

---@class System
local System = Class:new('System')

--[[
    是否关注一下事件,初始化时手动绑定
    --ComponentAdd
    --ComponentRemove
    --ComponentModify
    -------------------------------
    filter 定义格式固定为table.
    {Comp.Position, Comp.Rotation, Comp.Scale}
--]]

local function Hash(str)
    local seed = 131
    local hash = 0
    for i = 1, #str do
        hash = hash * seed + byte(sub(str, i, i))
    end
    return (hash & 0x7FFFFFFF)
end

function System:Init(world, name)
    self.world = world
    self.name = name
    self.group = nil      --> Update
    self.collector = nil    --> Notify
    local filter = self:GetFilter()
    if filter then
        self.filterName = Hash(concat(sort(filter), '.'))
    else
        error('System:Filter table is nil.[' .. self.name .. ']')
    end
    assert(self.OnUpdate or self.OnNotify and not (self.OnUpdate and self.OnNotify),
            'System:' .. name .. 'No Define OnUpdate or OnNotify')
end

function System:Notify()
    if self.collector then
        for i = 1, #self.collector do
            local entity = self.collector[i]
            self:OnNotify(entity)
        end
        clear(self.collector)
    end
end

--覆盖方法
function System:GetFilter()
    error(format('System:The system [%s] no define GetFilter().', self.name))
end

function System:Filter(entity)
    if not self.__handle then
        self.__handle = self:GetFilter()
        if type(self.__handle) ~= 'function' then
            error('System:self.GetFilter does not return a function.[' .. self.name .. ']')
            return false
        else
            return self:__handle(entity)
        end
    else
        return self:__handle(entity)
    end
end

function System:Destroy()
    self.collector = nil
    self.group = nil
end

return System