using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class CastObject : Cfg.Skill.Controller
	{
		/// <summary>
		/// 是否追踪目标
		/// <summary>
		public readonly bool IsTraceTarget;
		/// <summary>
		/// 飞行参数ID,数据有配置表
		/// <summary>
		public readonly int CurveId;
		/// <summary>
		/// 是否穿透
		/// <summary>
		public readonly bool PassBody;
		/// <summary>
		/// 投射起始偏移
		/// <summary>
		public Cfg.Vector3 Position;
		/// <summary>
		/// 投射起始旋转角度
		/// <summary>
		public Cfg.Vector3 EulerAngles;

		public CastObject(DataStream data) : base(data)
		{
			this.IsTraceTarget = data.GetBool();
			this.CurveId = data.GetInt();
			this.PassBody = data.GetBool();
			this.Position = new Vector3(data);
			this.EulerAngles = new Vector3(data);
		}
	}
}
