--------------------------------------------------------------------------------
--      Copyright (c) 2015 - 2016 , 蒙占志(topameng) topameng@gmail.com
--      All rights reserved.
--      Use, modification and distribution are subject to the "MIT License"
--------------------------------------------------------------------------------
local setmetatable = setmetatable
---@class List
local List = Class:new("List")

function List:ctor()
    self.length = 0
    self._prev = self
    self._next = self
end

function List:clear()
    self.ctor()
end

function List:push(value)
    --assert(value)
    local node = { value = value, _prev = 0, _next = 0, removed = false }

    self._prev._next = node
    node._next = self
    node._prev = self._prev
    self._prev = node

    self.length = self.length + 1
    return node
end

function List:pushnode(node)
    if not node.removed then
        return
    end

    self._prev._next = node
    node._next = self
    node._prev = self._prev
    self._prev = node
    node.removed = false
    self.length = self.length + 1
end

function List:pop()
    local _prev = self._prev
    self:remove(_prev)
    return _prev.value
end

function List:unshift(v)
    local node = { value = v, _prev = 0, _next = 0, removed = false }

    self._next._prev = node
    node._prev = self
    node._next = self._next
    self._next = node

    self.length = self.length + 1
    return node
end

function List:shift()
    local _next = self._next
    self:remove(_next)
    return _next.value
end

function List:remove(iter)
    if iter.removed then
        return
    end

    local _prev = iter._prev
    local _next = iter._next
    _next._prev = _prev
    _prev._next = _next

    self.length = math.max(0, self.length - 1)
    iter.removed = true
end

function List:find(v, iter)
    iter = iter or self

    repeat
        if v == iter.value then
            return iter
        else
            iter = iter._next
        end
    until iter == self

    return nil
end

function List:findlast(v, iter)
    iter = iter or self

    repeat
        if v == iter.value then
            return iter
        end

        iter = iter._prev
    until iter == self

    return nil
end

function List:next(iter)
    local _next = iter._next
    if _next ~= self then
        return _next, _next.value
    end

    return nil
end

function List:prev(iter)
    local _prev = iter._prev
    if _prev ~= self then
        return _prev, _prev.value
    end

    return nil
end

function List:erase(v)
    local iter = self:find(v)

    if iter then
        self:remove(iter)
    end
end

function List:insert(v, iter)
    if not iter then
        return self:push(v)
    end

    local node = { value = v, _next = 0, _prev = 0, removed = false }

    if iter._next then
        iter._next._prev = node
        node._next = iter._next
    else
        self.last = node
    end

    node._prev = iter
    iter._next = node
    self.length = self.length + 1
    return node
end

function List:head()
    return self._next.value
end

function List:tail()
    return self._prev.value
end

function List:clone()
    local t = List:new()

    for i, v in List.next, self, self do
        t:push(v)
    end

    return t
end

local node = nil
local current = nil
function List:movenext()
    node, current = self:next(node)
    return node ~= nil
end
function List:current()
    return current
end
function List:initenumerator()
    current = nil
    node = self
end
function List:totable()
    local t = {}
    for i, v in ilist(self) do
        table.insert(t, v)
    end
    return t
end

ilist = function(_list)
    return List.next, _list, _list
end
rilist = function(_list)
    return List.prev, _list, _list
end

setmetatable(List, { __call = List.new })
return List