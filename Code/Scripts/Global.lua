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
Debug = CS.UnityEngine.Debug

CSUtil = CS.Game.Util
AppConst = CS.Game.AppConst
LuaHelper = CS.Game.LuaHelper
Interface = CS.Game.Platform.Interface
ResMgr = CS.Game.ResourceManager.Instance
SoundMgr = CS.Game.SoundManager.Instance
NetworkMgr = CS.Game.NetworkManager.Instance

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

list = require 'System.list'
event = require 'System.events'

require 'System.coroutine'
require "Local"
-----------------------------------------------------------
require 'Function'

Define = require "Define"
Class = require "Common.Class"
Util = require "Common.Util"
Queue = require "Common.Queue"
Stack = require "Common.Stack"
GameEvent = require "Common.GameEvent"
--CfgMgr = require "Cfg.CfgManager"
--
----加载所以配置
--Util.Myxpcall(CfgMgr.Init)


