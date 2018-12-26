local name
local fields
local window

local time = 0
local coolTime = 6

local function Show(param)

end
local function Refresh(param)

end
local function Hide()

end
local function Destroy()

end
local function Update(dt)
    time = time - dt
    fields.Image_maskCircle.fillAmount = time / coolTime
    fields.Image_maskRect.fillAmount = time / coolTime
    fields.Label_CoolTime.text = math.ceil(time)
    if time > 0  then
        return
    end
    time = coolTime
end
local function Init(param)
    name, window, fields = unpack(param)
    print(#fields)
end

return {
    Init = Init,
    Show = Show,
    Refresh = Refresh,
    Hide = Hide,
    Destroy = Destroy,
    Update = Update,
}