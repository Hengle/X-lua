local Stream = require("Cfg.DataStream")
local find = string.find
local sub = string.sub

local function GetOrCreate(namespace)
	local t = _G
	local idx = 1
	while true do
		local start, ends = find(namespace, '.', idx, true)
		local subname = sub(namespace, idx, start and start - 1)
		local subt = t[subname]
		if not subt then
			subt = {}
			t[subname] = subt
		end
		t = subt
		if start then
			idx = ends + 1
		else
			return t
		end
	end
end

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
	o.VarListClass = self:GetList('CsvSingleClass')
	o.VarListCardElem = self:GetList('String')
	o.VarDictBase = self:GetDict('Int', 'String')
	o.VarDictEnum = self:GetDict('Long', 'Int')
	o.VarDictClass = self:GetDict('String', 'CsvSingleClass')
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
meta.class = 'Csv.Skill.ModelActions'
GetOrCreate('Csv.Skill')['ModelActions'] = meta
function Stream:GetCsvSkillModelActions()
	local o = {}
	setmetatable(o, Csv.Skill.ModelActions)
	o.ModelName = self:GetString()
	o.BaseModelName = self:GetString()
	o.NormalActions = self:GetList('CsvNormalAction')
	o.SkillActions = self:GetList('CsvSkillAction')
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.NormalAction'
GetOrCreate('Csv.Skill')['NormalAction'] = meta
function Stream:GetCsvSkillNormalAction()
	local o = {}
	setmetatable(o, Csv.Skill.NormalAction)
	o.ActionName = self:GetString()
	o.ActionSource = self:GetInt()
	o.OtherModelName = self:GetString()
	o.ActionFile = self:GetString()
	o.PreActionFile = self:GetString()
	o.PostActionFile = self:GetString()
	o.ActionSpeed = self:GetFloat()
	o.LoopTimes = self:GetInt()
	o.EffectId = self:GetInt()
	o.Actions = self:GetList('CsvAction')
	o.Effects = self:GetList('CsvEffectGroup')
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.SkillAction'
meta.EXPIRE_TIME = 1
GetOrCreate('Csv.Skill')['SkillAction'] = meta
function Stream:GetCsvSkillSkillAction()
	local o = {}
	setmetatable(o, Csv.Skill.SkillAction)
	o.SkillExpireTime = self:GetFloat()
	o.SkillEndTime = self:GetFloat()
	o.NeedTarget = self:GetBool()
	o.CanInterrupt = self:GetBool()
	o.SkillRange = self:GetFloat()
	o.CanShowSkillRange = self:GetBool()
	o.CanRotate = self:GetBool()
	o.CanMove = self:GetBool()
	o.StartMoveTime = self:GetFloat()
	o.EndMoveTime = self:GetFloat()
	o.RelateType = self:GetInt()
	o.HitPoints = self:GetList('CsvHitPointGroup')
	o.HitZones = self:GetList('CsvHitZone')
	o.BeAttackEffects = self:GetList('CsvBeAttackEffect')
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.HitZone'
GetOrCreate('Csv.Skill')['HitZone'] = meta
function Stream:GetCsvSkillHitZone()
	local o = {}
	setmetatable(o, Csv.Skill.HitZone)
	o.Id = self:GetInt()
	o.Sharp = self:GetInt()
	o.Zoffset = self:GetFloat()
	o.Xlength = self:GetFloat()
	o.BottomHeight = self:GetFloat()
	o.TopHeight = self:GetFloat()
	o.Zlength = self:GetFloat()
	o.YAngle = self:GetFloat()
	o.YRotation = self:GetFloat()
	o.MaxTarget = self:GetInt()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.BeAttackEffect'
GetOrCreate('Csv.Skill')['BeAttackEffect'] = meta
function Stream:GetCsvSkillBeAttackEffect()
	local o = {}
	setmetatable(o, Csv.Skill.BeAttackEffect)
	o.Id = self:GetInt()
	o.Curve = self:GetInt()
	o.DefencerAction = self:GetString()
	o.DefencerEffectId = self:GetInt()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.EffectGroup'
