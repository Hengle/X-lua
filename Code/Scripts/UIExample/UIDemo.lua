local require = require

local TEST = CS.Test
local examples = {
    [1] = "Basics",
}
local index = 1

local UIDemo = {}
local example = nil

function UIDemo:Init()
    TEST.LoadAtlas()

    local module = string.format("UIExample.%s", examples[index])
    example = require(module)
    if example then
        example:Init()
    else
        printyellow(string.format("案例%s 初始化失败!", examples[index]))
    end
end

function UIDemo:OnDestroy()
    --example:OnDestroy()
end

return UIDemo