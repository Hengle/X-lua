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
		public readonly List<GeneralAction> GeneralActions = new List<GeneralAction>();
		/// <summary>
		/// 技能动作
		/// <summary>
		public readonly List<SkillAction> SkillActions = new List<SkillAction>();

		public ActorConfig(DataStream data)
		{
			this.ModelName = data.GetString();
			this.BaseModelName = data.GetString();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.GeneralActions.Add((GeneralAction)data.GetObject(data.GetString()));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.SkillActions.Add((SkillAction)data.GetObject(data.GetString()));
			}
		}
	}
}
