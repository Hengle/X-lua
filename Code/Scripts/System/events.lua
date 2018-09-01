local type = type
local error = error
local EventLib = require "System.eventlib"

---@class Event
local Event = {}
local events = {}

---@return { event.disconnect }可断开当前监听
function Event.AddListener(event, handler)
    if not event or type(event) ~= "string" then
        error("event parameter in addlistener function has to be string, " .. type(event) .. " not right.")
    end
    if not handler or type(handler) ~= "function" then
        error("handler parameter in addlistener function has to be function, " .. type(handler) .. " not right")
    end

    if not events[event] then
        --create the Event with name
        events[event] = EventLib:new(event)
    end

    --conn this handler
    return events[event]:connect(handler)
end

function Event.Trigger(event, ...)
    if  events[event] then
        error("brocast " .. event .. " has no event.")
    else
        events[event]:fire(...)
    end
end

function Event.RemoveListener(event, handler)
    if not events[event] then
        error("remove " .. event .. " has no event.")
    else
        events[event]:disconnect(handler)
    end
end

function Event.Dump()
    local r = {}
    for k, v in pairs(events) do
        r[k] = v:ConnectionCount()
    end
    print('dump event list..\n', table.concat(r, '\n'))
end

function Event.NewSimple(name)
    if not name then
        error("event name can't nil")
    end
    return {
        Add = function(func)
            return Event.AddListener(name, func)
        end,
        Remove = function(func)
            Event.RemoveListener(name, func)
        end,
        Trigger = function(...)
            Event.Trigger(name, ...)
        end,
        name = name,
    }
end

return Event