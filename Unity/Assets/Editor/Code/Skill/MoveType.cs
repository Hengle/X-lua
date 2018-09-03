using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public sealed class MoveType
	{
		/// <summary>
		/// 方向移动
		/// <summary>
		public const int MoveBack = 0;
		/// <summary>
		/// 向目标移动
		/// <summary>
		public const int MoveToTarget = 1;
		/// <summary>
		/// 按当前方向移动
		/// <summary>
		public const int MoveInDirection = 2;
	}
}
