using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class ActorConfig : CfgObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public readonly string ModelName;
		/// <summary>
		/// 基础模型名称
		/// <summary>
		public readonly string BaseModelName;
		/// <summary>
		/// 普通动作
		/// <summary>
		public readonly Dictionary<string, GeneralAction> GeneralActions = new Dictionary<string, GeneralAction>();
		/// <summary>
		/// 技能动作
		/// <summary>
		public readonly Dictionary<string, SkillAction> SkillActions = new Dictionary<string, SkillAction>();

		public ActorConfig(DataStream data)
		{
			this.ModelName = data.GetString();
			this.BaseModelName = data.GetString();
			for (int n = data.GetInt(); n-- > 0;)
			{
				string k = data.GetString();
				this.GeneralActions[k] = (GeneralAction)data.GetObject(data.GetString());
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				string k = data.GetString();
				this.SkillActions[k] = (SkillAction)data.GetObject(data.GetString());
			}
		}
	}
}
