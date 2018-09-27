using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class PlayParticle : XmlCode.Skill.Timeline
	{
		/// <summary>
		/// 粒子资源的路径
		/// <summary>
		public string Path;
		/// <summary>
		/// 淡出时间,从结束开始淡出;-1时无淡出效果
		/// <summary>
		public float FadeOutTime;
		/// <summary>
		/// 施放相对位置(1 自己 ,2目标)
		/// <summary>
		public XmlCode.Skill.RelateType RelateType;
		/// <summary>
		/// 特效是否始终跟随目标对象方向变化
		/// <summary>
		public bool FollowDir;
		/// <summary>
		/// 结点名称,如果有配置则绑定到结点局部空间;反之绑定世界空间
		/// <summary>
		public string NodeName;
		/// <summary>
		/// 特效结点偏移;特效世界偏移
		/// <summary>
		public XmlCode.Vector3 Position;
		/// <summary>
		/// 特效结点旋转;特效世界旋转
		/// <summary>
		public XmlCode.Vector3 Rotation;
		/// <summary>
		/// 整体缩放大小
		/// <summary>
		public XmlCode.Vector3 Scale;
		/// <summary>
		/// 屏幕对齐类型
		/// <summary>
		public XmlCode.Skill.EffectAlignType AlignType;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Path", this.Path);
			Write(_1, "FadeOutTime", this.FadeOutTime);
			Write(_1, "RelateType", (int)this.RelateType);
			Write(_1, "FollowDir", this.FollowDir);
			Write(_1, "NodeName", this.NodeName);
			Write(_1, "Position", this.Position);
			Write(_1, "Rotation", this.Rotation);
			Write(_1, "Scale", this.Scale);
			Write(_1, "AlignType", (int)this.AlignType);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Path": this.Path = ReadString(_2); break;
				case "FadeOutTime": this.FadeOutTime = ReadFloat(_2); break;
				case "RelateType": this.RelateType = (XmlCode.Skill.RelateType)ReadInt(_2); break;
				case "FollowDir": this.FollowDir = ReadBool(_2); break;
				case "NodeName": this.NodeName = ReadString(_2); break;
				case "Position": this.Position = ReadObject<XmlCode.Vector3>(_2, "XmlCode.Vector3"); break;
				case "Rotation": this.Rotation = ReadObject<XmlCode.Vector3>(_2, "XmlCode.Vector3"); break;
				case "Scale": this.Scale = ReadObject<XmlCode.Vector3>(_2, "XmlCode.Vector3"); break;
				case "AlignType": this.AlignType = (XmlCode.Skill.EffectAlignType)ReadInt(_2); break;
			}
		}
	}
}
