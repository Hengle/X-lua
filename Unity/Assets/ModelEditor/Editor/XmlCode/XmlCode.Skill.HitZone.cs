using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class HitZone : XmlObject
	{
		/// <summary>
		/// 打击区域id
		/// <summary>
		public int Id;
		/// <summary>
		/// 打击范围的形态，0：方盒，1:圆柱,2:三棱柱
		/// <summary>
		public XmlCode.Skill.HitSharpType Sharp;
		/// <summary>
		/// 打击范围的中心点在Z轴上的偏移量，向前为正
		/// <summary>
		public float Zoffset;
		/// <summary>
		/// X方向长度
		/// <summary>
		public float Xlength;
		/// <summary>
		/// 底边距地面的高度
		/// <summary>
		public float BottomHeight;
		/// <summary>
		/// 顶部距地面的高度
		/// <summary>
		public float TopHeight;
		/// <summary>
		/// Z方向长度
		/// <summary>
		public float Zlength;
		/// <summary>
		/// 以y轴为中心的切面角度
		/// <summary>
		public float YAngle;
		/// <summary>
		/// 打击区域绕y轴旋转角度（顺时针）,构成扇形
		/// <summary>
		public float YRotation;
		/// <summary>
		/// 最大数量
		/// <summary>
		public int MaxTarget;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "Sharp", (int)this.Sharp);
			Write(_1, "Zoffset", this.Zoffset);
			Write(_1, "Xlength", this.Xlength);
			Write(_1, "BottomHeight", this.BottomHeight);
			Write(_1, "TopHeight", this.TopHeight);
			Write(_1, "Zlength", this.Zlength);
			Write(_1, "YAngle", this.YAngle);
			Write(_1, "YRotation", this.YRotation);
			Write(_1, "MaxTarget", this.MaxTarget);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "Sharp": this.Sharp = (XmlCode.Skill.HitSharpType)ReadInt(_2); break;
				case "Zoffset": this.Zoffset = ReadFloat(_2); break;
				case "Xlength": this.Xlength = ReadFloat(_2); break;
				case "BottomHeight": this.BottomHeight = ReadFloat(_2); break;
				case "TopHeight": this.TopHeight = ReadFloat(_2); break;
				case "Zlength": this.Zlength = ReadFloat(_2); break;
				case "YAngle": this.YAngle = ReadFloat(_2); break;
				case "YRotation": this.YRotation = ReadFloat(_2); break;
				case "MaxTarget": this.MaxTarget = ReadInt(_2); break;
			}
		}
	}
}
