using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class HitPointGroup : XmlObject
	{
		/// <summary>
		/// 打击点组ID
		/// <summary>
		public int Id;
		/// <summary>
		/// 打击点组名称
		/// <summary>
		public string Name;
		/// <summary>
		/// 打击点列表
		/// <summary>
		public List<Attack> Attacks;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "Name", this.Name);
			Write(_1, "Attacks", this.Attacks);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "Name": this.Name = ReadString(_2); break;
				case "Attacks": GetChilds(_2).ForEach (_3 => this.Attacks.Add(ReadDynamicObject<XmlCode.Skill.Attack>(_3, "Skill"))); break;
			}
		}
	}
}
