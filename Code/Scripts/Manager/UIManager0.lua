local pairs = pairs
local require = require
local format = string.format
local match = string.match
local loaded = package.loaded

local Time = Time
local Local = Local
local ResMgr = ResMgr
local Vector3 = Vector3
local LogError = LogError
local LuaUtils = LuaUtils
local LuaHelper = LuaHelper
local GameObject = GameObject
local printt = printt
local printcolor = printcolor
local ConditionOp = ConditionOp
local gameEvent = GameEvent
local GRoot = GRoot
local Stage = Stage
local XUtil = XUtil

local ViewUtil = require "Common.ViewUtil"

local LOAD_ING = 1              --正在加载中
local LOAD_SUCC = 2             --加载成功
local LAYER_UI = 20             --UI Layer层
local MAX_HIDE_VIEW_NUM = 0
local UI_LOAD_TYPE = CS.Game.ResourceLoadType.LoadBundleFromFile -- www方式加载

local NeedRefresh               --是否需要刷新

local OnShow                    --页面显示回调
local OnHide                    --页面隐藏回调

local OnShowTab                 --显示Tab页
local OnHideTab                 --隐藏Tab页

local CallBackDestroyAllDlgs    --销毁所有界面回调事件

--[[        界面相关数据
    filename                -- 文件名
    status                  -- 加载状态[加载成功/加载中]
    loaded                  -- 是否已被加载
    gameObject              -- 界面游戏对象
    script                  -- 脚本对象
    hideTime                -- 最后隐藏时刻

    dialogViewName          -- 是带dlgdialog的窗口?
    isShow                  -- 是否已显示
    needRefresh             -- 是否需要刷新
    refreshParams           -- 刷新界面时所带参数

-----废除一下参数
    --isDialog                -- 是否为tab组窗口[tab组嵌套,可形成递归,树形结构]
    --initedTabs              -- tabName,true:切页是否已初始化
    --tabIndex                -- 切页索引
    --tabs                    -- 切页数组
    --tabGroupStates          -- tabIndex,map{tabName,isShow}切页数组状态
--]]
---@type Stack
local _dialogStack = nil

local _views = {}               --  viewName:相对UI目录
local _uiRoot = nil

local UIManager = {}
local this = UIManager

--UIShowType = {
--    Default                 = 0, --默认策略显示
--    Refresh                 = 1, --强制调用show
--    DestroyWhenHide         = 2, --Hide时释放资源,默认隐藏不销毁资源,仅在资源销毁时调用
--}

local onInit = nil
local doShowAnimation = nil
local onShown = nil
local doHideAnimation = nil
local onHide = nil

---窗体类,实例载体
---@type FairyGUI.Window
local Window = Class:new()
function Window:ctor(...)
    local ins = FairyGUI.LuaWindow()
    XUtil.state(ins, self)
    ins:ConnectLua(self)

    self.name = 'window?'
    self.status = nil           -- 加载状态[加载成功/加载中]
    self.script = nil           -- 脚本对象
    self.hideTime = nil         -- 最后隐藏时刻
    self.loaded = nil           -- 是否已加载
end

onInit = function(window)

end
doShowAnimation = function(window)

end
onShown = function(window)

end
doHideAnimation = function(window)

end
onHide = function(window)

end


---特殊:模态窗口!



--显示已经加载的页面 用于弹出堆栈 或者tab页切换
local function ShowLoadedView(viewName)
    if Local.LogModuals.UIManager then
        print("[UIManager]ShowLoadedView: ", viewName)
    end
    local data = this.GetViewData(viewName)
    local params = data.params
    if not data.isShow and this.HasScript(viewName) then
        if this.IsUIShowType(viewName, UIShowType.Refresh) then
            if this.HasMethod(viewName, "ShowTab") and not this.IsUIShowType(viewName, UIShowType.ShowImmediate) then
                this.Call(viewName, "ShowTab", params)
            else
                this.Show(viewName, params)
            end
        else
            data.gameObject:SetActive(true)
            data.isShow = true
        end
    end

    if data.isDialog then
        this.RefreshDlgdialog(viewName, data.tabIndex)
        data.dialogViewName = viewName
        local tabGroup = this.GetTabGroup(viewName, data.tabIndex)
        if tabGroup then
            for tabName, isShow in pairs(data.tabGroupStates[data.tabIndex]) do
                if isShow then
                    local tabData = this.GetViewData(tabName)
                    tabData.dialogViewName = viewName
                    this.ShowLoadedView(tabName, tabData.params)
                end
            end
        end
    end
