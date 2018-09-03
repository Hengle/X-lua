using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class EffectGroup : XmlObject
	{
		/// <summary>
		/// 特效组ID
		/// <summary>
		public int Id;
		/// <summary>
		/// 特效组名称
		/// <summary>
		public string Name;
		/// <summary>
		/// 特效组行为列表
		/// <summary>
		public List<Action> Actions;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "Name", this.Name);
			Write(_1, "Actions", this.Actions);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "Name": this.Name = ReadString(_2); break;
				case "Actions": GetChilds(_2).ForEach (_3 => this.Actions.Add(ReadObject<XmlCode.Skill.Action>(_3, "XmlCode.Skill.Action"))); break;
			}
		}
	}
}
