local Stream = require("Cfg.DataStream")
local GetOrCreate = Util.GetOrCreate

local meta
meta= {}
meta.__index = meta
meta.class = 'Csv.AllType.AllClass'
meta.ConstString = 'Hello World'
meta.ConstFloat = 3.141527
GetOrCreate('Csv.AllType')['AllClass'] = meta
function Stream:GetCsvAllTypeAllClass()
	local o = {}
	setmetatable(o, Csv.AllType.AllClass)
	o.ID = self:GetInt()
	o.VarLong = self:GetLong()
	o.VarFloat = self:GetFloat()
	o.VarString = self:GetString()
	o.VarBool = self:GetBool()
	o.VarEnum = self:GetInt()
	o.VarClass = self:GetObject('CsvAllTypeSingleClass')
	o.VarListBase = self:GetList('String')
	o.VarListClass = self:GetList('CsvAllTypeSingleClass')
	o.VarListCardElem = self:GetList('String')
	o.VarDictBase = self:GetDict('Int', 'String')
	o.VarDictEnum = self:GetDict('Long', 'Int')
	o.VarDictClass = self:GetDict('String', 'CsvAllTypeSingleClass')
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.AllType.SingleClass'
GetOrCreate('Csv.AllType')['SingleClass'] = meta
function Stream:GetCsvAllTypeSingleClass()
	local o = {}
	setmetatable(o, Csv.AllType.SingleClass)
	o.Var1 = self:GetString()
	o.Var2 = self:GetFloat()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Character.Model'
GetOrCreate('Csv.Character')['Model'] = meta
function Stream:GetCsvCharacterModel()
	local o = {}
	setmetatable(o, Csv.Character.Model)
	o.Name = self:GetString()
	o.Level = self:GetInt()
	return o
end
GetOrCreate('Csv.AllType')['CardElement'] = {
	NULL = -9,
	Attack = 0,
	Extract = 1,
	Renounce = 2,
	Armor = 3,
	Control = 4,
	Cure = 5,
	Oneself = 6,
	Hand = 7,
	Brary = 8,
	Handack = 9,
}

return Stream
