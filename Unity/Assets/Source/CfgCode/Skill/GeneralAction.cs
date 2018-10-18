using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public  class GeneralAction : CfgObject
	{
		/// <summary>
		/// 行为名称
		/// <summary>
		public readonly string ActionName;
		/// <summary>
		/// 动作来源
		/// <summary>
		public readonly bool IsFromOther;
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
		/// 时间事件列表
		/// <summary>
		public readonly List<Timeline> Timelines = new List<Timeline>();

		public GeneralAction(DataStream data)
		{
			this.ActionName = data.GetString();
			this.IsFromOther = data.GetBool();
			this.OtherModelName = data.GetString();
			this.ActionFile = data.GetString();
			this.PreActionFile = data.GetString();
			this.PostActionFile = data.GetString();
			this.ActionSpeed = data.GetFloat();
			this.LoopTimes = data.GetInt();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.Timelines.Add((Timeline)data.GetObject(data.GetString()));
			}
		}
	}
}
