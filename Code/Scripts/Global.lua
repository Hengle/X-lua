UnityEngine = CS.UnityEngine
Shader = UnityEngine.Shader
Animator = UnityEngine.Animator
Animation = UnityEngine.Animation
AnimationClip = UnityEngine.AnimationClip
AnimationEvent = UnityEngine.AnimationEvent
AnimationState = UnityEngine.AnimationState
AudioClip = UnityEngine.AudioClip
AudioSource = UnityEngine.AudioSource
Physics = UnityEngine.Physics
GameObject = UnityEngine.GameObject
Transform = UnityEngine.Transform
---@type UnityEngine.Application
Application = UnityEngine.Application
SystemInfo = UnityEngine.SystemInfo
Screen = UnityEngine.Screen
Camera = UnityEngine.Camera
Material = UnityEngine.Material
Renderer = UnityEngine.Renderer
WWW = UnityEngine.WWW
Input = UnityEngine.Input
KeyCode = UnityEngine.KeyCode
CharacterController = UnityEngine.CharacterController
SkinnedMeshRenderer = UnityEngine.SkinnedMeshRenderer
Rect        = UnityEngine.Rect
---@type UnityEngine.RuntimePlatform
RuntimePlatform = UnityEngine.RuntimePlatform

FairyGUI = CS.FairyGUI
EventContext  = FairyGUI.EventContext
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
---@type FairyGUI.Timers
Timers = FairyGUI.Timers
UIConfig = FairyGUI.UIConfig

Game = CS.Game
CSUtil = Game.Util
AppConst = Game.AppConst
LuaHelper = Game.LuaHelper
Interface = Game.Platform.Interface
ResMgr = Game.ResourceManager.Instance
SoundMgr = Game.SoundManager.Instance
NetworkMgr = Game.NetworkManager.Instance
LuaWindow = Game.LuaWindow

local require = require
require 'xlua.extend'
require 'xlua.coroutine'            --?
XUtil = require 'xlua.util'
XProfiler = require 'xlua.profiler'
XMemory = require 'xlua.memory'

Mathf		= require "UnityEngine.Mathf"
Vector3 	= require "UnityEngine.Vector3"
Quaternion	= require "UnityEngine.Quaternion"
Vector2		= require "UnityEngine.Vector2"
Vector4		= require "UnityEngine.Vector4"
Color		= require "UnityEngine.Color"
Ray			= require "UnityEngine.Ray"
Bounds		= require "UnityEngine.Bounds"
RaycastHit	= require "UnityEngine.RaycastHit"
Touch		= require "UnityEngine.Touch"
LayerMask	= require "UnityEngine.LayerMask"
Plane		= require "UnityEngine.Plane"
Time		= require "UnityEngine.Time"

require "Local"
-----------------------------------------------------------
require 'Common.Function'

Define = require "Define"
---@type Class
Class = require "Common.Class"
Util = require "Common.Util"
List = require 'Common.List'
Event = require "Common.Event"
Easing = require "Common.Easing"
GameEvent = require "Common.GameEvent"

require "Common.Timer"

unpack = table.unpack

IsEditor = Application.isEditor
IsAndroid = Application.platform == RuntimePlatform.Android
IsIPhone = Application.platform == RuntimePlatform.IPhonePlayer


