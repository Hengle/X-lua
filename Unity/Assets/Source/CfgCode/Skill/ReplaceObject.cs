using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public  class ReplaceObject : Cfg.Skill.Controller
	{
		/// <summary>
		/// 新对象
		/// <summary>
		public readonly string NewObject;
		/// <summary>
		/// 相对位置
		/// <summary>
		public Cfg.Vector3 Offset;
		/// <summary>
		/// 相对旋转
		/// <summary>
		public Cfg.Vector3 EulerAngles;

		public ReplaceObject(DataStream data) : base(data)
		{
			this.NewObject = data.GetString();
			this.Offset = (Vector3)data.GetObject(data.GetString());
			this.EulerAngles = (Vector3)data.GetObject(data.GetString());
		}
	}
}
