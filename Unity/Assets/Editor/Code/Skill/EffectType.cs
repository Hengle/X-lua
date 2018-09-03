using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public sealed class EffectType
	{
		/// <summary>
		/// 站立
		/// <summary>
		public const int Stand = 0;
		/// <summary>
		/// 跟随
		/// <summary>
		public const int Follow = 1;
		/// <summary>
		/// 追踪对象
		/// <summary>
		public const int Trace = 2;
		/// <summary>
		/// 追踪位置
		/// <summary>
		public const int TracePos = 3;
		/// <summary>
		/// 绑定到相机
		/// <summary>
		public const int BindToCamera = 4;
		/// <summary>
		/// UI站立
		/// <summary>
		public const int UIStand = 5;
	}
}