end
--隐藏已经加载的页面 用于弹出堆栈 或者tab页切换
local function HideLoadedView(viewName)
    if Local.LogModuals.UIManager then
        print("HideLoadedView ==>>", viewName)
    end
    local data = this.GetViewData(viewName)
    if data.isShow and this.HasScript(viewName) then
        if this.IsUIShowType(viewName, UIShowType.Refresh) then
            if this.HasMethod(viewName, "HideTab") and not this.IsUIShowType(viewName, UIShowType.ShowImmediate) then
                this.Call(viewName, "HideTab")
            else
                this.Hide(viewName)
            end
        else
            data.hideTime = Time.time
            data.gameObject:SetActive(false)
            data.isShow = false
        end
    end

    if data.isDialog then
        local tabGroup = this.GetTabGroup(viewName, data.tabIndex)
        if tabGroup then
            for tabName, isShow in pairs(data.tabGroupStates[data.tabIndex]) do
                if isShow then
                    this.HideLoadedView(tabName)
                end
            end
        end
    end
end
--显示加载中提示!
local function ShowLoading(beginTime, endTime)
    if _isLocked == true then
        return
    end
    if Local.LogModuals.UIManager then
        print("ShowLoading ==>>")
    end
    local params = { beginTime = ConditionOp(beginTime, beginTime, 0.5), endTime = ConditionOp(endTime, endTime, 3) }
    if this.IsShow("dlgopenloading") then
        this.Refresh("dlgopenloading", params)
    else
        this.Show("dlgopenloading", params)
    end
end
--隐藏加载中提示!
local function HideLoading()
    if _isLocked == true then
        return
    end
    if Local.LogModuals.UIManager then
        print("HideLoading ==>>")
    end
    this.Hide("dlgopenloading")
end


function UIManager.Init()
    _uiRoot = FindObj("/UIRoot/UICamera")
    if _uiRoot and _uiRoot.transform.parent then
        GameObject.DontDestroyOnLoad(_uiRoot.transform.parent)
    end

    gameEvent.UpdateEvent:Add(this.Update)
    gameEvent.LateUpdateEvent:Add(this.LateUpdate)
    _dialogStack = Stack:new()
end

function UIManager.Update()
    for viewName, info in pairs(_views) do
        if info.isShow then
            if (this.HasMethod(viewName, "Update")) then
                this.Call(viewName, "Update")
            end
        end
    end
    for i = #_playingParticleSystems, 1, -1 do
        local particle = _playingParticleSystems[i].particle
        if not particle.isPlaying then
            local callback = _playingParticleSystems[i].callback
            if callback then
                callback()
            end
            table.remove(_playingParticleSystems, i)
        else
            _playingParticleSystems[i].time = _playingParticleSystems[i].time + Time.deltaTime
        end
    end
end

function UIManager.LateUpdate()
    for viewName, info in pairs(_views) do
        if info.isShow then
            if (this.HasMethod(viewName, "LateUpdate")) then
                this.Call(viewName, "LateUpdate")
            end

        end

        if info.needRefresh and NeedRefresh(viewName) then
            if Local.LogModuals.UIManager then
                print("LateUpdate Refresh ==>>", viewName)
                printt(info.refreshparams)
            end
            info.needRefresh = false
            --PrintYellow("ViewName",viewName)
            this.Call(viewName, "Refresh", info.refreshparams)
        end

    end

    --late_updateCharacterInfoOnUI()
