FairyGUI = CS.FairyGUI
EventContext = FairyGUI.EventContext
EventListener = FairyGUI.EventListener
EventDispatcher = FairyGUI.EventDispatcher
InputEvent = FairyGUI.InputEvent
NTexture = FairyGUI.NTexture
Container = FairyGUI.Container
Image = FairyGUI.Image
Stage = FairyGUI.Stage
Controller = FairyGUI.Controller
---@type FairyGUI.GObject
GObject = FairyGUI.GObject
---@type FairyGUI.GGraph
GGraph = FairyGUI.GGraph
---@type FairyGUI.GGroup
GGroup = FairyGUI.GGroup
---@type FairyGUI.GImage
GImage = FairyGUI.GImage
---@type FairyGUI.GLoader
GLoader = FairyGUI.GLoader
PlayState = FairyGUI.PlayState
GMovieClip = FairyGUI.GMovieClip
TextFormat = FairyGUI.TextFormat
---@type FairyGUI.GTextField
GTextField = FairyGUI.GTextField
GRichTextField = FairyGUI.GRichTextField
GTextInput = FairyGUI.GTextInput
---@type FairyGUI.GComponent
GComponent = FairyGUI.GComponent
---@type FairyGUI.GList
GList = FairyGUI.GList
---@type FairyGUI.GRoot
GRoot = FairyGUI.GRoot          ---- 禁止其他模块使用GRoot
---@type FairyGUI.GLabel
GLabel = FairyGUI.GLabel
---@type FairyGUI.GButton
GButton = FairyGUI.GButton
GComboBox = FairyGUI.GComboBox
GProgressBar = FairyGUI.GProgressBar
GSlider = FairyGUI.GSlider
PopupMenu = FairyGUI.PopupMenu
ScrollPane = FairyGUI.ScrollPane
Transition = FairyGUI.Transition
---@type FairyGUI.UIPackage
UIPackage = FairyGUI.UIPackage
---@type FairyGUI.Window
Window = FairyGUI.Window
---@type FairyGUI.DragDropManager
DragDropManager = FairyGUI.DragDropManager
GObjectPool = FairyGUI.GObjectPool
Relations = FairyGUI.Relations
RelationType = FairyGUI.RelationType
UIPanel = FairyGUI.UIPanel
UIPainter = FairyGUI.UIPainter
TypingEffect = FairyGUI.TypingEffect
UIConfig = FairyGUI.UIConfig

local setmetatable = setmetatable
local getmetatable = getmetatable
local rawset = rawset
local rawget = rawget

local this = {}
local Delegates = {}
this.Delegates = Delegates
setmetatable(Delegates, { __mode = "k" })

local evtCallback1 = FairyGUI.EventCallback1

local function GetDelegate(func, obj, createIfNone, delegateType)
    local mapping
    if obj ~= nil then
        mapping = obj.Delegates
        if mapping == nil then
            mapping = {}
            setmetatable(mapping, { __mode = "k" })
            obj.Delegates = mapping
        end
    else
        mapping = this.Delegates
    end

    local delegate = mapping[func]
    if createIfNone and delegate == nil then
        local realFunc
        if obj ~= nil then
            realFunc = function(context)
                func(obj, context)
            end
        else
            realFunc = func
        end
        delegateType = delegateType or evtCallback1
        delegate = delegateType(realFunc)

        mapping[func] = delegate
    end

    return delegate
end

local EventListener_mt = getmetatable(EventListener)
local oldListenerAdd = rawget(EventListener_mt, 'Add')
local oldListenerRemove = rawget(EventListener_mt, 'Remove')
local oldListenerSet = rawget(EventListener_mt, 'Set')
local oldListenerAddCapture = rawget(EventListener_mt, 'AddCapture')
local oldListenerRemoveCapture = rawget(EventListener_mt, 'RemoveCapture')

local function AddListener(listener, func, obj)
    local delegate = GetDelegate(func, obj, true)
    oldListenerAdd(listener, delegate)
end

local function RemoveListener(listener, func, obj)
    local delegate = GetDelegate(func, obj, false)
    if delegate ~= nil then
        oldListenerRemove(listener, delegate)
    end
end

local function SetListener(listener, func, obj)
    if func == nil then
        oldListenerSet(listener, nil)
    else
        local delegate = GetDelegate(func, obj, true)
        oldListenerSet(listener, delegate)
    end
end

local function AddListenerCapture(listener, func, obj)
    local delegate = GetDelegate(func, obj, true)
    oldListenerAddCapture(listener, delegate)
end

local function RemoveListenerCapture(listener, func, obj)
    local delegate = GetDelegate(func, obj, false)
    if delegate ~= nil then
        oldListenerRemoveCapture(listener, delegate)
    end
end

rawset(EventListener_mt, 'Add', AddListener)
rawset(EventListener_mt, 'Remove', RemoveListener)
rawset(EventListener_mt, 'Set', SetListener)
rawset(EventListener_mt, 'AddCapture', AddListenerCapture)
rawset(EventListener_mt, 'RemoveCapture', RemoveListenerCapture)


