local pairs = pairs
local error = error
local type = type
local insert = table.insert
local format = string.format

local Class = Class
local Util = Util
local Event = Class:new()

function Event:ctor(name)
    self.name = name or "__unknown__"
    self.eventMap = {}
    self.statusMap = {}
end

function Event:Add(event, handler)
    if not event or type(event) ~= "string" then
        error("[Event.Add]event has to be string, " .. type(event) .. " not right.")
    end
    local handlers = self.eventMap[event]
    if not handlers then
        handlers = {}
        self.eventMap[event] = handlers
    end
    insert(handlers, handler)
    return function()
        self:Remove(event, handler)
    end
end

function Event:Remove(event, handler)
    --print("[Event:Remove]remove event handler.", self.name, event)
    if self.eventMap[event] then
        local handlers = self.eventMap[event]
        for k, v in pairs(handlers) do
            if v == handler then
                self.eventMap[event][k] = nil
            end
        end
    else
        print("[Event:Remove]event no handler.", self.name)
    end
end

function Event:Destroy(event)
    if self.eventMap[event] then
        self.eventMap[event] = nil
    else
        print("[Event:Destroy]eventlib no event", self.name, event)
    end
end

function Event:AddStatusListener(listener)
    self.statusMap[listener.name] = listener
end

function Event:Trigger(event, ...)
    local events = self.eventMap[event]
    if not events then
        --error("[Event:Trigger]eventlib no event:" .. event)
        return
    end
    for _, handler in ipairs(events) do
        --print("[Event:Trigger] ", self.name, target, handler)
        if self.statusMap[event] then
            self.statusMap[event]:BeginSample()
        end
        Util.Myxpcall(handler, ...)
        if self.statusMap[event] then
            self.statusMap[event]:EndSample()
        end
    end
end

function Event:Dump(event)
    local r = {}
    if event then
        if self.eventMap[event] then
            insert(r,format("%s\t\t\t\t%d", event, #self.eventMap[event]))
        else
            printcolor("eventlib no event", event)
        end
    else
        for k, v in pairs(self.eventMap) do
            insert(r,format("%s\t\t\t\t%d", k, #v))
        end
    end
    print('dump event list..', '\n' .. table.concat(r, '\n'))
end

local global = Event:new("__global")
-- 如果不指定事件名,则创建一个匿名事件
function Event:NewSimple(name)
    name = name or {}
    return {
        Add               = function(self, handler)
            return global:Add(name, handler)
        end,
        Remove            = function(self, event, handler)
            global:Remove(event, handler)
        end,
        Trigger           = function(self, ...)
            global:Trigger(name, ...)
        end,
        AddStatusListener = function(self, listener)
            global:AddStatusListener(listener)
        end,
        Destroy           = function(self)
            global:Destroy(name)
        end,
        Dump              = function(self, event)
            global:Dump(event)
        end,
        name              = name,
    }
end

return Event