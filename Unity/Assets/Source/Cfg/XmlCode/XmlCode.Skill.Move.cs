using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class Move : XmlCode.Skill.Controller
	{
		/// <summary>
		/// 移动方式:0向目标移动 1按指定方向移动
		/// <summary>
		public XmlCode.Skill.MoveType Type;
		/// <summary>
		/// 施放相对位置(1 自己 ,2目标)
		/// <summary>
		public XmlCode.Skill.RelateType RelateType;
		/// <summary>
		/// 起始位置相对目标偏移
		/// <summary>
		public XmlCode.Vector3 Offset;
		/// <summary>
		/// Y轴顺时针旋转角度
		/// <summary>
		public float Angle;
		/// <summary>
		/// 位移长度
		/// <summary>
		public float Length;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Type", (int)this.Type);
			Write(_1, "RelateType", (int)this.RelateType);
			Write(_1, "Offset", this.Offset);
			Write(_1, "Angle", this.Angle);
			Write(_1, "Length", this.Length);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Type": this.Type = (XmlCode.Skill.MoveType)ReadInt(_2); break;
				case "RelateType": this.RelateType = (XmlCode.Skill.RelateType)ReadInt(_2); break;
				case "Offset": this.Offset = ReadObject<XmlCode.Vector3>(_2, "XmlCode.Vector3"); break;
				case "Angle": this.Angle = ReadFloat(_2); break;
				case "Length": this.Length = ReadFloat(_2); break;
			}
		}
	}
}
