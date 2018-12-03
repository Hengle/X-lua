local pairs = pairs
local require = require
local format = string.format
local match = string.match
local insert = table.insert
local loaded = package.loaded
local trim = string.trim
local clone = table.clone

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
local printyellow = printyellow

local ViewUtil = require "Common.ViewUtil"

local LOAD_ING = 1              --正在加载中
local LOAD_SUCC = 2             --加载成功
local LAYER_UI = 20             --UI Layer层
local UI_LOAD_TYPE = CS.Game.ResourceLoadType.LoadBundleFromFile -- www方式加载
local DLG_LOADING_MASK = "DlgUILock"    -- 加载界面时锁UI,禁止一切操作

local _views = {}
local _stage = nil             -- 舞台对象
local _isLocked = false        -- UI界面被锁,禁止一切操作
local _callBackDestroyAllDlgs

local NeedRefresh               --是否需要刷新
local OnInit
local DoShow
local OnShow                    --页面显示回调
local DoHide
local OnHide                    --页面隐藏回调
local ShowLoadingLock           --显示加载中提示!
local HideLoadingLock           --隐藏加载中提示!

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

---@class UIData
local Data = {
    base = nil,
    script = nil,
    fields = nil,
    params = nil, -- 用于传参
    fileName = nil,
    viewName = nil,
    status = nil,
    loaded = nil,
    isShow = false,

    OnInit = OnInit, --  脚本对应事件 Init
    DoShowTween = DoShow, --  脚本对应事件 DoShow
    OnShow = OnShow, --  脚本对应事件 Show
    DoHideTween = DoHide, --  脚本对应事件 DoHide
    OnHide = OnHide, --  脚本对应事件 Hide
}

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

        if info.needRefresh and NeedRefresh(viewName) then
            if Local.LogModuals.UIManager then
                print("[UIManager]LateUpdate Refresh", viewName)
                printt(info.refreshParams, "LateUpdate Refresh")
            end
            info.needRefresh = false
            this.Call(viewName, "Refresh", info.params)
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
        data = clone(Data)
        local _, fileName = match(viewName, "^(.*)%.(.*)$")
        data.fileName = ConditionOp(fileName, fileName, viewName)
        data.viewName = viewName
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
ShowLoadingLock = function(beginTime, endTime)
    if _isLocked == true then
        return
    end
    if Local.LogModuals.UIManager then
        printyellow("[UIManager]ShowLoading Locked")
    end
    local params = { beginTime = ConditionOp(beginTime, beginTime, 0.5), endTime = ConditionOp(endTime, endTime, 3) }
    if this.IsShow(DLG_LOADING_MASK) then
        this.Refresh(DLG_LOADING_MASK, params)
    else
        this.Show(DLG_LOADING_MASK, params)
    end
end
HideLoadingLock = function()
    if _isLocked == true then
        return
    end
    if Local.LogModuals.UIManager then
        printyellow("[UIManager]HideLoading Locked")
    end
    this.Hide(DLG_LOADING_MASK)
end
---@param data UIData
OnInit = function(data)
    local viewName = data.viewName
    ---@type Game.LuaWindow
    local window = data.base
    window:Center()
    window.displayObject.gameObject.layer = LAYER_UI
    if this.Call(viewName, "Init", { viewName, window, data.fields }) then
        --
    else
        window:HideImmediately()
    end
end
---@param data UIData
DoShow = function(data)
    local viewName = data.viewName
    ---@type Game.LuaWindow
    local window = data.base
    window:FinishDisplay()


end
---@param data UIData
OnShow = function(data)
    local window, viewName, params = data.base, data.viewName, data.params
    if Local.LogModuals.UIManager then
        printyellow("[UIManager]OnShow", viewName)
    end
    if viewName ~= DLG_LOADING_MASK then
        HideLoadingLock()
    end

    if this.Call(viewName, "Show", data.params) then
        data.isShow = true
        this.Refresh(viewName, params)
    else
        window:HideImmediately()
    end
end
---@param data UIData
DoHide = function(data)

end
---@param data UIData
OnHide = function(data)
    if Local.LogModuals.UIManager then
        printyellow("OnHide ==>>", viewName)
    end
    local data = this.GetViewData(viewName)
    if this.Call(viewName, "Hide") then
        data.isShow = false
        data.gameObject:SetActive(false)
        data.dialogViewName = nil
        if this.IsUIShowType(viewName, UIShowType.DestroyWhenHide) then
            this.Destroy(viewName, "Destroy")
        end
    end
end

NeedRefresh = function(viewName)
    return this.IsShow(viewName) --or this.IsInStack(viewName)
