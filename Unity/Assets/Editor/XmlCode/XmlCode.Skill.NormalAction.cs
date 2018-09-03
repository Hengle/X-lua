using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class NormalAction : XmlObject
	{
		/// <summary>
		/// 行为名称
		/// <summary>
		public string ActionName;
		/// <summary>
		/// 动作来源
		/// <summary>
		public XmlCode.Skill.ActionSourceType ActionSource;
		/// <summary>
		/// 其他模型名称,用于套用其他模型动作
		/// <summary>
		public string OtherModelName;
		/// <summary>
		/// 绑定的动作名称
		/// <summary>
		public string ActionFile;
		/// <summary>
		/// 前摇动作名称
		/// <summary>
		public string PreActionFile;
		/// <summary>
		/// 后摇动作名称
		/// <summary>
		public string PostActionFile;
		/// <summary>
		/// 动作播放速率
		/// <summary>
		public float ActionSpeed;
		/// <summary>
		/// 动作循环次数
		/// <summary>
		public int LoopTimes;
		/// <summary>
		/// 特效ID
		/// <summary>
		public int EffectId;
		/// <summary>
		/// 时间事件列表
		/// <summary>
		public List<Action> Actions;
		/// <summary>
		/// 特效组列表
		/// <summary>
		public List<EffectGroup> Effects;

		public override void Write(TextWriter _1)
		{
			Write(_1, "ActionName", this.ActionName);
			Write(_1, "ActionSource", (int)this.ActionSource);
			Write(_1, "OtherModelName", this.OtherModelName);
			Write(_1, "ActionFile", this.ActionFile);
			Write(_1, "PreActionFile", this.PreActionFile);
			Write(_1, "PostActionFile", this.PostActionFile);
			Write(_1, "ActionSpeed", this.ActionSpeed);
			Write(_1, "LoopTimes", this.LoopTimes);
			Write(_1, "EffectId", this.EffectId);
			Write(_1, "Actions", this.Actions);
			Write(_1, "Effects", this.Effects);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "ActionName": this.ActionName = ReadString(_2); break;
				case "ActionSource": this.ActionSource = (XmlCode.Skill.ActionSourceType)ReadInt(_2); break;
				case "OtherModelName": this.OtherModelName = ReadString(_2); break;
				case "ActionFile": this.ActionFile = ReadString(_2); break;
				case "PreActionFile": this.PreActionFile = ReadString(_2); break;
				case "PostActionFile": this.PostActionFile = ReadString(_2); break;
				case "ActionSpeed": this.ActionSpeed = ReadFloat(_2); break;
				case "LoopTimes": this.LoopTimes = ReadInt(_2); break;
				case "EffectId": this.EffectId = ReadInt(_2); break;
				case "Actions": GetChilds(_2).ForEach (_3 => this.Actions.Add(ReadObject<XmlCode.Skill.Action>(_3, "XmlCode.Skill.Action"))); break;
				case "Effects": GetChilds(_2).ForEach (_3 => this.Effects.Add(ReadObject<XmlCode.Skill.EffectGroup>(_3, "XmlCode.Skill.EffectGroup"))); break;
			}
		}
	}
}
