--[[
-- table扩展工具类，对table不支持的功能执行扩展
-- 注意：
-- 1、所有参数带hashtable的函数，将把table当做哈希表对待
-- 2、所有参数带array的函数，将把table当做可空值数组对待
-- 3、所有参数带tb的函数，对表通用，不管是哈希表还是数组
--]]
local type = type
local pairs = pairs
local rep = string.rep
local tostring = tostring
local getmetatable = getmetatable
local insert = table.insert

-- 计算哈希表长度
local function count(hashtable)
    local count = 0
    for _, _ in pairs(hashtable) do
        count = count + 1
    end
    return count
end

-- 计算数据长度
local function length(array)
    if array.n ~= nil then
        return array.n
    end

    local count = 0
    for i, _ in pairs(array) do
        if count < i then
            count = i
        end
    end
    return count
end

-- 设置数组长度
local function setlen(array, n)
    array.n = n
end

-- 获取哈希表所有键
local function keys(hashtable)
    local keys = {}
    for k, v in pairs(hashtable) do
        keys[#keys + 1] = k
    end
    return keys
end

-- 获取哈希表所有值
local function values(hashtable)
    local values = {}
    for k, v in pairs(hashtable) do
        values[#values + 1] = v
    end
    return values
end

-- 合并哈希表：将src_hashtable表合并到dest_hashtable表，相同键值执行覆盖
local function merge(src_hashtable, dest_hashtable)
    for k, v in pairs(src_hashtable) do
        dest_hashtable[k] = v
    end
end

-- 合并数组：将src_array数组从begin位置开始插入到dest_array数组
-- 注意：begin <= 0被认为没有指定起始位置，则将两个数组执行拼接
local function insertarray(src_array, dest_array, begin)
    assert(begin == nil or type(begin) == "number")
    if begin == nil or begin <= 0 then
        begin = #dest_array + 1
    end

    local src_len = #src_array
    for i = 0, src_len - 1 do
        dest_array[i + begin] = src_array[i + 1]
    end
end

-- 从数组中查找指定值，返回其索引，没找到返回false
local function indexof(array, value, begin)
    for i = begin or 1, #array do
        if array[i] == value then
            return i
        end
    end
    return false
end

-- 从哈希表查找指定值，返回其键，没找到返回nil
-- 注意：
-- 1、containskey用hashtable[key] ~= nil快速判断
-- 2、containsvalue由本函数返回结果是否为nil判断
local function keyof(hashtable, value)
    for k, v in pairs(hashtable) do
        if v == value then
            return k
        end
    end
    return nil
end

-- 从数组中删除指定值，返回删除的值的个数
local function removebyvalue(array, value, removeall)
    local remove_count = 0
    for i = #array, 1, -1 do
        if array[i] == value then
            table.remove(array, i)
            remove_count = remove_count + 1
            if not removeall then
                break
            end
        end
    end
    return remove_count
end

-- 筛选符合条件的项：不对原表执行操作
local function filter(tb, condition)
    local filter = {}
    for k, v in pairs(tb) do
        if not condition(k, v) then
            filter[k] = v
        end
    end
    return filter
end

-- dump表
local function dump(t, dump_metatable, max_level)
    local lookup_table = {}
    local level = 0
    local dump_metatable = dump_metatable
    local max_level = max_level or 1

    local function _dump(tb, level)
        local str = "\n" .. rep("\t", level) .. "{\n"
        for k, v in pairs(tb) do
            local k_is_str = type(k) == "string" and 1 or 0
            local v_is_str = type(v) == "string" and 1 or 0
            str = str .. rep("\t", level + 1) .. "[" .. rep("\"", k_is_str) .. (tostring(k) or type(k)) .. rep("\"", k_is_str) .. "]" .. " = "
            if type(v) == "table" then
                if not lookup_table[v] and ((not max_level) or level < max_level) then
                    lookup_table[v] = true
                    str = str .. _dump(v, level + 1, dump_metatable) .. "\n"
                else
                    str = str .. (tostring(v) or type(v)) .. ",\n"
                end
            else
                str = str .. rep("\"", v_is_str) .. (tostring(v) or type(v)) .. rep("\"", v_is_str) .. ",\n"
            end
        end
        if dump_metatable then
                local mt = getmetatable(tb)
                if mt ~= nil and type(mt) == "table" then
                    str = str .. rep("\t", level + 1) .. "[\"__metatable\"]" .. " = "
                    if not lookup_table[mt] and ((not max_level) or level < max_level) then
                        lookup_table[mt] = true
                        str = str .. _dump(mt, level + 1, dump_metatable) .. "\n"
                    else
                        str = str .. (tostring(v) or type(v)) .. ",\n"
                    end
            end
        end
        str = str .. rep("\t", level) .. "},"
        return str
    end

    return _dump(t, level)
end

table.count = count
table.length = length
table.setlen = setlen
table.keys = keys
table.values = values
table.merge = merge
table.insertarray = insertarray
table.indexof = indexof
table.keyof = keyof
table.removebyvalue = removebyvalue
table.filter = filter
table.dump = dump
