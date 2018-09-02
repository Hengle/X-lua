local Event = Event
local Time = Time


local UpdateEvent = Event:NewSimple("Update")
local CoUpdateEvent = Event:NewSimple("CoUpdate")
local LateUpdateEvent = Event:NewSimple("LateUpdate")
local FixedUpdateEvent = Event:NewSimple("FixedUpdate")

return {
    UpdateEvent = UpdateEvent,
    CoUpdateEvent = CoUpdateEvent,
    LateUpdateEvent = LateUpdateEvent,
    FixedUpdateEvent = FixedUpdateEvent,
}