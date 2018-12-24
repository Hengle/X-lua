local format = string.format
local CSUtil = CSUtil
local allCfgs = nil

local CfgManager = {}
-- 配置名列表 --
CfgManager.NameTable = {}

---便于修改csv数据路径
function create_datastream_path(relPath)
    return format('%sconfig/csv/%s', CSUtil.DataPath, relPath)
end

local function Init()
    allCfgs = require "Cfg.Config"
    for k, _ in pairs(allCfgs) do
        CfgManager.NameTable[k] = k
    end
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