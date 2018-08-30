--------------------------------------------------------------------------------
--      Copyright (c) 2015 - 2016 , 蒙占志(topameng) topameng@gmail.com
--      All rights reserved.
--      Use, modification and distribution are subject to the "MIT License"
--------------------------------------------------------------------------------

local pcall = pcall
local assert = assert
local insert = table.insert
local error = error
local print = print
local setmetatable = setmetatable
local traceback = debug.traceback
local ilist = ilist

local _pcall = {
    __call = function(self, ...)
        local status, err
        if nil == self.obj then
            status, err = pcall(self.func, ...)
        else
            status, err = pcall(self.func, self.obj, ...)
        end
        if not status then
            error(err .. '\n' .. traceback(), 2)
        end
    end,

    __eq   = function(lhs, rhs)
        return lhs.func == rhs.func and lhs.obj == rhs.obj
    end
}
local function functor(func, obj)
    return setmetatable({ func = func, obj = obj }, _pcall)
end
---@class _Event
local _event = {}
_event.__index = _event

function _event:CreateListener(func, obj)
    func = functor(func, obj)
    return { value = func, _prev = 0, _next = 0, removed = true }
end

function _event:Add(func, obj)
    assert(func)

    if self.keepSafe then
        self.list:push(xfunctor(func, obj))
    else
        self.list:push(functor(func, obj))
    end
end

function _event:Remove(func, obj)
    assert(func)

    for i, v in ilist(self.list) do
        if v.func == func and v.obj == obj then
            if self.lock then
                self.rmList:push({ func = func, obj = obj })
            else
                self.list:remove(i)
            end
        end
    end
end

function _event:AddListener(handle)
    assert(handle)

    if self.lock then
        insert(self.opList, function()
            self.list:pushnode(handle)
        end)
    else
        self.list:pushnode(handle)
    end
end

function _event:RemoveListener(handle)
    assert(handle)

    if self.lock then
        insert(self.opList, function()
            self.list:remove(handle)
        end)
    else
        self.list:remove(handle)
    end
end

function _event:Count()
    return self.list.length
end

function _event:Clear()
    self.list:clear()
    self.opList = {}
    self.lock = false
    self.keepSafe = false
    self.current = nil
end

function _event:Dump()
    local count = 0

    for _, v in ilist(self.list) do
        if v.obj then
            print("update function:", v.func, "object name:", v.obj.name)
        else
            print("update function: ", v.func)
        end

        count = count + 1
    end

    print("all function is:", count)
end

_event.__call = function(self, ...)
    local _list = self.list
    self.lock = true
    local ilist = ilist

    for i, f in ilist(_list) do
        self.current = i
        local flag, msg = f(...)

        if not flag then
            _list:remove(i)
            self.lock = false
            error(msg)
        end
    end

    local opList = self.opList
    self.lock = false

    for i, op in ipairs(opList) do
        op()
        opList[i] = nil
    end
end

function event(name)
    return setmetatable({ name = name, lock = false, opList = {}, list = list:new() }, _event)
end
