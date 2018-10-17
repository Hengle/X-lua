using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Character
{
	public  class Model : XmlObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public string Name = "";
		/// <summary>
		/// 模型分组类型
		/// <summary>
		public XmlCfg.Character.GroupType GroupType;
		/// <summary>
		/// 模型级别
		/// <summary>
		public int Level;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Name", this.Name);
			Write(_1, "GroupType", (int)this.GroupType);
			Write(_1, "Level", this.Level);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Name": this.Name = ReadString(_2); break;
				case "GroupType": this.GroupType = (XmlCfg.Character.GroupType)ReadInt(_2); break;
				case "Level": this.Level = ReadInt(_2); break;
			}
		}
	}
}