GetOrCreate('Csv.Skill')['EffectGroup'] = meta
function Stream:GetCsvSkillEffectGroup()
	local o = {}
	setmetatable(o, Csv.Skill.EffectGroup)
	o.Id = self:GetInt()
	o.Name = self:GetString()
	o.Actions = self:GetList('CsvAction')
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.HitPointGroup'
GetOrCreate('Csv.Skill')['HitPointGroup'] = meta
function Stream:GetCsvSkillHitPointGroup()
	local o = {}
	setmetatable(o, Csv.Skill.HitPointGroup)
	o.Id = self:GetInt()
	o.Name = self:GetString()
	o.Attacks = self:GetList('CsvAttack')
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.Action'
GetOrCreate('Csv.Skill')['Action'] = meta
function Stream:GetCsvSkillAction()
	local o = {}
	setmetatable(o, Csv.Skill.Action)
	o.Timeline = self:GetFloat()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.Attack'
GetOrCreate('Csv.Skill')['Attack'] = meta
function Stream:GetCsvSkillAttack()
	local o = {}
	setmetatable(o, Csv.Skill.Attack)
	o.Id = self:GetInt()
	o.HitZoneId = self:GetInt()
	o.BeAttackEffectId = self:GetInt()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.ShakeScreen'
GetOrCreate('Csv.Skill')['ShakeScreen'] = meta
function Stream:GetCsvSkillShakeScreen()
	local o = {}
	setmetatable(o, Csv.Skill.ShakeScreen)
	o.Type = self:GetString()
	o.Frequency = self:GetInt()
	o.FrequencyDuration = self:GetFloat()
	o.FrequencyAtten = self:GetFloat()
	o.Amplitude = self:GetFloat()
	o.AmplitudeAtten = self:GetFloat()
	o.Life = self:GetFloat()
	o.MinRange = self:GetFloat()
	o.MaxRange = self:GetFloat()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.Movement'
GetOrCreate('Csv.Skill')['Movement'] = meta
function Stream:GetCsvSkillMovement()
	local o = {}
	setmetatable(o, Csv.Skill.Movement)
	o.Type = self:GetInt()
	o.Duration = self:GetFloat()
	o.Speed = self:GetFloat()
	o.Acceleration = self:GetFloat()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.ParticleEffect'
GetOrCreate('Csv.Skill')['ParticleEffect'] = meta
function Stream:GetCsvSkillParticleEffect()
	local o = {}
	setmetatable(o, Csv.Skill.ParticleEffect)
	o.Id = self:GetInt()
	o.Type = self:GetInt()
	o.FadeOutTime = self:GetFloat()
	o.Path = self:GetString()
	o.Life = self:GetFloat()
	o.FollowDirection = self:GetBool()
	o.FollowBeAttackedDirection = self:GetBool()
	o.Scale = self:GetFloat()
	o.CasterBindType = self:GetInt()
	o.FollowBoneDirection = self:GetBool()
	o.TargetBindType = self:GetInt()
	o.InstanceTraceType = self:GetInt()
	o.WorldOffsetX = self:GetFloat()
	o.WorldOffsetY = self:GetFloat()
	o.WorldOffsetZ = self:GetFloat()
	o.WorldRotateX = self:GetFloat()
	o.WorldRotateY = self:GetFloat()
	o.WorldRotateZ = self:GetFloat()
	o.BonePostionX = self:GetFloat()
	o.BonePostionY = self:GetFloat()
	o.BonePostionZ = self:GetFloat()
	o.BoneRotationX = self:GetFloat()
	o.BoneRotationY = self:GetFloat()
	o.BoneRotationZ = self:GetFloat()
	o.BoneScaleX = self:GetFloat()
	o.BoneScaleY = self:GetFloat()
	o.BoneScaleZ = self:GetFloat()
	o.BoneName = self:GetString()
	o.TraceTime = self:GetFloat()
	o.AlignType = self:GetInt()
	o.IsPoolDestroy = self:GetBool()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.SoundEffect'
