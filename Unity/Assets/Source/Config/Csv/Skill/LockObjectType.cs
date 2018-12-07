using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public enum LockObjectType
	{
		/// <summary>
		/// 不需要目标
		/// <summary>
		None = 0,
		/// <summary>
		/// 敌方目标
		/// <summary>
		Enemy = 1,
		/// <summary>
		/// 己方目标
		/// <summary>
		Teammate = 2,
		/// <summary>
		/// 自己
		/// <summary>
		Self = 3,
		/// <summary>
		/// 中立方
		/// <summary>
		Other = 4,
	}
}
