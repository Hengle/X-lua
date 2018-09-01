local unpack = unpack or table.unpack
local insert = table.insert
local find = string.find
local format = string.format

-- 字符串分割
-- @str：被分割的字符串
-- @sep：分隔符
local function split(str, sep)
	local fields = {}
	local flag = "(.-)" .. sep
	local last_end = 1
	local s, e, cap = str:find(flag, 1)
	while s do
		if s ~= 1 or cap ~= "" then
			insert(fields, cap)
		end
		last_end = e + 1
		s, e, cap = str:find(flag, last_end)
	end
	if last_end <= #str then
		cap = str:sub(last_end)
		insert(fields, cap)
	end
	return fields
end
-- 字符串连接
local function join(join_table, joiner)
	if #join_table == 0 then
		return ""
	end

	local fmt = "%s"
	for i = 2, #join_table do
		fmt = fmt .. joiner .. "%s"
	end

	return format(fmt, unpack(join_table))
end
-- 是否包含
-- 注意：plain为true时，关闭模式匹配机制，此时函数仅做直接的 “查找子串”的操作
local function contains(target_string, pattern, plain)
	plain = plain or true
	local find_pos_begin, find_pos_end = find(target_string, pattern, 1, plain)
	return find_pos_begin ~= nil
end

-- 以某个字符串开始
local function startswith(target_string, start_pattern, plain)
	plain = plain or true
	local find_pos_begin, find_pos_end = find(target_string, start_pattern, 1, plain)
	return find_pos_begin == 1
end

-- 以某个字符串结尾
local function endswith(target_string, start_pattern, plain)
	plain = plain or true
	local find_pos_begin, find_pos_end = find(target_string, start_pattern, -#start_pattern, plain)
	return find_pos_end == #target_string
end


string.split = split
string.join = join
string.contains = contains
string.startswith = startswith
string.endswith = endswith
