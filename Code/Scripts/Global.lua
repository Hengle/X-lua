Shader = CS.UnityEngine.Shader
Animator = CS.UnityEngine.Animator
Animation = CS.UnityEngine.Animation
AnimationClip = CS.UnityEngine.AnimationClip
AnimationEvent = CS.UnityEngine.AnimationEvent
AnimationState = CS.UnityEngine.AnimationState
AudioClip = CS.UnityEngine.AudioClip
AudioSource = CS.UnityEngine.AudioSource
Physics = CS.UnityEngine.Physics
GameObject = CS.UnityEngine.GameObject
Transform = CS.UnityEngine.Transform
Application = CS.UnityEngine.Application
SystemInfo = CS.UnityEngine.SystemInfo
Screen = CS.UnityEngine.Screen
Camera = CS.UnityEngine.Camera
Material = CS.UnityEngine.Material
Renderer = CS.UnityEngine.Renderer
WWW = CS.UnityEngine.WWW
Input = CS.UnityEngine.Input
KeyCode = CS.UnityEngine.KeyCode
CharacterController = CS.UnityEngine.CharacterController
SkinnedMeshRenderer = CS.UnityEngine.SkinnedMeshRenderer

CSUtil = CS.Game.Util
AppConst = CS.Game.AppConst
LuaHelper = CS.Game.LuaHelper
Interface = CS.Game.Platform.Interface
ResMgr = CS.Game.ResourceManager.Instance
SoundMgr = CS.Game.SoundManager.Instance
NetworkMgr = CS.Game.NetworkManager.Instance
EasyTouch = CS.HedgehogTeam.EasyTouch.EasyTouch
Gesture = CS.HedgehogTeam.EasyTouch.Gesture
ECTInput = CS.HedgehogTeam.EasyTouch.ECTInput

local require = require
require 'System.string'
require 'System.table'

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

Profiler = require 'System.profiler'
Memory = require 'System.memory'

require 'System.coroutine'
require "Local"
-----------------------------------------------------------
require 'Function'

Define = require "Define"
Class = require "Common.Class"
Util = require "Common.Util"
List = require 'Common.List'
Queue = require "Common.Queue"
Stack = require "Common.Stack"
Event = require "Common.Event"
GameEvent = require "Common.GameEvent"


