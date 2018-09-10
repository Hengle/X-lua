using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class SpawnObject : XmlCode.Skill.Action
	{
		/// <summary>
		/// 子物体ID
		/// <summary>
		public float Id;
		/// <summary>
		/// 子物体类型
		/// <summary>
		public float SpawnType;
		/// <summary>
		/// 子物体存活时间
		/// <summary>
		public float Life;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "SpawnType", this.SpawnType);
			Write(_1, "Life", this.Life);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadFloat(_2); break;
				case "SpawnType": this.SpawnType = ReadFloat(_2); break;
				case "Life": this.Life = ReadFloat(_2); break;
			}
		}
	}
}
