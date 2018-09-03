using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class BeAttackEffect : XmlObject
	{
		/// <summary>
		/// 被击效果id
		/// <summary>
		public int Id;
		/// <summary>
		/// 被打击者的抛物曲线
		/// <summary>
		public int Curve;
		/// <summary>
		/// 被打击者的受击动作，null为默认
		/// <summary>
		public string DefencerAction;
		/// <summary>
		/// 被打击者身上出现的被击特效，Null为默认
		/// <summary>
		public int DefencerEffectId;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Id", this.Id);
			Write(_1, "Curve", this.Curve);
			Write(_1, "DefencerAction", this.DefencerAction);
			Write(_1, "DefencerEffectId", this.DefencerEffectId);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Id": this.Id = ReadInt(_2); break;
				case "Curve": this.Curve = ReadInt(_2); break;
				case "DefencerAction": this.DefencerAction = ReadString(_2); break;
				case "DefencerEffectId": this.DefencerEffectId = ReadInt(_2); break;
			}
		}
	}
}