end

function UIManager.SecondUpdate(now)
    this.UnloadExpireView(now)
    for viewName, info in pairs(_views) do
        if info.isShow and this.HasMethod(viewName, "SecondUpdate") then
            this.Call(viewName, "SecondUpdate", now)
        end
    end
end


function UIManager.UnloadExpireView(now)
    local unshowViewNum = 0
    local toDestroyViewName
    local minHideTime = now
    for name, data in pairs(_views) do
        if data ~= nil and data.status == LOAD_SUCC and not data.isShow and not this.IsPersistent(name) and not this.IsInStack(name) then
            unshowViewNum = unshowViewNum + 1
            if data.hideTime ~= nil and data.hideTime < minHideTime then
                toDestroyViewName = name
                minHideTime = data.hideTime
            end
        end
    end
    if toDestroyViewName and unshowViewNum > MAX_HIDE_VIEW_NUM then
        this.Destroy(toDestroyViewName)
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
        LogError("View:%s script file not find!", viewName)
    end
    return view
end

function UIManager.HasScript(viewName)
    return LuaHelper.ScriptExists(format("UI.%s", viewName))
end

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
        print(format("View. %s.%s not find.", viewName, methodName))
        return
    end
    local viewData = this.GetViewData(viewName)
    if viewData.status ~= LOAD_SUCC and methodName ~= "Show" and methodName ~= "ShowDialog" and methodName ~= "ShowTabByIndex" and methodName ~= "ShowTab" then
        LogError("uimanager. view:%s not loaded! can't call method:%s", viewName, methodName)
        return
    end
    local succ = LuaUtils.Myxpcall(method, params)
    if succ then
        return true
    else
        LogError("View.call  %s.%s fail.", viewName, methodName)
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
        print(format("View. %s.%s not find.", viewName, methodName))
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

function UIManager.IsUIShowType(viewName, uiShowType)
    local viewUIShowType = this.GetUIShowType(viewName)
    return uiShowType & viewUIShowType > 0
end
---获取所有显示的窗口
function UIManager.GetDlgsShow()
    local list = {}
    for name, data in pairs(_views) do
        if data.isShow then
            table.insert(list, name)
        end
    end
    return list
end

function UIManager.IsPersistent(viewName)
    return Local.UIPersistentMap[viewName] == true
end

function UIManager.IsInStack(viewName)
    if _dialogStack:Count() > 0 then
        _dialogStack:InitEnumerator()
        while _dialogStack:MoveNext() do
            if _dialogStack:Current() == viewName then
                return true
            else
                local data = this.GetViewData(_dialogStack:Current())
                if data.isInitedTabs and data.isInitedTabs[viewName] then
                    return true
                end
            end
        end
    end
    return false
end

---遮罩层功能?
function UIManager.SetLock(lock)
    _isLocked = lock
end
function UIManager.GetIsLock()
    return _isLocked
end


------------------------------------------------------------------
---普通窗口操作方法
------------------------------------------------------------------
OnShow = function(viewName, params)
    if Local.LogModuals.UIManager then
        printcolor("OnShow ==>>", viewName)
    end
    local data = this.GetViewData(viewName)
    data.params = params
    if viewName ~= "dlgopenloading" then
        HideLoading()
    end
    if data.dialogViewName ~= nil and data.dialogViewName ~= this.CurrentDialogName() then
        print("Dialog has been hide", "dialog name:", data.dialogViewName, "current dialog name:", this.CurrentDialogName())
        --HideLoadedView(viewName)隐藏界面加载提示界面
    elseif this.HasScript(viewName) then
        this.Refresh(viewName, params)
        --if data.uifadein ~= nil then
        --    data.uifadein:Play(true)
        --end
    end