GetOrCreate('Csv.Skill')['SoundEffect'] = meta
function Stream:GetCsvSkillSoundEffect()
	local o = {}
	setmetatable(o, Csv.Skill.SoundEffect)
	o.Probability = self:GetFloat()
	o.VolumeMin = self:GetFloat()
	o.VolumeMax = self:GetFloat()
	o.PathList = self:GetList('String')
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.SpawnObject'
GetOrCreate('Csv.Skill')['SpawnObject'] = meta
function Stream:GetCsvSkillSpawnObject()
	local o = {}
	setmetatable(o, Csv.Skill.SpawnObject)
	o.Id = self:GetFloat()
	o.SpawnType = self:GetFloat()
	o.Life = self:GetFloat()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.TraceObject'
meta.BODY_CORRECT = 0.7
meta.HEAD_CORRECT = 1.3
GetOrCreate('Csv.Skill')['TraceObject'] = meta
function Stream:GetCsvSkillTraceObject()
	local o = {}
	setmetatable(o, Csv.Skill.TraceObject)
	o.EffectId = self:GetInt()
	o.IsTraceTarget = self:GetBool()
	o.TraceCurveId = self:GetInt()
	o.OffsetX = self:GetFloat()
	o.OffsetY = self:GetFloat()
	o.OffsetZ = self:GetFloat()
	o.TraceType = self:GetInt()
	o.CasterBindType = self:GetInt()
	o.TargetBindType = self:GetInt()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.FlyWeapon'
GetOrCreate('Csv.Skill')['FlyWeapon'] = meta
function Stream:GetCsvSkillFlyWeapon()
	local o = {}
	setmetatable(o, Csv.Skill.FlyWeapon)
	o.BulletRadius = self:GetFloat()
	o.PassBody = self:GetBool()
	o.BeAttackEffectId = self:GetInt()
	return o
end
meta= {}
meta.__index = meta
meta.class = 'Csv.Skill.Bomb'
GetOrCreate('Csv.Skill')['Bomb'] = meta
function Stream:GetCsvSkillBomb()
	local o = {}
	setmetatable(o, Csv.Skill.Bomb)
	o.Id = self:GetInt()
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
GetOrCreate('Csv.Skill')['ActionSourceType'] = {
	NULL = -9,
	SelfModel = 0,
	OtherModel = 1,
}
GetOrCreate('Csv.Skill')['SkillTargetType'] = {
	NULL = -9,
	Enemy = 0,
	Teammate = 1,
	Self = 2,
}
GetOrCreate('Csv.Skill')['SkillRelateType'] = {
	NULL = -9,
	Self = 0,
	Target = 1,
}
GetOrCreate('Csv.Skill')['HitSharpType'] = {
	NULL = -9,
	Cube = 0,
	Cylinder = 1,
	Trangle = 2,
}
GetOrCreate('Csv.Skill')['ShakeType'] = {
	NULL = -9,
	Horizontal = 0,
	Vertical = 1,
	Mix = 2,
}
GetOrCreate('Csv.Skill')['MoveType'] = {
	NULL = -9,
	MoveBack = 0,
	MoveToTarget = 1,
	MoveInDirection = 2,
}
GetOrCreate('Csv.Skill')['EffectType'] = {
	NULL = -9,
	Stand = 0,
	Follow = 1,
	Trace = 2,
	TracePos = 3,
	BindToCamera = 4,
	UIStand = 5,
}
GetOrCreate('Csv.Skill')['EffectAlignType'] = {
	NULL = -9,
	None = 0,
	LeftTop = 1,
	Left = 2,
	LeftBottom = 3,
	Top = 4,
	Center = 5,
	Bottom = 6,
	RightTop = 7,
	Right = 8,
	RightBottom = 9,
}
GetOrCreate('Csv.Skill')['SpawnType'] = {
	NULL = -9,
	FlyWeapon = 0,
	Bomb = 1,
	Object = 2,
}
GetOrCreate('Csv.Skill')['TraceType'] = {
	NULL = -9,
	Fly = 0,
	Fixed = 1,
}
GetOrCreate('Csv.Skill')['TraceBindType'] = {
	NULL = -9,
	Body = 0,
	Head = 1,
	Foot = 2,
}

return Stream
