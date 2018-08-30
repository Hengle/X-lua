-- 1 表示需要递归导出子GameObject的fields
-- 2 表示只导到当前GameObject,不再导出子GameObject
-- 直接解析指定组件,非GameObject
local prefixs = {
    Text = 1,
    Image = 1,
    RawImage = 1,
    Button = 1,
    Toggle = 1,
    Slider = 1,
    Panel = 1,
    ScrollView = 1,
    InputField = 1,

    Grid = 1,
    List = 2,
}

local function CollectExportedGameObject(transform, sets)
    for i = 0, transform.childCount - 1 do

        local childT = transform:GetChild(i)
        ---@type UnityEngine.GameObject
        local child = childT.gameObject
        local name = child.name

        local _, pos = name:find('_', 2, true)
        local isGroup = false
        if pos and pos > 1 then

            local prefix = name:sub(1, pos - 1)
            local ftype = prefixs[prefix]
            if ftype then
                if sets[name] then
                    --		printyellow("warn:" .. name .. " dumplicate! skip!")
                else
                    local com = child:GetComponent(prefix)
                    if com then
                        sets[name] = com
                    end
                    isGroup = (ftype == 2)
                end
            end
        end
        if not isGroup then
            CollectExportedGameObject(childT, sets)
        end
    end
end

local function ExportFields(gameObject)
    local fields = {}
    CollectExportedGameObject(gameObject.transform, fields)
    return fields
end

return {
    ExportFields = ExportFields,
}