end
OnHide = function(viewName)
    if Local.LogModuals.UIManager then
        printcolor("OnHide ==>>", viewName)
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
OnShowTab = function(viewName, tabName, params)
    if Local.LogModuals.UIManager then
        printcolor("OnShowTab ==>>", viewName, tabName)
    end
    local data = this.GetViewData(viewName)
    local tabData = this.GetViewData(tabName)
    if tabData.isShow then
        return
    end

    if data.isInitedTabs[tabName] then
        tabData.params = params
        ShowLoadedView(tabName, params)
    else
        if (this.HasMethod(tabName, "ShowTab")) and not this.IsUIShowType(tabName, UIShowType.ShowImmediate) then
            this.Call(tabName, "ShowTab", params)
        else
            this.Show(tabName, params)
        end

        data.isInitedTabs[tabName] = true
        tabData.dialogViewName = viewName
    end
end
OnHideTab = function(viewName, tabName)
    if Local.LogModuals.UIManager then
        printcolor("OnHideTab ==>>", viewName, tabName)
    end
    local data = this.GetViewData(viewName)
    if Local.LogModuals.UIManager then
        printt(data.tabGroupStates)
    end
    local tabData = this.GetViewData(tabName)
    if not tabData.isShow then
        return
    end
    if Local.LogModuals.UIManager then
        printcolor("TabName:", tabName, "inited:", data.isInitedTabs[tabName])
    end
    if data.isInitedTabs[tabName] then
        HideLoadedView(tabName)
    end
end
NeedRefresh = function(viewName)
    return this.IsShow(viewName) or this.IsInStack(viewName)
end
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
            print(format("View: %s %s", viewName, "not loaded!\n"), debug.traceback())
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
                    LogError(format("View %s prefab load fail!", viewName))
                    return
                end
                data.status = LOAD_SUCC
                data.gameObject = viewObj
                data.fields = ViewUtil.ExportFields(viewObj)
                local trans = viewObj.transform
                trans.parent = _uiRoot.transform
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

function UIManager.Refresh(viewName, params)
    if Local.LogModuals.UIManager then
        printcolor("Refresh ==>>", viewName)
    end
    if NeedRefresh(viewName) then
        local data = this.GetViewData(viewName)
        data.needRefresh = true
        data.refreshParams = params
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
        printcolor("Hide ==>>", viewName)
    end
    local data = this.GetViewData(viewName)
    data.hideTime = Time.time
    if not data.isShow then
        print(format("View:%s not show!", viewName))
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
        printcolor("HideImmediate ==>>", viewName)
    end
    local data = this.GetViewData(viewName)
    data.hideTime = Time.time
    if not data.isShow then
        print(format("View:%s not show!", viewName))
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
    printt(CallBackDestroyAllDlgs, "RegistCallBackDestroyAllDlgs")
    CallBackDestroyAllDlgs = callback
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
    _dialogStack:Clear()
    if CallBackDestroyAllDlgs then
        CallBackDestroyAllDlgs()
        CallBackDestroyAllDlgs = nil
    end
end


---------------------------------------------------下列函数可废除


--Tab类型界面控制
function UIManager.ShowTab(tabName, params)
    if _dialogStack:Count() > 0 then
        local viewName = _dialogStack:Peek()
        if Local.LogModuals.UIManager then
            print("ShowTab ==>>", viewName, tabName)
            printt(params)
        end
        local data = this.GetViewData(viewName)
        data.tabGroupStates[data.tabIndex][tabName] = true
        OnShowTab(viewName, tabName, params)
    end
end

function UIManager.HideTab(tabName)
    if _dialogStack:Count() > 0 then
        local viewName = _dialogStack:Peek()
        if Local.LogModuals.UIManager then
            printcolor("HideTab ==>>", viewName, tabName)
        end
        local data = this.GetViewData(viewName)
        if Local.LogModuals.UIManager then
            printt(data.tabGroupStates)
        end
        data.tabGroupStates[data.tabIndex][tabName] = false
        OnHideTab(viewName, tabName)
    end
end

function UIManager.GetDialog(viewName)
    if _dialogConfigs == nil then
        print("GetDialog Error! Dialog CSV Not Loaded!", viewName)
        return nil
    end
    return _dialogConfigs[viewName]
end

