using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class CastObject : XmlCfg.Skill.Controller
	{
		/// <summary>
		/// 是否追踪目标
		/// <summary>
		public bool IsTraceTarget;
		/// <summary>
		/// 飞行参数ID,数据有配置表
		/// <summary>
		public int CurveId;
		/// <summary>
		/// 是否穿透
		/// <summary>
		public bool PassBody;
		/// <summary>
		/// 投射起始偏移
		/// <summary>
		public XmlCfg.Vector3 Position;
		/// <summary>
		/// 投射起始旋转角度
		/// <summary>
		public XmlCfg.Vector3 EulerAngles;

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "IsTraceTarget", this.IsTraceTarget);
			Write(_1, "CurveId", this.CurveId);
			Write(_1, "PassBody", this.PassBody);
			Write(_1, "Position", this.Position);
			Write(_1, "EulerAngles", this.EulerAngles);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "IsTraceTarget": this.IsTraceTarget = ReadBool(_2); break;
				case "CurveId": this.CurveId = ReadInt(_2); break;
				case "PassBody": this.PassBody = ReadBool(_2); break;
				case "Position": this.Position = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
				case "EulerAngles": this.EulerAngles = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
			}
		}
	}
}
