using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class EffectGroup : CfgObject
	{
		/// <summary>
		/// 特效组ID
		/// <summary>
		public readonly int Id;
		/// <summary>
		/// 特效组名称
		/// <summary>
		public readonly string Name;
		/// <summary>
		/// 特效组行为列表
		/// <summary>
		public readonly List<Action> Actions = new List<Action>();

		public EffectGroup(DataStream data)
		{
			this.Id = data.GetInt();
			this.Name = data.GetString();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.Actions.Add(new Action(data));
			}
		}
	}
}