function UIManager.GetTabGroup(viewName, tabIndex)
    local dialog = this.GetDialog(viewName)

    if dialog == nil then
        print("GetTabGroup Error! No TabGroup!", viewName, tabIndex)
        return nil
    end
    return dialog.tabgroups[tabIndex]
end

function UIManager.ShowTabByIndex(viewName, tabIndex, params)
    if Local.LogModuals.UIManager then
        printcolor("ShowTabByIndex ==>>", viewName, tabIndex)
    end
    local data = this.GetViewData(viewName)
    local tabgroup = this.GetTabGroup(viewName, tabIndex)
    if tabgroup then
        data.tabIndex = tabIndex
        if data.tabGroupStates[data.tabIndex] == nil then
            data.tabGroupStates[data.tabIndex] = {}
            for _, tab in ipairs(tabgroup.tabs) do
                data.tabGroupStates[data.tabIndex][tab.tabname] = not tab.hide
            end
        end
        if Local.LogModuals.UIManager then
            printt(data.tabGroupStates)
        end
        for tabName, isShow in pairs(data.tabGroupStates[data.tabIndex]) do
            if isShow then
                OnShowTab(viewName, tabName, params)
            end
        end
    end
end

function UIManager.HideTabByIndex(viewName, tabIndex)
    if Local.LogModuals.UIManager then
        print("HideTabByIndex ==>>", viewName, tabIndex)
    end
    local tabgroup = this.GetTabGroup(viewName, tabIndex)
    if tabgroup then
        for _, tab in ipairs(tabgroup.tabs) do
            OnHideTab(viewName, tab.tabName)
        end
    end
    --local NoviceGuideTrigger = require "noviceguide.noviceguide_trigger"
    --NoviceGuideTrigger.HideDialog(viewName)
end

------------------------------------------------------------------
---下面是公用弹窗的用法
------------------------------------------------------------------

--local function ShowAlertDlg(params)
--    local InfoManager = require "assistant.infomanager"
--    if isLocked == true then
--        return InfoManager.AddInfo(params)
--    end
--    return InfoManager.ShowInfo(params)
--end





------------------------------------------------------------------
---针对模块操作方法
------------------------------------------------------------------
function UIManager.ShowMaincityDlgs()
    if Local.LogModuals.UIManager then
        print("ShowMaincityDlgs ==>>")
    end
    for _, dlg in pairs(Local.MaincityDlgList) do
        if not this.IsShow(dlg) then
            this.Show(dlg)
        end
    end
end

function UIManager.HideMainCityDlgs()
    if Local.LogModuals.UIManager then
        print("HideMainCityDlgs ==>>")
    end
    for _, dlg in pairs(Local.MaincityDlgList) do
        if this.IsShow(dlg) then
            this.Hide(dlg)
        end
    end
end

function UIManager.RefreshDlgdialog(dialogViewName, tabindex)
    local dialogData = this.GetViewData("dlgdialog")
    if dialogData ~= nil and dialogData.uifadeout ~= nil then
        dialogData.uifadeout:Stop()
    end
    this.ShowOrRefresh("dlgdialog", { viewName = dialogViewName, tabIndex = tabindex })
end

function UIManager.ShowDialog(viewName, params, tabIndex)
    if Local.LogModuals.UIManager then
        print("ShowDialog ==>>", viewName)
        printt(params)
    end
    -- 在showdialog之前隐藏其它窗口
    this.BeforeShowDialog()

    if tabIndex == nil then
        tabIndex = 1
    end
    local data = this.GetViewData(viewName)
    data.isDialog = true
    data.isInitedTabs = {} -- tab_name,true
    data.tabGroupStates = {}
    data.dialogViewName = viewName

    if _dialogStack:Count() > 0 then
        local lastViewName = _dialogStack:Peek()
        if lastViewName == viewName then
            return
        end
        HideLoadedView(lastViewName)
    else
        this.HideMainCityDlgs()
    end

    _dialogStack:Push(viewName)
    if Local.LogModuals.UIManager then
        printt(_dialogStack:ToTable())
    end

    if this.HasScript(viewName) then
        ShowLoading()
        if (this.HasMethod(viewName, "ShowDialog")) and not this.IsUIShowType(viewName, UIShowType.ShowImmediate) then
            this.Call(viewName, "ShowDialog", params)
        else
            this.Show(viewName, params)
        end
    else
        OnShow(viewName, params)
    end

    this.ShowTabByIndex(viewName, tabIndex, params)
    this.RefreshDlgdialog(viewName, data.tabIndex)

