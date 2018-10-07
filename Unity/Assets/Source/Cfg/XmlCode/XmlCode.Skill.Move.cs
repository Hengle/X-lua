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
		/// 是否相对于自己移动
		/// <summary>
		public bool IsRelateSelf;
		/// <summary>
		/// 起始位置相对目标偏移
		/// <summary>
		public XmlCode.Vector3 Offset;
		/// <summary>
		/// Y轴顺时针旋转角度
		/// <summary>
		public float Angle;
		/// <summary>
		/// 位移速度m/s
		/// <summary>
		public float Speed;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Type", (int)this.Type);
			Write(_1, "IsRelateSelf", this.IsRelateSelf);
			Write(_1, "Offset", this.Offset);
			Write(_1, "Angle", this.Angle);
			Write(_1, "Speed", this.Speed);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Type": this.Type = (XmlCode.Skill.MoveType)ReadInt(_2); break;
				case "IsRelateSelf": this.IsRelateSelf = ReadBool(_2); break;
				case "Offset": this.Offset = ReadObject<XmlCode.Vector3>(_2, "XmlCode.Vector3"); break;
				case "Angle": this.Angle = ReadFloat(_2); break;
				case "Speed": this.Speed = ReadFloat(_2); break;
			}
		}
	}
}
