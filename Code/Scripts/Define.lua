--手写枚举定义
local CtrlNames = {
	Prompt = "PromptCtrl",
	Message = "MessageCtrl"
}

local PanelNames = {
	"PromptPanel",	
	"MessagePanel",
}

--协议类型--
local ProtocalType = {
	BINARY = 0,
	PB_LUA = 1,
	PBC = 2,
	SPROTO = 3,
}
--当前使用的协议类型--
local TestProtoType = ProtocalType.BINARY;

return {
	CtrlNames,
	PanelNames,
	ProtocalType,
	TestProtoType,
}

