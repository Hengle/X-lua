using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Character
{
	public class Model : CfgObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public readonly string Name;
		/// <summary>
		/// 模型分组类型
		/// <summary>
		public readonly int GroupType;
		/// <summary>
		/// 模型路径
		/// <summary>
		public readonly string ModelPath;
		/// <summary>
		/// Avatar路径
		/// <summary>
		public readonly string AvatarPath;
		/// <summary>
		/// 碰撞体半径
		/// <summary>
		public readonly float BodyRadius;
		/// <summary>
		/// 碰撞体高度
		/// <summary>
		public readonly float BodyHeight;
		/// <summary>
		/// 模型缩放
		/// <summary>
		public readonly float ModelScale;

		public Model(DataStream data)
		{
			this.Name = data.GetString();
			this.GroupType = data.GetInt();
			this.ModelPath = data.GetString();
			this.AvatarPath = data.GetString();
			this.BodyRadius = data.GetFloat();
			this.BodyHeight = data.GetFloat();
			this.ModelScale = data.GetFloat();
		}
	}
}
