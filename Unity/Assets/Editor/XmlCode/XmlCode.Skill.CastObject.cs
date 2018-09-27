using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class CastObject : XmlCode.Skill.Controller
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
		public XmlCode.Vector3 Offset;
		/// <summary>
		/// 释放者释放绑定位置
		/// <summary>
		public XmlCode.Skill.BindType CasterBindType;
		/// <summary>
		/// 被击者受击绑定位置
		/// <summary>
		public XmlCode.Skill.BindType TargetBindType;

		public override void Write(TextWriter _1)
		{
			Write(_1, "IsTraceTarget", this.IsTraceTarget);
			Write(_1, "CurveId", this.CurveId);
			Write(_1, "PassBody", this.PassBody);
			Write(_1, "Offset", this.Offset);
			Write(_1, "CasterBindType", (int)this.CasterBindType);
			Write(_1, "TargetBindType", (int)this.TargetBindType);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "IsTraceTarget": this.IsTraceTarget = ReadBool(_2); break;
				case "CurveId": this.CurveId = ReadInt(_2); break;
				case "PassBody": this.PassBody = ReadBool(_2); break;
				case "Offset": this.Offset = ReadObject<XmlCode.Vector3>(_2, "XmlCode.Vector3"); break;
				case "CasterBindType": this.CasterBindType = (XmlCode.Skill.BindType)ReadInt(_2); break;
				case "TargetBindType": this.TargetBindType = (XmlCode.Skill.BindType)ReadInt(_2); break;
			}
		}
	}
}
