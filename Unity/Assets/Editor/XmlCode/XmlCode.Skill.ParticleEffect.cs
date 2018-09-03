using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class ParticleEffect : XmlCode.Skill.Action
	{
		/// <summary>
		/// 粒子特效id
		/// <summary>
		public int Id;
		/// <summary>
		/// 特效类型
		/// <summary>
		public int Type;
		/// <summary>
		/// 淡出时间
		/// <summary>
		public float FadeOutTime;
		/// <summary>
		/// 粒子资源的路径
		/// <summary>
		public string Path;
		/// <summary>
		/// 粒子的存续时间，-1则与参与动画播放时间等长
		/// <summary>
		public float Life;
		/// <summary>
		/// 是否跟随释放者方向
		/// <summary>
		public bool FollowDirection;
		/// <summary>
		/// 敌人的被击特效是否始终跟随敌我位置变化而转向
		/// <summary>
		public bool FollowBeAttackedDirection;
		/// <summary>
		/// 缩放大小
		/// <summary>
		public float Scale;
		/// <summary>
		/// 释放者施放技能的位置编号
		/// <summary>
		public int CasterBindType;
		/// <summary>
		/// 是否跟随绑定骨骼方向
		/// <summary>
		public bool FollowBoneDirection;
		/// <summary>
		/// 被击者出现被击特效的位置
		/// <summary>
		public int TargetBindType;
		/// <summary>
		/// 跟踪类型(0:Line)
		/// <summary>
		public int InstanceTraceType;
		/// <summary>
		/// 特效世界偏移X
		/// <summary>
		public float WorldOffsetX;
		/// <summary>
		/// 特效世界偏移Y
		/// <summary>
		public float WorldOffsetY;
		/// <summary>
		/// 特效世界偏移Z
		/// <summary>
		public float WorldOffsetZ;
		/// <summary>
		/// 特效世界旋转X
		/// <summary>
		public float WorldRotateX;
		/// <summary>
		/// 特效世界旋转Y
		/// <summary>
		public float WorldRotateY;
		/// <summary>
		/// 特效世界旋转Z
		/// <summary>
		public float WorldRotateZ;
		/// <summary>
		/// 特效骨骼偏移X
		/// <summary>
		public float BonePostionX;
		/// <summary>
		/// 特效骨骼偏移Y
		/// <summary>
		public float BonePostionY;
		/// <summary>
		/// 特效骨骼偏移Z
		/// <summary>
		public float BonePostionZ;
		/// <summary>
		/// 特效骨骼旋转X
		/// <summary>
		public float BoneRotationX;
		/// <summary>
		/// 特效骨骼旋转Y
		/// <summary>
		public float BoneRotationY;
		/// <summary>
		/// 特效骨骼旋转Z
		/// <summary>
		public float BoneRotationZ;
		/// <summary>
		/// 特效骨骼缩放X
		/// <summary>
		public float BoneScaleX;
		/// <summary>
		/// 特效骨骼缩放Y
		/// <summary>
		public float BoneScaleY;
		/// <summary>
		/// 特效骨骼缩放Z
		/// <summary>
		public float BoneScaleZ;
		/// <summary>
		/// 骨骼名称
		/// <summary>
		public string BoneName;
		/// <summary>
		/// 飞行时间
		/// <summary>
		public float TraceTime;
		/// <summary>
		/// 屏幕对齐类型
		/// <summary>
		public XmlCode.Skill.EffectAlignType AlignType;
		/// <summary>
		/// 是否特效池管理
		/// <summary>
		public bool IsPoolDestroy;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "Type", this.Type);
			Write(_1, "FadeOutTime", this.FadeOutTime);
			Write(_1, "Path", this.Path);
			Write(_1, "Life", this.Life);
			Write(_1, "FollowDirection", this.FollowDirection);
			Write(_1, "FollowBeAttackedDirection", this.FollowBeAttackedDirection);
			Write(_1, "Scale", this.Scale);
			Write(_1, "CasterBindType", this.CasterBindType);
			Write(_1, "FollowBoneDirection", this.FollowBoneDirection);
			Write(_1, "TargetBindType", this.TargetBindType);
			Write(_1, "InstanceTraceType", this.InstanceTraceType);
			Write(_1, "WorldOffsetX", this.WorldOffsetX);
			Write(_1, "WorldOffsetY", this.WorldOffsetY);
			Write(_1, "WorldOffsetZ", this.WorldOffsetZ);
			Write(_1, "WorldRotateX", this.WorldRotateX);
			Write(_1, "WorldRotateY", this.WorldRotateY);
			Write(_1, "WorldRotateZ", this.WorldRotateZ);
			Write(_1, "BonePostionX", this.BonePostionX);
			Write(_1, "BonePostionY", this.BonePostionY);
			Write(_1, "BonePostionZ", this.BonePostionZ);
			Write(_1, "BoneRotationX", this.BoneRotationX);
			Write(_1, "BoneRotationY", this.BoneRotationY);
			Write(_1, "BoneRotationZ", this.BoneRotationZ);
			Write(_1, "BoneScaleX", this.BoneScaleX);
			Write(_1, "BoneScaleY", this.BoneScaleY);
			Write(_1, "BoneScaleZ", this.BoneScaleZ);
			Write(_1, "BoneName", this.BoneName);
			Write(_1, "TraceTime", this.TraceTime);
			Write(_1, "AlignType", (int)this.AlignType);
			Write(_1, "IsPoolDestroy", this.IsPoolDestroy);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "Type": this.Type = ReadInt(_2); break;
				case "FadeOutTime": this.FadeOutTime = ReadFloat(_2); break;
				case "Path": this.Path = ReadString(_2); break;
				case "Life": this.Life = ReadFloat(_2); break;
				case "FollowDirection": this.FollowDirection = ReadBool(_2); break;
				case "FollowBeAttackedDirection": this.FollowBeAttackedDirection = ReadBool(_2); break;
				case "Scale": this.Scale = ReadFloat(_2); break;
				case "CasterBindType": this.CasterBindType = ReadInt(_2); break;
				case "FollowBoneDirection": this.FollowBoneDirection = ReadBool(_2); break;
				case "TargetBindType": this.TargetBindType = ReadInt(_2); break;
				case "InstanceTraceType": this.InstanceTraceType = ReadInt(_2); break;
				case "WorldOffsetX": this.WorldOffsetX = ReadFloat(_2); break;
				case "WorldOffsetY": this.WorldOffsetY = ReadFloat(_2); break;
				case "WorldOffsetZ": this.WorldOffsetZ = ReadFloat(_2); break;
				case "WorldRotateX": this.WorldRotateX = ReadFloat(_2); break;
				case "WorldRotateY": this.WorldRotateY = ReadFloat(_2); break;
				case "WorldRotateZ": this.WorldRotateZ = ReadFloat(_2); break;
				case "BonePostionX": this.BonePostionX = ReadFloat(_2); break;
				case "BonePostionY": this.BonePostionY = ReadFloat(_2); break;
				case "BonePostionZ": this.BonePostionZ = ReadFloat(_2); break;
				case "BoneRotationX": this.BoneRotationX = ReadFloat(_2); break;
				case "BoneRotationY": this.BoneRotationY = ReadFloat(_2); break;
				case "BoneRotationZ": this.BoneRotationZ = ReadFloat(_2); break;
				case "BoneScaleX": this.BoneScaleX = ReadFloat(_2); break;
				case "BoneScaleY": this.BoneScaleY = ReadFloat(_2); break;
				case "BoneScaleZ": this.BoneScaleZ = ReadFloat(_2); break;
				case "BoneName": this.BoneName = ReadString(_2); break;
				case "TraceTime": this.TraceTime = ReadFloat(_2); break;
				case "AlignType": this.AlignType = (XmlCode.Skill.EffectAlignType)ReadInt(_2); break;
				case "IsPoolDestroy": this.IsPoolDestroy = ReadBool(_2); break;
			}
		}
	}
}
