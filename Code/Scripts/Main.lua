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
    Util.Myxpcall(TEST)
    --local demo = require "UIExample.UIDemo"
    --Util.Myxpcall(demo.Init, demo)
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
        local step = (l >> 5) + 1
        for i = l, step, -step do
            h = (h ~ (h << 5) + byte(sub(str, i, i)) + (h >> 2))
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
    --local t1 = {'1', '2', '3', '4', '5', '6', '7', '8', '9', '10'}
    --local t2 = {'1', '23'}
    --print(Hash"positionrotationscale")
    --print(Hash(table.concat(t1, '.')))
    --print(Hash(table.concat(t2, '.')))

    local t1 = { 4, 1, 2, 5, 34, 3 }
    local t2 = { 4, 1, 2, 5, 34, 3 }

    local Filter = require('ECS.Filter')
    local f1 = Filter:new()
    local f2 = Filter:new()
    f1:All(2, 1, 3)
    f1:None(4)
    f1:Any(5)
    f2:All(1, 2, 3, f2:None(4), f2:Any(5))
    printyellow(f1:GetHashCode(), f2:GetHashCode())
    print('self.AllHandle -', f1.AllHandle == nil)
    print('self.AllHandle -', f2.AllHandle == nil)
    print('----self', table.concat(t1, ','))
    table.sort(t2)
    print('----lua', table.concat(t2, ','))
end

Main = {
    Init = Init,
    Update = Update,
    SecondUpdate = SecondUpdate,
    LateUpdate = LateUpdate,
    FixedUpdate = FixedUpdate,
    OnDestroy = OnDestroy
}



