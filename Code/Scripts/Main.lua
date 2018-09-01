local require = require

require "Global"

local modules = require "Modules"
local gameEvent = GameEvent

local Time = Time
Main = {}

local function InitModule()
    for _, name in ipairs(modules) do
        local module = require(name)
        module.Init()
    end
end

local sumTime = 0

local function A() 
    print("------", sumTime)
    sumTime = sumTime + 1
end

function Main.Init()
    InitModule()
    gameEvent.UpdateEvent.Add(A)
    gameEvent.LateUpdateEvent.Add(A)
    gameEvent.FixedUpdateEvent.Add(A)
    print('xlua init successful')
end

--逻辑update
function Main.Update(deltaTime, unscaledDeltaTime)
    print("--Update")
    Time:SetDeltaTime(deltaTime, unscaledDeltaTime)
    gameEvent.UpdateEvent.Trigger()
end
function Main.LateUpdate()
    gameEvent.LateUpdateEvent.Trigger()
end
--物理update
function Main.FixedUpdate(fixedDeltaTime)
    Time:SetFixedDelta(fixedDeltaTime)
    gameEvent.FixedUpdateEvent.Trigger()
end
