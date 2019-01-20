--[[
json 格式做网络协议
class 字段顺序排列,则数据在json中也顺序排列
字段为kv时,数据可乱序,此时的key也是一种数据
借鉴M10项目
--]]
local Json = require('rapidjson')
local Event = Event
local GameEvent = GameEvent
---@type Game.NetworkManager
local Network = Game.Manager.NetworkMgr

---@class NetworkManager
local NetworkManager = {}
local networkEvt = Event.NewSimple('NetworkEvent')
---@type Game.NetworkChannel
local channel

---@param channel Game.NetworkChannel
local onChannelConnected = function(channel)
    printyellow(channel.Name, "正常连接到服务器")
end
---@param channel Game.NetworkChannel
local onChannelClosed = function(channel)
    printyellow(channel.Name, "Channel Closed")
end
---@param channel Game.NetworkChannel
---@param count number
local onMissHeartBeat = function(channel, count)
    printyellow(channel.Name, 'Miss Heart Beat', count)
end
local onProtocolError = function(msg)

end
---@param type int
---@param msg byte[]
local onReceive = function(type, data)
    local msg = Json.decode(data)
    printt(msg, type)
end

local function SecondUpdate()
    local t = { a = Mathf.Random(-10, -1), b = Mathf.Random(1, 10) }
    channel:Send(Mathf.Random(1, 100), Json.encode(t))
end

local ip = "192.168.50.90"
local port = 8686

function NetworkManager.Init()
    Network.OnNetworkConnected = onChannelConnected
    Network.OnNetworkClosed = onChannelClosed
    Network.OnNetworkMissHeartBeat = onMissHeartBeat

    channel = Network:CreateNetworkChannel("NetworkChannel")
    channel.NetworkReceive = onReceive
    channel.NetworkChannelConnected = onChannelConnected
    channel:Connect(ip, port)

    local secondTimer = Timer:new(SecondUpdate, 1, -1, false)
    secondTimer:Start()

    local t = { 1, 2, 3, 'nil', 4, 5 }
    local json = Json.encode(t)
    printyellow(json)
    local t1 = Json.decode(json)
    printyellow(dump(t1, "json"))
end

---@param msg table
function NetworkManager.Send(type, msg)
    local encode = Json.encode(msg)
    channel:Send(type, encode)
end

function NetworkManager.Destroy()
    Network:DestroyNetworkChannel(channel.Name)
end

return NetworkManager