end

function UIManager.Hidedialog(viewName, isImmediate)
    if Local.LogModuals.UIManager then
        print("HideDialog ==>>", viewName)
    end
    if _dialogStack:Count() == 0 then
        return
    else
        local currentViewName = _dialogStack:Peek()
        if currentViewName ~= viewName then
            return
        end
    end
    local data = this.GetViewData(viewName)
    local tabGroup = this.gettabgroup(viewName, data.tabIndex)
    if tabGroup then
        this.HideTabByIndex(viewName, data.tabIndex)
    end
    if this.HasScript(viewName) then
        if (this.HasMethod(viewName, "HideDialog")) then
            this.Call(viewName, "HideDialog")
        else
            this.Hide(viewName)
        end
    end

    _dialogStack:Pop()

    if _dialogStack:Count() > 0 then
        local lastview_name = _dialogStack:Peek()
        ShowLoadedView(lastview_name)
    else
        this.ShowMaincityDlgs()
        if isImmediate and true == isImmediate then
            this.HideImmediate("dlgdialog")
        else
            this.Hide("dlgdialog")
        end
    end
    if Local.LogModuals.UIManager then
        printt(_dialogStack:ToTable())
    end
    --local NoviceGuideTrigger = require "noviceguide.noviceguide_trigger"
    --NoviceGuideTrigger.HideDialog(viewName)
end
function UIManager.CurrentDialogName()
    if _dialogStack:Count() == 0 then
        return "Stack Is Empty!"
    else
        return _dialogStack:Peek()
    end
end
function UIManager.CloseAllDialog()
    if _dialogStack:Count() > 0 then
        local lastViewName = _dialogStack:Peek()
        if (this.HasMethod(lastViewName, "HideDialog")) then
            this.Call(lastViewName, "HideDialog")
        else
            this.Hide(lastViewName)
        end
        _dialogStack:Clear()
        this.ShowMaincityDlgs()
        this.Hide("dlgdialog")
    end
end

------------------------------------------------------------------
---UI粒子系统操作方法
------------------------------------------------------------------
-- 播放粒子系统及子节点中粒子系统
function UIManager.PlayUIParticleSystem(go, callback)
    go:SetActive(false)
    go:SetActive(true)
    local particles = go:GetComponentsInChildren(UnityEngine.ParticleSystem)
    if particles and particles.Length ~= 0 and particles[1] then
        particles[1]:Stop(true)
        particles[1]:Play(true)
        if callback then
            table.insert(_playingParticleSystems, { particle = particles[1], callback = callback, time = 0 })
        end
    end
end
-- 停止粒子系统及子节点中粒子系统
function UIManager.StopUIParticleSystem(go)
    local particle = go:GetComponent("ParticleSystem")
    if particle then
        particle:Stop(true)
    end
    go:SetActive(false)
end
-- 遍历子节点中粒子系统
function UIManager.IsPlaying(go)
    local particles = go:GetComponentsInChildren(UnityEngine.ParticleSystem)
    for i = 1, particles.Length do
        local particle = particles[i]
        if particle.isPlaying then
            return true
        end
    end
    return false
end
-- 遍历子节点中粒子系统
function UIManager.IsStopped(go)
    local particles = go:GetComponentsInChildren(UnityEngine.ParticleSystem)
    for i = 1, particles.Length do
        local particle = particles[i]
        if not particle.isStopped then
            return false
        end
    end
    return true
end

return UIManager