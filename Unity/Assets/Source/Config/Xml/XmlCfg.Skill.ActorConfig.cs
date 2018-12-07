using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class ActorConfig : XmlObject
	{
		/// <summary>
		/// 模型名称
		/// <summary>
		public string ModelName = "";
		/// <summary>
		/// 基础模型名称
		/// <summary>
		public string BaseModelName = "";
		/// <summary>
		/// 普通动作
		/// <summary>
		public Dictionary<string, GeneralAction> GeneralActions = new Dictionary<string, GeneralAction>();
		/// <summary>
		/// 技能动作
		/// <summary>
		public Dictionary<string, SkillAction> SkillActions = new Dictionary<string, SkillAction>();

		public override void Write(TextWriter _1)
		{
			Write(_1, "ModelName", this.ModelName);
			Write(_1, "BaseModelName", this.BaseModelName);
			Write(_1, "GeneralActions", this.GeneralActions);
			Write(_1, "SkillActions", this.SkillActions);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "ModelName": this.ModelName = ReadString(_2); break;
				case "BaseModelName": this.BaseModelName = ReadString(_2); break;
				case "GeneralActions": GetChilds(_2).ForEach (_3 => this.GeneralActions.Add(ReadString(GetOnlyChild(_3, "Key")), ReadObject<XmlCfg.Skill.GeneralAction>(GetOnlyChild(_3, "Value"), "XmlCfg.Skill.GeneralAction"))); break;
				case "SkillActions": GetChilds(_2).ForEach (_3 => this.SkillActions.Add(ReadString(GetOnlyChild(_3, "Key")), ReadDynamicObject<XmlCfg.Skill.SkillAction>(GetOnlyChild(_3, "Value"), "XmlCfg.Skill"))); break;
			}
		}
	}
}
