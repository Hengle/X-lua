local List = List
local Class = Class

---@class Stack:List
local Stack = Class:new(List)

function Stack:Push(item)
    self:push(item)
end
function Stack:Peek()
    return self:head()
end
function Stack:Pop()
    return self:pop()
end
function Stack:Clear()
    self:clear()
end
function Stack:Count()
    return self.length
end
function Stack:Contains(item)
    local node = self:find(item)
    return node ~= nil
end
function Stack:Clone(item)
    self:clone()
end

function Stack:MoveNext()
    return self:movenext()
end
function Stack:Current()
    return self:current()
end
function Stack:InitEnumerator()
    self:initenumerator()
end
function Stack:ToTable()
    return self:totable()
end

return Stack