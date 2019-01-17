local Event = Event
local GameEvent = GameEvent
---@type Game.NetworkManager
local Network = Game.Manager.NetworkMgr

---@class NetworkManager
local NetworkManager = {}
local networkEvt = Event.NewSimple('NetworkEvent')
---@type Game.NetworkChannel
local channel

local onChannelConnected = function(channel)
    printyellow(channel.Name .. "正常连接到服务器")
end
local onChannelClosed = function(channel)

end
local onMissHeartBeat = function(channel, count)

end
local onProtocolError = function(msg)

end
---@param type int
---@param msg byte[]
local onReceive = function(type, msg)
    printyellow(type, msg)
end

local ip = "192.168.50.90"
local port = 8686

function NetworkManager.Init()
    --Network.OnNetworkConnected = onChannelConnected
    --Network.OnNetworkClosed = onChannelClosed
    --Network.OnNetworkMissHeartBeat = onMissHeartBeat

    --channel = Network:CreateNetworkChannel('NetworkChannel')
    --channel.NetworkReceive = onReceive
    --channel:Connect(ip, port)
    --GameEvent.DestroyEvent:Add(NetworkManager.Destroy)
end

---@param msg table
function NetworkManager.Send(msg)

end

function NetworkManager.Destroy()
    Network:DestroyNetworkChannel(channel.Name)
end

return NetworkManager