end
local ConnectLua = function(data, com)
    ---@type Game.LuaWindow
    local ins = FairyGUI.LuaWindow()
    ins.contentPane = com
    ins:ConnectLua(data)
    data.base = ins     -- LuaWindow
    data.hideTime = 0   -- 脚本对象
    return ins
end

function UIManager.Show(viewName, params)
    if Local.LogModuals.UIManager then
        printyellow("[UIManager]Show", viewName)
        printt(params)
    end
    local data = this.GetViewData(viewName)
    if data.isShow then
        return
    end
    data.params = params
    ShowLoadingLock(viewName)
    if data.status ~= LOAD_SUCC then
        if not data.loaded then
            print(format("view: %s %s", viewName, "not loaded!\n"))
            data.loaded = LOAD_ING
            --ShowLoading()--显示界面加载提示界面
            ResMgr:AddTask(format("ui/%s.bundle", data.fileName), function(fui)
                if fui == nil then
                    return
                end
                local name = trim("_fui")
                local viewCom = UIPackage.CreateObject(name, name).asCom
                local window = ConnectLua(data, viewCom)
                if not viewCom then
                    data.loaded = nil
                    LogError(format("view %s fgui load fail!", viewName))
                    return
                end
                data.status = LOAD_SUCC
                data.fields = ViewUtil.ExportFields(viewCom)
                window:Show()
            end, UI_LOAD_TYPE)
        end
        return
    end

    data.base:Show()
end
function UIManager.Refresh(viewName, params)
    if Local.LogModuals.UIManager then
        printyellow("[UIManager]Refresh", viewName)
    end
    if NeedRefresh(viewName) then
        local data = this.GetViewData(viewName)
        data.needRefresh = true
        data.params = params
    end
end
function UIManager.ShowOrRefresh(viewName, params)
    if this.IsShow(viewName) then
        this.Refresh(viewName, params)
    else
        this.Show(viewName, params)
    end
end
function UIManager.IfShowThenCall(viewName, methodName, params)
    if this.IsShow(viewName) then
        this.Call(viewName, methodName, params)
    end
end
function UIManager.Hide(viewName)
    if Local.LogModuals.UIManager then
        printyellow("[UIManager]Hide", viewName)
    end
    local data = this.GetViewData(viewName)
    data.hideTime = Time.time
    if not data.isShow then
        print(format("view:%s not show!", viewName))
        return
    end
    if data.uifadeout ~= nil then
        --local DlgHiding = require "UI.DlgHiding"
        --UIEventListenerHelper.SetPlayTweenFinish(data.uifadeout, function(uifadeout)
        --    DlgHiding.OnFadeOutEnd()
        --    OnHide(viewName)
        --end)
        --data.uifadeout:Play(true)
        --DlgHiding.OnFadeOutBegin()
    else
        OnHide(viewName)
    end
    --local NoviceGuideTrigger = require "noviceguide.noviceguide_trigger"
    --NoviceGuideTrigger.HideDialog(viewName)
end
function UIManager.HideImmediate(viewName)
    if Local.LogModuals.UIManager then
        printyellow("[UIManager]HideImmediate", viewName)
    end
    local data = this.GetViewData(viewName)
    data.hideTime = Time.time
    if not data.isShow then
        print(format("view:%s not show!", viewName))
        return
    end
    OnHide(viewName)
end
function UIManager.Destroy(viewName)
    local data = this.GetViewData(viewName)
    if data.isShow then
        this.HideImmediate(viewName)
    end
    this.Call(viewName, "Destroy")
    if data.fields then
        for k, _ in pairs(data.fields) do
            data.fields[k] = nil
        end
        data.fields = nil
    end
    _views[viewName] = nil
    assert(loaded[this.GetModuleName(viewName)])
    loaded[this.GetModuleName(viewName)] = nil
    GameObject.Destroy(data.gameObject)
end
function UIManager.HideAll()
    for name, data in pairs(_views) do
        this.Hide(name)
    end
end
function UIManager.RegistCallBackDestroyAllDlgs(callback)
    printt(_callBackDestroyAllDlgs, "RegistCallBackDestroyAllDlgs")
    _callBackDestroyAllDlgs = callback
end
function UIManager.DestroyAllDlgs()
    local list = this.GetDlgsShow()
    local d = false
    for _, name in pairs(list) do
        d = false
        for _, persistentName in pairs(Local.UIPersistentList) do
            if name == persistentName then
                this.Hide(name)
                d = true
                break
            end
        end
    end
    --_dialogStack:Clear()
    if _callBackDestroyAllDlgs then
        _callBackDestroyAllDlgs()
        _callBackDestroyAllDlgs = nil
    end
end

return UIManager
