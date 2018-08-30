local event = event
local Time = Time

---@type _Event
UpdateEvent = event("Update")
---@type _Event
CoUpdateEvent = event("CoUpdate")
---@type _Event
LateUpdateEvent = event("LateUpdate")
---@type _Event
FixedUpdateEvent = event("FixedUpdate")

function PrintEvents()
    UpdateEvent:Dump()
    FixedUpdateEvent:Dump()
end

--逻辑update
function Update(deltaTime, unscaledDeltaTime)
    Time:SetDeltaTime(deltaTime, unscaledDeltaTime)
    UpdateEvent()
    CoUpdateEvent()
end
function LateUpdate()
    LateUpdateEvent()
end
--物理update
function FixedUpdate(fixedDeltaTime)
    Time:SetFixedDelta(fixedDeltaTime)
    FixedUpdateEvent()
end
