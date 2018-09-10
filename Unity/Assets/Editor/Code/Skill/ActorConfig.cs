using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class ActorConfig : CfgObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public readonly string ModelName;
		/// <summary>
		/// 模型分组类型
		/// <summary>
		public readonly int GroupType;
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
			this.GroupType = data.GetInt();
			this.BaseModelName = data.GetString();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.GeneralActions.Add(new GeneralAction(data));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.SkillActions.Add((SkillAction)data.GetObject(data.GetString()));
			}
		}
	}
}
