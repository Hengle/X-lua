--[[
-- table扩展工具类，对table不支持的功能执行扩展
-- 注意：
-- 1、所有参数带hashtable的函数，将把table当做哈希表对待
-- 2、所有参数带array的函数，将把table当做可空值数组对待
-- 3、所有参数带t的函数，对表通用，不管是哈希表还是数组
--]]
local type = type
local pairs = pairs
local ipairs = ipairs
local rep = string.rep
local tostring = tostring
local getmetatable = getmetatable
local insert = table.insert
local floor = math.floor
local sort = table.sort
local concat = table.concat
local getmetatable = getmetatable
local setmetatable = setmetatable
local format = string.format

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
local function filter(hashtable, condition)
    local filter = {}
    for k, v in pairs(hashtable) do
        if not condition(k, v) then
            filter[k] = v
        end
    end
    return filter
end
-- dump table
local function dump(hashtable, max_level)
    local limit = max_level or 3
    local _dump = nil
    _dump = function(t, level)
        if level >= limit then
            return
        end
        local code = { "{\n" }
        local tab = rep("\t", level)
        for k, v in pairs(t) do
            if type(v) == "number" then
                insert(code, format("%s\t<color=orange>[%s]</color> = %s,\n",tab, k, v))
            elseif type(v) == "string" then
                insert(code, format("%s\t<color=orange>%s</color> = %s,\n",tab, k, v))
            elseif type(v) == "table" then
                insert(code, format("%s\t<color=orange>%s</color> = %s,\n",tab, k, _dump(v, level + 1) or ''))
            end
        end
        insert(code, tab .. "}")
        return concat(code)
    end

    return _dump(hashtable, 0)
end

-- 清理表
local function clear(t)
    if t == nil then
        return
    end
    for k, _ in pairs(t) do
        t[k] = nil
    end
end
-- 深复制表,但不包含function类型
local function deepcopyto(src, dst)
    for k, v in pairs(src) do
        if type(v) ~= "table" then
            dst[k] = v
        else
            dst[k] = dst[k] or {}
            deepcopyto(v, dst[k])
        end
    end
end
-- 浅复制表,只取第一层基础类型
local function shallowcopyto(src, dst)
    for k, v in pairs(src) do
        dst[k] = v
    end
end
-- 自身完全复制;注意死循环
local function clone(src)
    local dst = {}
    for k, v in pairs(src) do
        if type(v) == "table" then
            dst[k] = clone(v)
        else
            dst[k] = v
        end
    end
    local mt = getmetatable(src)
    setmetatable(dst, mt)
    return dst
end
--二分法查找
local function binsearch(array, v)
    local from, to
    from = 1
    to = #array
    while from <= to do
        local mid = floor(from / 2 + to / 2)
        -- PrintYellow(from,to,mid)
        if array[mid] > v then
            to = mid - 1
        elseif array[mid] < v then
            from = mid + 1
        else
            return array[mid]
        end
    end
    return nil
end
-- 数组对比
local function swap(t, a, b)
    local temp = t[a]
    t[a] = t[b]
    t[b] = temp
end
local function mysort(t, cmp)
    for i = 1, #t do
        for j = #t, i + 1, -1 do
            if not cmp(t[j - 1], t[j]) then
                swap(t, j, j - 1)
            end
        end
    end
end
local function compare(array1, array2, cmp)
    if array1 == array2 then
        return true
    else
        if nil == array1 or nil == array2 then
            return false
        elseif #array1 ~= #array2 then
            return false
        else
            if cmp then
                mysort(array1, cmp)
                mysort(array2, cmp)
            else
                sort(array1)
                sort(array2)
            end
            for i, value in ipairs(array1) do
                if value ~= array2[i] then
                    print(format("[table.compare] array1[%s]=[%s] while array2[%s]=[%s], return false!", i, value, i, array2[i]))
                    return false
                end
            end
            return true
        end
    end
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
table.clear = clear
table.deepcopyto = deepcopyto
table.shallowcopyto = shallowcopyto
table.clone = clone
table.binsearch = binsearch
table.compare = compare




-- dump表
-- local function dump(t, dump_metatable, max_level)
--     local lookup_table = {}
--     local level = 0
--     local dump_metatable = dump_metatable
--     local max_level = max_level or 1

--     local function _dump(t, level)
--         local str = "\n" .. rep("\t", level) .. "{\n"
--         for k, v in pairs(t) do
--             local k_is_str = type(k) == "string" and 1 or 0
--             local v_is_str = type(v) == "string" and 1 or 0
--             str = str .. rep("\t", level + 1) .. "[" .. rep("\"", k_is_str) .. (tostring(k) or type(k)) .. rep("\"", k_is_str) .. "]" .. " = "
--             if type(v) == "table" then
--                 if not lookup_table[v] and ((not max_level) or level < max_level) then
--                     lookup_table[v] = true
--                     str = str .. _dump(v, level + 1, dump_metatable) .. "\n"
--                 else
--                     str = str .. (tostring(v) or type(v)) .. ",\n"
--                 end
--             else
--                 str = str .. rep("\"", v_is_str) .. (tostring(v) or type(v)) .. rep("\"", v_is_str) .. ",\n"
--             end
--         end
--         if dump_metatable then
--                 local mt = getmetatable(t)
--                 if mt ~= nil and type(mt) == "table" then
--                     str = str .. rep("\t", level + 1) .. "[\"__metatable\"]" .. " = "
--                     if not lookup_table[mt] and ((not max_level) or level < max_level) then
--                         lookup_table[mt] = true
--                         str = str .. _dump(mt, level + 1, dump_metatable) .. "\n"
--                     else
--                         str = str .. (tostring(v) or type(v)) .. ",\n"
--                     end
--             end
--         end
--         str = str .. rep("\t", level) .. "},"
--         return str
--     end

--     return _dump(t, level)
-- end