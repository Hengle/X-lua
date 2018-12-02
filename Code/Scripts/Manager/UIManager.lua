local pairs = pairs
local require = require
local format = string.format
local match = string.match
local insert = table.insert
local loaded = package.loaded

local Time = Time
local Local = Local
local ResMgr = ResMgr
local Vector3 = Vector3
local LuaUtils = LuaUtils
local LuaHelper = LuaHelper
local GameObject = GameObject
local ConditionOp = ConditionOp
local gameEvent = GameEvent
local XUtil = XUtil
local GRoot = GRoot
local UIPackage = UIPackage
local Stage = Stage

local LogError = LogError
local printt = printt
local print = print
local printcolor = printcolor

local ViewUtil = require "Common.ViewUtil"

local LOAD_ING = 1              --正在加载中
local LOAD_SUCC = 2             --加载成功
local LAYER_UI = 20             --UI Layer层
local UI_LOAD_TYPE = CS.Game.ResourceLoadType.LoadBundleFromFile -- www方式加载

local _views = {}
local _stage = nil             -- 舞台对象

local UIManager = {}
local this = UIManager

---#注:未完成功能
---#遮罩界面,避免对当前窗口后面物体进行交互
---#界面返回,至上一次界面
---窗口定义UIShowType函数指明显示类型
UIShowType = {
    Default = 0, --默认策略显示
    Refresh = 1, --强制调用show
    DestroyWhenHide = 2, --Hide时释放资源,默认隐藏不销毁资源,仅在资源销毁时调用
}

local ConnectLua = function(view)
    local ins = FairyGUI.LuaWindow()
    ins:ConnectLua(view)

    view.base = ins             -- LuaWindow
    view.status = nil           -- 加载状态[加载成功/加载中]
    view.script = nil           -- 脚本对象
    view.hideTime = nil         -- 最后隐藏时刻
    view.loaded = false           -- 是否已加载
    view.isShow = false           -- 是否已加载
end

function UIManager.Init()
    _stage = FindObj("Stage")
    if _stage then
        GameObject.DontDestroyOnLoad(_stage)
    end

    gameEvent.UpdateEvent:Add(this.Update)
    gameEvent.SecondUpdateEvent:Add(this.SecondUpdate)
    gameEvent.LateUpdateEvent:Add(this.LateUpdate)
end

function UIManager.Update()
    for viewName, info in pairs(_views) do
        if info.isShow then
            if this.HasMethod(viewName, "Update") then
                this.Call(viewName, "Update")
            end
        end
    end
end

function UIManager.LateUpdate()
    for viewName, info in pairs(_views) do
        if info.isShow then
            if this.HasMethod(viewName, "LateUpdate") then
                this.Call(viewName, "LateUpdate")
            end
        end
    end
end

function UIManager.SecondUpdate(now)
    this.UnloadExpireView(now)
    for viewName, info in pairs(_views) do
        if info.isShow and this.HasMethod(viewName, "SecondUpdate") then
            this.Call(viewName, "SecondUpdate", now)
        end
    end
end

function UIManager.GetViewData(viewName)
    local data = _views[viewName]
    if not data then
        data = {}
        local _, fileName = match(viewName, "^(.*)%.(.*)$")
        data.fileName = ConditionOp(fileName, fileName, viewName)
        _views[viewName] = data
    end
    return data
end

function UIManager.GetModuleName(viewName)
    return "UI." .. viewName
end

function UIManager.GetViewModule(viewName)
    local view = require(this.GetModuleName(viewName))
    if not view then
        LogError("[UIManager]%s script file not find!", viewName)
    end
    return view
end

function UIManager.HasScript(viewName)
    return LuaHelper.HasScript(this.GetViewModule(viewName))
end
---字段指变量和函数
function UIManager.HasMethod(viewName, methodName)
    local view = this.GetViewModule(viewName)
    if not view then
        return false
    end
    local method = view[methodName]
    if not method then
        return false
    end
    return true
end

function UIManager.IsShow(viewName)
    local data = _views[viewName]
    if data and data.isShow then
        return data.isShow
    end
    return false
end

function UIManager.HasLoaded(viewName)
    local viewData = this.GetViewData(viewName)
    return viewData.status == LOAD_SUCC
end

function UIManager.Call(viewName, methodName, params)
    local view = this.GetViewModule(viewName)
    if not view then
        return
    end
    local method = view[methodName]
    if not method then
        print(format("[UIManager] %s.%s not find.", viewName, methodName))
        return
    end
    local viewData = this.GetViewData(viewName)
    if viewData.status ~= LOAD_SUCC and methodName ~= "Show" then
        LogError("[UIManager]%s not loaded! can't call method:%s", viewName, methodName)
        return
    end
    local succ = LuaUtils.Myxpcall(method, params)
    if succ then
        return true
    else
        LogError("[UIManager]call  %s.%s fail.", viewName, methodName)
        return false
    end
end

function UIManager.CallWithReturn(viewName, methodName, params)
    local view = this.GetViewModule(viewName)
    if not view then
        return nil
    end
    local method = view[methodName]
    if not method then
        print(format("[UIManager]%s.%s not find.", viewName, methodName))
        return nil
    end
    return method(params)
end

function UIManager.GetUIShowType(viewName)
    local uishowtype = UIShowType.Default
    if this.HasMethod(viewName, "UIShowType") then
        uishowtype = this.CallWithReturn(viewName, "UIShowType")
    end
    return uishowtype
end
---获取所有显示的窗口
function UIManager.GetDlgsShow()
    local list = {}
    for name, data in pairs(_views) do
        if data.isShow then
            insert(list, name)
        end
    end
    return list
end
function UIManager.IsPersistent(viewName)
    return Local.UIPersistentMap[viewName] == true
end

---------------------------------------------
---普通窗口操作方法
---------------------------------------------
function UIManager.Show(viewName, params)
    if Local.LogModuals.UIManager then
        printcolor("Show ==>>", viewName)
        printt(params)
    end
    local data = this.GetViewData(viewName)
    if data.isShow then
        return
    end
    if data.status ~= LOAD_SUCC then
        if not data.loaded then
            print(format("[UIMananger]view: %s %s", viewName, "not loaded!\n"), debug.traceback())
            data.loaded = LOAD_ING
            --ShowLoading()--显示界面加载提示界面
            ResMgr:AddTask(format("ui/%s.bundle", data.fileName), function(assetObj)
                if assetObj == nil then
                    return
                end
                local viewObj = GameObject.Instantiate(assetObj)
                GameObject.DontDestroyOnLoad(viewObj)
                if not viewObj then
                    data.loaded = nil
                    LogError(format("[UIMananger]view %s prefab load fail!", viewName))
                    return
                end
                data.status = LOAD_SUCC
                data.gameObject = viewObj
                data.fields = ViewUtil.ExportFields(viewObj)
                local trans = viewObj.transform
                --trans.parent = _uiRoot.transform
                trans.localPosition = Vector3.zero
                trans.localScale = Vector3.one
                viewObj.name = data.fileName
                viewObj.layer = LAYER_UI
                viewObj:SetActive(true)
                if this.Call(viewName, "Init", { viewName, viewObj, data.fields })
                        and this.Call(viewName, "Show", params) then
                    data.isShow = true
                    OnShow(viewName, params)
                else
                    viewObj:SetActive(false)
                end
            end, UI_LOAD_TYPE)
        end
        return
    end

    data.gameObject:SetActive(true)
    if this.Call(viewName, "Show", params) then
        data.isShow = true
        OnShow(viewName, params)
    else
        data.gameObject:SetActive(false)
    end
end


return UIManager
