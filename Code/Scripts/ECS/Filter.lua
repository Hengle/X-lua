local type = type
local error = error
local pcall = pcall
local select = select
local concat = table.concat
local sort = table.sort
local format = string.format
local sub = string.sub
local byte = string.byte
local loadstring = loadstring or load

local function getchr(c)
    return "\\" .. c:byte()
end
local function make_safe(text)
    return ("%q"):format(text):gsub('\n', 'n'):gsub("[\128-\255]", getchr)
end

local function filterJoinRaw(prefix, seperator, ...)
    local accum = {}
    local build = {}
    for i = 1, select('#', ...) do
        local item = select(i, ...)
        if type(item) == 'string' then
            accum[#accum + 1] = ("(e[%s] ~= nil)"):format(make_safe(item))
        elseif type(item) == 'function' then
            build[#build + 1] = ('local subfilter_%d_ = select(%d, ...)')
                    :format(i, i)
            accum[#accum + 1] = ('(subfilter_%d_(system, e))'):format(i)
        else
            error 'Filter token must be a string or a filter function.'
        end
    end
    local source = ('%s\nreturn function(system, e) return %s(%s) end')
            :format(
            concat(build, '\n'),
            prefix,
            concat(accum, seperator))
    local loader, err = loadstring(source)
    if err then
        error(err)
    end
    return loader(...)
end

local function filterJoin(...)
    local state, value = pcall(filterJoinRaw, ...)
    if state then
        return value
    else
        return nil, value
    end
end

--- Makes a Filter that selects Entities with all specified Components and Filters.
local function RequireAll(...)
    return filterJoin('', ' and ', ...)
end

--- Makes a Filter that selects Entities with at least one of the specified Components and Filters.
local function RequireAny(...)
    return filterJoin('', ' or ', ...)
end

--- Makes a Filter that rejects Entities with all specified Components and Filters, and selects all other Entities.
local function RejectAll(...)
    return filterJoin('not', ' and ', ...)
end

--- Makes a Filter that rejects Entities with at least one of the specified Components and Filters, and selects all other Entities.
local function RejectAny(...)
    return filterJoin('not', ' or ', ...)
end

local function Hash(str)
    local seed = 131
    local hash = 0
    for i = 1, #str do
        hash = hash * seed + byte(sub(str, i, i))
    end
    return (hash & 0x7FFFFFFF)
end
-- 为filter 筛选条件表唯一标识
local function GenUnique(filter)
    return Hash(concat(sort(filter), '.'))
end

---@class Filter
local Filter = {
    RequireAll = RequireAll,
    RequireAny = RequireAny,
    RejectAll = RejectAll,
    RejectAny = RejectAny,
    GenUnique = GenUnique,
}
return Filter