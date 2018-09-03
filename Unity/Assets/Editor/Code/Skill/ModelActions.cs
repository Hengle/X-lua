using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class ModelActions : CfgObject
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
		public readonly List<NormalAction> NormalActions = new List<NormalAction>();
		/// <summary>
		/// 技能动作
		/// <summary>
		public readonly List<SkillAction> SkillActions = new List<SkillAction>();

		public ModelActions(DataStream data)
		{
			this.ModelName = data.GetString();
			this.BaseModelName = data.GetString();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.NormalActions.Add(new NormalAction(data));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.SkillActions.Add(new SkillAction(data));
			}
		}
	}
}
