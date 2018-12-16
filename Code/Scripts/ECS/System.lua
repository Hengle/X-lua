local printcolor = printcolor
local printyellow = printyellow
local type = type
local assert = assert
local format = string.format
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
--]]

function System:Init(world, name)
    self.world = world
    self.name = name
    self.group = nil      --> Update
    assert(self.OnUpdate or self.OnNotify and not (self.OnUpdate and self.OnNotify),
            'System:' .. name .. 'No Define OnUpdate or OnNotify')
end

function System:Notify()
    for i = 1, #self.group do
        local entity = self.group[i]
        self:OnNotify(entity)
    end
    clear(self.group)
end

--覆盖方法
function System:GetFilter()
    error('System:The system will traverses all entities.[' .. self.name .. ']')
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