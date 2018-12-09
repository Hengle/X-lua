local format = string.format
local error = error
local pairs = pairs
local assert = assert
local clone = clone
local Struct = require('ECS.ComponentStruct')

---@class Component : CompFunc
local Component = {}

local components = {}

local function Create(name)
    local comp = Struct[name]
    if comp then
        return clone(comp.fileds)
    else
        error(format('Component: %s not registered in the system.', name))
        return nil
    end
end

local function Register(name, fileds)
    local comp = components[name]
    if comp then
        error(format('Component: %s already exists', name))
    --else
        --comp = { name = name, fileds = fileds, }
        --components[name] = comp
    end
end

local function Extend(custom)
    for k, v in pairs(custom) do
        assert(Struct[k] == nil, "Component: %s has been defined", k)
        Struct[k] = v
    end
end

local function Init()
    for k, v in pairs(Struct) do
        Register(k, v)
        Component[k] = k
    end
end

---@class CompFunc
local func = {
    Init = Init,
    Create = Create,
    Extend = Extend,
    --Register = Register,
}
func.__index = func
setmetatable(Component, func)
return Component