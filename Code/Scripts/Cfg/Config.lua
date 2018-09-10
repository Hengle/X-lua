local Stream = require("Cfg.DataStruct")
local cfgs = {}
for _, s in ipairs({
	{ name = 'AllClass', method = 'GetCsvAllTypeAllClass', index = 'ID', output = 'AllType/AllClass.data' },
	{ name = 'Model', method = 'GetCsvCharacterModel', index = 'Name', output = 'Character/Model.data' },
	{ name = 'ActorConfig', method = 'GetCsvSkillActorConfig', index = 'ModelName', output = 'Skill/ActorConfig.data' },
}) do
	local data = Stream.new(s.output)
	local cfg = {}
	while data:NextRow() do
		local value = data[s.method](data)
		local key = value[s.index]
		cfg[key] = value
	end
	cfgs[s.name] = cfg
end

return cfgs
