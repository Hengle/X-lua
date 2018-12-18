local setmetatable = setmetatable
local assert = assert
local type = type

---@class Class
local Class = {}

function Class:new(className, base)
    assert(type(className) == "string", "You must provide a name(string) for your class")
    local class = {}
    class.__index = class
    class.__class = className

    if base then
        setmetatable(class, { __index = base })
        class.base = base
    else
        setmetatable(class, {})
    end

    function class:new(...)
        local obj = {}
        setmetatable(obj, self)
        if obj.ctor then
            obj:ctor(...)
        end
        return obj
    end
    return class
end

return Class

--[[
类型单例实现例子
local Player = class:new()
local _instance = nil

function Player:ctor(name)
    self.name = name
    Player.new = function()
        print("单例类型不允许再实例化")
    end
    _instance = self
end
function Player:Instance()
    local o = _instance
    if o then return o end
    o = Player:new("王")
    _G.Player = o
    return o
end
--]]
