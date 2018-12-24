local Event = Event

local UpdateEvent = Event:NewSimple("Update")
local SecondUpdateEvent = Event:NewSimple("SecondUpdate")
local LateUpdateEvent = Event:NewSimple("LateUpdate")
local FixedUpdateEvent = Event:NewSimple("FixedUpdate")
local ECSEvent = Event:new('ECS')

return {
    UpdateEvent = UpdateEvent,
    SecondUpdateEvent = SecondUpdateEvent,
    LateUpdateEvent = LateUpdateEvent,
    FixedUpdateEvent = FixedUpdateEvent,
    ECSEvent = ECSEvent,
}