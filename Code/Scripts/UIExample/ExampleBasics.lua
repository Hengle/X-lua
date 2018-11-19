---@type ExampleBasics
local ExampleBasics = {}
---@type FairyGUI.GComponent
local root = nil

function ExampleBasics:Init()
    local package = UIPackage.CreateObject("DlgBasics", "DlgBasics")
    root = GRoot.inst:AddChild(package).asCom
    ExampleBasics:Show(root)
end

---@param com FairyGUI.GComponent
function ExampleBasics:Show(com)
    local win = com:GetChild("Window")
    local list= win:GetChild("List").asList
    for i = 1, list.numChildren do
        ExampleBasics:SetButton(list:GetChildAt(i - 1).asButton)
    end
end

local function OnClick()
    printyellow("----->OnClick")
end

---@param button FairyGUI.GButton
function ExampleBasics:SetButton(button)
    local labName = button:GetChild("labName")
    printyellow("GLabel - ", labName.text)
    button.onClick:Add(OnClick)
end

function ExampleBasics:OnDestroy()
    root:Dispose()
end

return ExampleBasics