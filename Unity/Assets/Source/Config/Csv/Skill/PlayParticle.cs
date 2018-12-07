using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class PlayParticle : Cfg.Skill.Timeline
	{
		/// <summary>
		/// 粒子资源的路径
		/// <summary>
		public readonly string Path;
		/// <summary>
		/// 是否相对于自己移动
		/// <summary>
		public readonly bool IsRelateSelf;
		/// <summary>
		/// 特效是否始终跟随目标对象方向变化
		/// <summary>
		public readonly bool FollowDir;
		/// <summary>
		/// 节点名称,如果有配置则绑定到节点局部空间;反之绑定世界空间
		/// <summary>
		public readonly string NodeName;
		/// <summary>
		/// 特效结点偏移;特效世界偏移
		/// <summary>
		public Cfg.Vector3 Position;
		/// <summary>
		/// 特效结点旋转;特效世界旋转
		/// <summary>
		public Cfg.Vector3 EulerAngles;
		/// <summary>
		/// 整体缩放大小
		/// <summary>
		public Cfg.Vector3 Scale;
		/// <summary>
		/// 屏幕对齐类型
		/// <summary>
		public readonly int AlignType;

		public PlayParticle(DataStream data) : base(data)
		{
			this.Path = data.GetString();
			this.IsRelateSelf = data.GetBool();
			this.FollowDir = data.GetBool();
			this.NodeName = data.GetString();
			this.Position = new Vector3(data);
			this.EulerAngles = new Vector3(data);
			this.Scale = new Vector3(data);
			this.AlignType = data.GetInt();
		}
	}
}
