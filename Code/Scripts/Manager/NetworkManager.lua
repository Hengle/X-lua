local NetworkMgr = NetworkMgr
local Event = Event

---@class NetworkManager
local NetworkManager = {}
local networkEvt = Event.NewSimple('NetworkEvent')
---@type Game.NetworkChannel
local network

local onChannelConnected = function(channel, userData)

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

local ip = "127.0.0.1"
local port = 86868

function NetworkManager.Init()
    --NetworkMgr.NetworkConnected = onChannelConnected
    --NetworkMgr.NetworkClosed = onChannelClosed
    --NetworkMgr.NetworkMissHeartBeat = onMissHeartBeat

    local channel = NetworkMgr:CreateNetworkChannel('NetworkChannel')
    network = channel
    --channel.NetworkReceive = onReceive
    channel:Connect(ip, port)
end

---@param msg table
function NetworkManager.Send(msg)

end

function NetworkManager.Destroy()
    --network:Dispose()
    --NetworkMgr:Dispose()
end

return NetworkManager