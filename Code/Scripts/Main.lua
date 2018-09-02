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
    print('lua framework init successful.')
end

--逻辑update
function Main.Update(deltaTime, unscaledDeltaTime)
    Time:SetDeltaTime(deltaTime, unscaledDeltaTime)
    gameEvent.UpdateEvent:Trigger()
end
function Main.LateUpdate()
    gameEvent.LateUpdateEvent:Trigger()
end
--物理update
function Main.FixedUpdate(fixedDeltaTime)
    Time:SetFixedDelta(fixedDeltaTime)
    gameEvent.FixedUpdateEvent:Trigger()
end
