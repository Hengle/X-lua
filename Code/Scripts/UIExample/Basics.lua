---@type ExampleBasics
local Basics = {}
---@type FairyGUI.GComponent
local mainCom = nil
---@type FairyGUI.GComponent
local container = nil
---@type FairyGUI.Controller
local ctrl = nil
---@type FairyGUI.GButton
local btnBack = nil
local dlgTable = {}
local package = "Basics"

-----------------严重问题:移除对象,未释放委托!除非指明要释放资源

function Basics:Init()
    local main = UIPackage.CreateObject(package, "Main")
    mainCom = GRoot.inst:AddChild(main).asCom
    Basics:Show(mainCom)
end

---@param context FairyGUI.EventContext
local function SetButton(context)
    ---@type FairyGUI.GButton
    local button = context.sender
    printyellow("--->>>", button.name)
    local type = button.name:sub(5)
    ---@type FairyGUI.GComponent
    local dlg = dlgTable[type]
    if not dlg then
        dlg = UIPackage.CreateObject(package, "Demo_" .. type).asCom
        dlgTable[type] = dlg
    end

    container:RemoveChildren(0, -1, true)
    container:AddChild(dlg)
    ctrl.selectedIndex = 1
    btnBack.visible = true

    if type == "Text" then
        local rich = dlg:GetChild("n12").asRichTextField
        rich.onClickLink:Add(function (context)
            local rich = context.sender
            rich.text = "[img]ui://Basics/pet[/img][color=#FF0000]点击链接触发事件[/color]：" .. context.data
        end)
        local input = dlg:GetChild("n22").asTextInput
        local result = dlg:GetChild("n24").asTextField
        local inputBtn = dlg:GetChild("n25").asButton
        inputBtn.onClick:Add(function ()
            result.text = input.text
        end)
    end
end

---@param main FairyGUI.GComponent
function Basics:Show(main)
    container = main:GetChild("container").asCom
    ctrl = main:GetController("c1")
    ctrl.selectedIndex = 0
    btnBack = main:GetChild("btn_Back")
    btnBack.visible = false
    btnBack.onClick:Add(function()
        ctrl.selectedIndex = 0
        btnBack.visible = false
    end)

    local num = main.numChildren
    for i = 1, num do
        local child = main:GetChildAt(i - 1)
        if child.group ~= nil and child.group.name == "btns" then
            child.onClick:Add(SetButton)
        end
    end
end

function Basics:OnDestroy()
    mainCom:Dispose()
end

return Basics