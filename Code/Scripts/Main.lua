local require = require
require "Global"

local modules = require "Modules"
local gameEvent = GameEvent
local LogError = LogError

local Time = Time

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

local function Init()
    Util.Myxpcall(InitModule)
    printcolor("orange", 'lua framework init successful.')
    TEST()
    local demo = require "UIExample.UIDemo"
    Util.Myxpcall(demo.Init, demo)
end

--逻辑update
local function Update(deltaTime, unscaledDeltaTime)
    gameEvent.UpdateEvent:Trigger()
end
local function SecondUpdate(time)
    gameEvent.SecondUpdateEvent:Trigger()
end
local function LateUpdate()
    gameEvent.LateUpdateEvent:Trigger()
end
--物理update
local function FixedUpdate(fixedDeltaTime)
       gameEvent.FixedUpdateEvent:Trigger()
end

local function OnDestroy()
    local demo = require "UIExample.UIDemo"
    demo:OnDestroy()
    --local util = require('xlua.util')
    --util.print_func_ref_by_csharp()
end

--------------------------------------------------------
---------------------- 测试 ----------------------------

function TEST()
    local byte = string.byte
    local sub = string.sub
    local len = string.len

    local function JSHash(str)
        local l = len(str)
        local h = l
        local step = (l>>5) + 1
        for i=l,step,-step do
            h = (h~(h << 5) + byte(sub(str, i, i)) + (h >> 2))
        end
        return h
    end
    local function Hash(str)
        local seed = 131;
        local hash = 0;

        for i = 1, #str do
            hash = hash * seed + byte(sub(str, i, i))
        end
        return (hash & 0x7FFFFFFF)
    end
    --print(JSHash("0000000000000000000000000000000000"))
    --print(JSHash("f0l0l0w0m0e0n0t0w0i0t0t0e0r0?0:0)0"))
    --print(JSHash("x0x0x0x0x0x0x0x0x0x0x0x0x0x0x0x0x0"))
    -- output:-- 1777619995-- 1777619995-- 1777619995
    print(Hash"PositionSpeed")
    print(Hash"SpeedPosition")
    print(Hash"SpeedPosition")
end

Main = {
    Init = Init,
    Update = Update,
    SecondUpdate = SecondUpdate,
    LateUpdate = LateUpdate,
    FixedUpdate = FixedUpdate,
    OnDestroy = OnDestroy
}
