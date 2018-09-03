using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class NormalAction : CfgObject
	{
		/// <summary>
		/// 行为名称
		/// <summary>
		public readonly string ActionName;
		/// <summary>
		/// 动作来源
		/// <summary>
		public readonly int ActionSource;
		/// <summary>
		/// 其他模型名称,用于套用其他模型动作
		/// <summary>
		public readonly string OtherModelName;
		/// <summary>
		/// 绑定的动作名称
		/// <summary>
		public readonly string ActionFile;
		/// <summary>
		/// 前摇动作名称
		/// <summary>
		public readonly string PreActionFile;
		/// <summary>
		/// 后摇动作名称
		/// <summary>
		public readonly string PostActionFile;
		/// <summary>
		/// 动作播放速率
		/// <summary>
		public readonly float ActionSpeed;
		/// <summary>
		/// 动作循环次数
		/// <summary>
		public readonly int LoopTimes;
		/// <summary>
		/// 特效ID
		/// <summary>
		public readonly int EffectId;
		/// <summary>
		/// 时间事件列表
		/// <summary>
		public readonly List<Action> Actions = new List<Action>();
		/// <summary>
		/// 特效组列表
		/// <summary>
		public readonly List<EffectGroup> Effects = new List<EffectGroup>();

		public NormalAction(DataStream data)
		{
			this.ActionName = data.GetString();
			this.ActionSource = data.GetInt();
			this.OtherModelName = data.GetString();
			this.ActionFile = data.GetString();
			this.PreActionFile = data.GetString();
			this.PostActionFile = data.GetString();
			this.ActionSpeed = data.GetFloat();
			this.LoopTimes = data.GetInt();
			this.EffectId = data.GetInt();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.Actions.Add(new Action(data));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.Effects.Add(new EffectGroup(data));
			}
		}
	}
}
