local setmetatable = setmetatable

---@class Class
local Class = {}

function Class:new(base)
	local class = {}
	class.__index = class--index 是给obj访问元表时使用
	
	local mt = {}
	setmetatable(class, mt)
	if base then
		mt.__index = base
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


