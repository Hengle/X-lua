---@type string
local name
---@type UI.Emoji.DlgEmoji
local fields
---@type Game.LuaWindow
local window

local function Show(param)

end
local function Refresh(param)

end
local function Hide()

end
local function Destroy()

end
local function Init(param)
    name, window, fields = unpack(param)

end

return {
    Init = Init,
    Show = Show,
    Refresh = Refresh,
    Hide = Hide,
    Destroy = Destroy,
}