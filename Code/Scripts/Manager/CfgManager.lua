local format = string.format
local ipairs = ipairs
local Stream = require("Cfg.DataStruct")
local CSUtil = CSUtil
local allCfgs = nil

local function LoadCsv(relPath, methodName, index)
    local path = format('%sconfig/csv/%s', CSUtil.DataPath, relPath)
    local data = Stream.new(path)
    local cfg = {}
    while data:NextRow() do
        local value = data[methodName](data)
        local key = value[index]
        cfg[key] = value
    end
    return cfg
end

local function LoadAllCsv(cfgs)
    for _, s in ipairs(cfgs) do
        allCfgs[s.name] = LoadCsv(s.output, s.method, s.index)
    end
end

local function Init()
    LoadAllCsv(require "Cfg.Config")
end

local function GetConfig(name)
    if allCfgs then
        return allCfgs[name]
    end
    return nil
end
local function GetConfigByIndex(name, index)
    return allCfgs[name][index]
end

---@class CfgManager
local CfgManager = {
    Init = Init,
    GetConfig = GetConfig,
    GetConfigByIndex = GetConfigByIndex,
}
return CfgManager