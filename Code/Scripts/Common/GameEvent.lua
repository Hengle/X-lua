local Event = event
local Time = Time


local UpdateEvent = Event.NewSimple("Update")
local CoUpdateEvent = Event.NewSimple("CoUpdate")
local LateUpdateEvent = Event.NewSimple("LateUpdate")
local FixedUpdateEvent = Event.NewSimple("FixedUpdate")

local function Dump()
    Event.Dump()
end

return {
    Dump = Dump,
    UpdateEvent = UpdateEvent,
    CoUpdateEvent = CoUpdateEvent,
    LateUpdateEvent = LateUpdateEvent,
    FixedUpdateEvent = FixedUpdateEvent,
}