using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class HitZone : XmlObject
	{
		/// <summary>
		/// 打击区域id
		/// <summary>
		public int Id;
		/// <summary>
		/// 打击范围的形态，0：方盒，1:圆柱,2:球
		/// <summary>
		public XmlCfg.Skill.HitSharpType Sharp;
		/// <summary>
		/// 坐标偏移量
		/// <summary>
		public XmlCfg.Vector3 Offset;
		/// <summary>
		/// 最大数量
		/// <summary>
		public int MaxNum;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "Sharp", (int)this.Sharp);
			Write(_1, "Offset", this.Offset);
			Write(_1, "MaxNum", this.MaxNum);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "Sharp": this.Sharp = (XmlCfg.Skill.HitSharpType)ReadInt(_2); break;
				case "Offset": this.Offset = ReadObject<XmlCfg.Vector3>(_2, "XmlCfg.Vector3"); break;
				case "MaxNum": this.MaxNum = ReadInt(_2); break;
			}
		}
	}
}
