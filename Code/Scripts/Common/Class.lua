local setmetatable = setmetatable

local Class = {}

function Class:new(super)
	local class = {}
	class.__index = class--index 是给obj访问元表时使用
	
	local mt = {}
	setmetatable(class, mt)
	if super then 
		mt.__index = super
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


