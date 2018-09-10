local allCfgs = nil

local CfgManager = {}
-- 配置名列表 --
CfgManager.NameTable={}

function CfgManager.Init()
    allCfgs = require "Cfg.Config"
    for k, _ in pairs(allCfgs) do
        CfgManager.NameTable[k] = k
    end
end

function CfgManager.GetConfig(cfgName)
    if allCfgs then
        return allCfgs[cfgName]
    end
    return nil
end

return CfgManager