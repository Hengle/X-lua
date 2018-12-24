using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class PlayParticle : XmlCfg.Skill.Timeline
	{
		/// <summary>
		/// 粒子资源的路径
		/// <summary>
		public string Path = "";
		/// <summary>
		/// 是否相对于自己移动
		/// <summary>
		public bool IsRelateSelf;
		/// <summary>
		/// 特效是否始终跟随目标对象方向变化
		/// <summary>
		public bool FollowDir;
		/// <summary>
		/// 节点名称,如果有配置则绑定到节点局部空间;反之绑定世界空间
		/// <summary>
		public string NodeName = "";
		/// <summary>
		/// 特效结点偏移;特效世界偏移
		/// <summary>
		public XmlCfg.Vector3 Position;
		/// <summary>
		/// 特效结点旋转;特效世界旋转
		/// <summary>
		public XmlCfg.Vector3 EulerAngles;
		/// <summary>
		/// 整体缩放大小
		/// <summary>
		public XmlCfg.Vector3 Scale;
		/// <summary>
		/// 屏幕对齐类型
		/// <summary>
		public XmlCfg.Skill.EffectAlignType AlignType;

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "Path", this.Path);
			Write(_1, "IsRelateSelf", this.IsRelateSelf);
			Write(_1, "FollowDir", this.FollowDir);
			Write(_1, "NodeName", this.NodeName);
			Write(_1, "Position", this.Position);
			Write(_1, "EulerAngles", this.EulerAngles);
			Write(_1, "Scale", this.Scale);
			Write(_1, "AlignType", (int)this.AlignType);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Path": this.Path = ReadString(_2); break;
				case "IsRelateSelf": this.IsRelateSelf = ReadBool(_2); break;
				case "FollowDir": this.FollowDir = ReadBool(_2); break;
				case "NodeName": this.NodeName = ReadString(_2); break;
				case "Position": this.Position = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
				case "EulerAngles": this.EulerAngles = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
				case "Scale": this.Scale = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
				case "AlignType": this.AlignType = (XmlCfg.Skill.EffectAlignType)ReadInt(_2); break;
			}
		}
	}
}
