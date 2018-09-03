using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public enum EffectType
	{
		/// <summary>
		/// 站立
		/// <summary>
		Stand = 0,
		/// <summary>
		/// 跟随
		/// <summary>
		Follow = 1,
		/// <summary>
		/// 追踪对象
		/// <summary>
		Trace = 2,
		/// <summary>
		/// 追踪位置
		/// <summary>
		TracePos = 3,
		/// <summary>
		/// 绑定到相机
		/// <summary>
		BindToCamera = 4,
		/// <summary>
		/// UI站立
		/// <summary>
		UIStand = 5,
	}
}
