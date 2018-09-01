local type = type
local ipairs = ipairs
local tostring = tostring
local concat = table.concat
local format = string.format
local traceback = debug.traceback
local dump = table.dump
local Debug = Debug

--unity对象判空--
function IsNull(unity_object)
	if unity_object == nil then
		return true
	end
	
	if type(unity_object) == "userdata" and unity_object.IsNull ~= nil then
		return unity_object:IsNull()
	end
	
	return false
end

--输出日志--
function Log(str)
    if not Local.LogManager then
        return
    end
    if Local.LogTraceback then
        Debug.Log(str .. "\r\n" .. traceback());
    else
        Debug.Log(str);
    end
end
--错误日志--
function LogError(str)
    if not Local.LogManager then
        return
    end
    if Local.LogTraceback then
        Debug.LogError(str .. "\r\n" .. traceback());
    else
        Debug.LogError(str)
    end
end
--警告日志--
function LogWarning(str)
    if not Local.LogManager then
        return
    end
    if Local.LogTraceback then
        Debug.LogWarning(str .. "\r\n" .. traceback());
    else
        Debug.LogWarning(str)
    end
end
---@param a 判定条件
---@param b true返回b
---@param c false返回c
function ConditionOp(a, b, c)
    if a then
        return b
    else
        return c
    end
end

print = function(...)
    if not Local.LogManager then
        return
    end
    local args = { ... }
    for k, v in ipairs(args) do
        args[k] = tostring(v)
    end
    if Local.LogTraceback then
        Debug.Log(concat(args, '\t') .. '\n' .. traceback())
    else
        Debug.Log(concat(args, '\t'))
    end
end
local color = "yellow"
local dump_level = 3
--输出黄色日志-无格式
function printcolor(...)
    if not Local.LogManager then
        return
    end
    local args = { ... }
    for k, v in ipairs(args) do
        args[k] = tostring(v)
    end

    local log = format("<color=%s>%s</color>\t\n", color, concat(args, '\t'))
    if Local.LogTraceback then
        Debug.Log(log .. traceback());
    else
        Debug.Log(log);
    end
end
function printt(t, des)
    if not Local.LogManager then
        return
    end
    if type(t) == "table" then
        print(format("<color=orange>%s</color>\n%s", des, dump(t, dump_level)))
    else
        print(des .. '\n' .. t)
    end
end