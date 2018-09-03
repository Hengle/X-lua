using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class ParticleEffect : Csv.Skill.Action
	{
		/// <summary>
		/// 粒子特效id
		/// <summary>
		public readonly int Id;
		/// <summary>
		/// 特效类型
		/// <summary>
		public readonly int Type;
		/// <summary>
		/// 淡出时间
		/// <summary>
		public readonly float FadeOutTime;
		/// <summary>
		/// 粒子资源的路径
		/// <summary>
		public readonly string Path;
		/// <summary>
		/// 粒子的存续时间，-1则与参与动画播放时间等长
		/// <summary>
		public readonly float Life;
		/// <summary>
		/// 是否跟随释放者方向
		/// <summary>
		public readonly bool FollowDirection;
		/// <summary>
		/// 敌人的被击特效是否始终跟随敌我位置变化而转向
		/// <summary>
		public readonly bool FollowBeAttackedDirection;
		/// <summary>
		/// 缩放大小
		/// <summary>
		public readonly float Scale;
		/// <summary>
		/// 释放者施放技能的位置编号
		/// <summary>
		public readonly int CasterBindType;
		/// <summary>
		/// 是否跟随绑定骨骼方向
		/// <summary>
		public readonly bool FollowBoneDirection;
		/// <summary>
		/// 被击者出现被击特效的位置
		/// <summary>
		public readonly int TargetBindType;
		/// <summary>
		/// 跟踪类型(0:Line)
		/// <summary>
		public readonly int InstanceTraceType;
		/// <summary>
		/// 特效世界偏移X
		/// <summary>
		public readonly float WorldOffsetX;
		/// <summary>
		/// 特效世界偏移Y
		/// <summary>
		public readonly float WorldOffsetY;
		/// <summary>
		/// 特效世界偏移Z
		/// <summary>
		public readonly float WorldOffsetZ;
		/// <summary>
		/// 特效世界旋转X
		/// <summary>
		public readonly float WorldRotateX;
		/// <summary>
		/// 特效世界旋转Y
		/// <summary>
		public readonly float WorldRotateY;
		/// <summary>
		/// 特效世界旋转Z
		/// <summary>
		public readonly float WorldRotateZ;
		/// <summary>
		/// 特效骨骼偏移X
		/// <summary>
		public readonly float BonePostionX;
		/// <summary>
		/// 特效骨骼偏移Y
		/// <summary>
		public readonly float BonePostionY;
		/// <summary>
		/// 特效骨骼偏移Z
		/// <summary>
		public readonly float BonePostionZ;
		/// <summary>
		/// 特效骨骼旋转X
		/// <summary>
		public readonly float BoneRotationX;
		/// <summary>
		/// 特效骨骼旋转Y
		/// <summary>
		public readonly float BoneRotationY;
		/// <summary>
		/// 特效骨骼旋转Z
		/// <summary>
		public readonly float BoneRotationZ;
		/// <summary>
		/// 特效骨骼缩放X
		/// <summary>
		public readonly float BoneScaleX;
		/// <summary>
		/// 特效骨骼缩放Y
		/// <summary>
		public readonly float BoneScaleY;
		/// <summary>
		/// 特效骨骼缩放Z
		/// <summary>
		public readonly float BoneScaleZ;
		/// <summary>
		/// 骨骼名称
		/// <summary>
		public readonly string BoneName;
		/// <summary>
		/// 飞行时间
		/// <summary>
		public readonly float TraceTime;
		/// <summary>
		/// 屏幕对齐类型
		/// <summary>
		public readonly int AlignType;
		/// <summary>
		/// 是否特效池管理
		/// <summary>
		public readonly bool IsPoolDestroy;

		public ParticleEffect(DataStream data) : base(data)
		{
			this.Id = data.GetInt();
			this.Type = data.GetInt();
			this.FadeOutTime = data.GetFloat();
			this.Path = data.GetString();
			this.Life = data.GetFloat();
			this.FollowDirection = data.GetBool();
			this.FollowBeAttackedDirection = data.GetBool();
			this.Scale = data.GetFloat();
			this.CasterBindType = data.GetInt();
			this.FollowBoneDirection = data.GetBool();
			this.TargetBindType = data.GetInt();
			this.InstanceTraceType = data.GetInt();
			this.WorldOffsetX = data.GetFloat();
			this.WorldOffsetY = data.GetFloat();
			this.WorldOffsetZ = data.GetFloat();
			this.WorldRotateX = data.GetFloat();
			this.WorldRotateY = data.GetFloat();
			this.WorldRotateZ = data.GetFloat();
			this.BonePostionX = data.GetFloat();
			this.BonePostionY = data.GetFloat();
			this.BonePostionZ = data.GetFloat();
			this.BoneRotationX = data.GetFloat();
			this.BoneRotationY = data.GetFloat();
			this.BoneRotationZ = data.GetFloat();
			this.BoneScaleX = data.GetFloat();
			this.BoneScaleY = data.GetFloat();
			this.BoneScaleZ = data.GetFloat();
			this.BoneName = data.GetString();
			this.TraceTime = data.GetFloat();
			this.AlignType = data.GetInt();
			this.IsPoolDestroy = data.GetBool();
		}
	}
}
