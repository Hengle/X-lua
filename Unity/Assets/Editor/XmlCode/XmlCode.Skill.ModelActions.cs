using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class ModelActions : XmlObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public string ModelName;
		/// <summary>
		/// 基础模型名称
		/// <summary>
		public string BaseModelName;
		/// <summary>
		/// 普通动作
		/// <summary>
		public List<NormalAction> NormalActions;
		/// <summary>
		/// 技能动作
		/// <summary>
		public List<SkillAction> SkillActions;

		public override void Write(TextWriter _1)
		{
			Write(_1, "ModelName", this.ModelName);
			Write(_1, "BaseModelName", this.BaseModelName);
			Write(_1, "NormalActions", this.NormalActions);
			Write(_1, "SkillActions", this.SkillActions);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "ModelName": this.ModelName = ReadString(_2); break;
				case "BaseModelName": this.BaseModelName = ReadString(_2); break;
				case "NormalActions": GetChilds(_2).ForEach (_3 => this.NormalActions.Add(ReadObject<XmlCode.Skill.NormalAction>(_3, "XmlCode.Skill.NormalAction"))); break;
				case "SkillActions": GetChilds(_2).ForEach (_3 => this.SkillActions.Add(ReadObject<XmlCode.Skill.SkillAction>(_3, "XmlCode.Skill.SkillAction"))); break;
			}
		}
	}
}
