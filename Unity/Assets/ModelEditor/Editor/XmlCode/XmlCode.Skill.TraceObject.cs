using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class TraceObject : XmlCode.Skill.SpawnObject
	{
		/// <summary>
		/// 身体矫正值
		/// <summary>
		public const float BODY_CORRECT = 0.7f;
		/// <summary>
		/// 头部矫正值
		/// <summary>
		public const float HEAD_CORRECT = 1.3f;
		/// <summary>
		/// 特效ID
		/// <summary>
		public int EffectId;
		/// <summary>
		/// 是否追踪目标
		/// <summary>
		public bool IsTraceTarget;
		/// <summary>
		/// 飞行参数ID,数据有配置表
		/// <summary>
		public int TraceCurveId;
		/// <summary>
		/// 目标偏移X
		/// <summary>
		public float OffsetX;
		/// <summary>
		/// 目标偏移Y
		/// <summary>
		public float OffsetY;
		/// <summary>
		/// 目标偏移Z
		/// <summary>
		public float OffsetZ;
		/// <summary>
		/// 追踪类型
		/// <summary>
		public XmlCode.Skill.TraceType TraceType;
		/// <summary>
		/// 释放者绑定位置
		/// <summary>
		public XmlCode.Skill.TraceBindType CasterBindType;
		/// <summary>
		/// 被击者绑定位置
		/// <summary>
		public XmlCode.Skill.TraceBindType TargetBindType;

		public override void Write(TextWriter _1)
		{
			Write(_1, "EffectId", this.EffectId);
			Write(_1, "IsTraceTarget", this.IsTraceTarget);
			Write(_1, "TraceCurveId", this.TraceCurveId);
			Write(_1, "OffsetX", this.OffsetX);
			Write(_1, "OffsetY", this.OffsetY);
			Write(_1, "OffsetZ", this.OffsetZ);
			Write(_1, "TraceType", (int)this.TraceType);
			Write(_1, "CasterBindType", (int)this.CasterBindType);
			Write(_1, "TargetBindType", (int)this.TargetBindType);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "EffectId": this.EffectId = ReadInt(_2); break;
				case "IsTraceTarget": this.IsTraceTarget = ReadBool(_2); break;
				case "TraceCurveId": this.TraceCurveId = ReadInt(_2); break;
				case "OffsetX": this.OffsetX = ReadFloat(_2); break;
				case "OffsetY": this.OffsetY = ReadFloat(_2); break;
				case "OffsetZ": this.OffsetZ = ReadFloat(_2); break;
				case "TraceType": this.TraceType = (XmlCode.Skill.TraceType)ReadInt(_2); break;
				case "CasterBindType": this.CasterBindType = (XmlCode.Skill.TraceBindType)ReadInt(_2); break;
				case "TargetBindType": this.TargetBindType = (XmlCode.Skill.TraceBindType)ReadInt(_2); break;
			}
		}
	}
}
