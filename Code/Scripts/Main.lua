local require = require

require "Global"

local modules = require "Modules"
local gameEvent = GameEvent
local LogError = LogError

local Time = Time
Main = {}

local function InitModule()
    for _, name in ipairs(modules) do
        local module = require(name)
        if module then
            module.Init()
        else
            LogError("module %s init fail!", name)
        end
    end
end

function Main.Init()
    InitModule()
    printcolor("orange", 'lua framework init successful.')

    local demo = require "UIExample.UIDemo"
    Util.Myxpcall(demo.Init, demo)
end

--逻辑update
function Update(deltaTime, unscaledDeltaTime)
    Time:SetDeltaTime(deltaTime, unscaledDeltaTime)
    gameEvent.UpdateEvent:Trigger()
end
function LateUpdate()
    gameEvent.LateUpdateEvent:Trigger()
end
--物理update
function FixedUpdate(fixedDeltaTime)
    Time:SetFixedDelta(fixedDeltaTime)
    gameEvent.FixedUpdateEvent:Trigger()
end

function OnDestroy()
    local demo = require "UIExample.UIDemo"
    demo:OnDestroy()
    --local util = require('xlua.util')
    --util.print_func_ref_by_csharp()
end

--------------------------------------------------------
---------------------- 测试 ----------------------------
function TEST()
    --local CfgMgr = require('Manager.CfgManager')
    --printt(CfgMgr.NameTable, 'NameTable')
    --local queue = Queue:new()
    --queue:Enqueue('1')
    --queue:Enqueue('2')
    --print("Queue Count:", queue:Count())
    --print("Queue Dequeue:", queue:Dequeue())
    --print("Queue Count:", queue:Count())
    --print("Queue Dequeue:", queue:Dequeue())
    --print("Queue Count:", queue:Count())
end