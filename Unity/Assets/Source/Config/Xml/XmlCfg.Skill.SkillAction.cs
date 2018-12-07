using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg.Skill
{
	public class SkillAction : XmlCfg.Skill.GeneralAction
	{
		/// <summary>
		/// 默认后续技能使用期限,用于单个技能多段输出
		/// <summary>
		public const float EXPIRE_TIME = 1f;
		/// <summary>
		/// 后续技能使用期限,用于单个技能多段输出
		/// <summary>
		public float SkillExpireTime;
		/// <summary>
		/// 技能结束时间
		/// <summary>
		public float SkillEndTime;
		/// <summary>
		/// 是否可被打断
		/// <summary>
		public bool CanInterrupt;
		/// <summary>
		/// 技能锁定对象类型(0不需要目标 1敌方目标 ,2己方目标 3自己 4中立方)
		/// <summary>
		public XmlCfg.Skill.LockObjectType LockType;
		/// <summary>
		/// 技能作用范围[半径]
		/// <summary>
		public float SkillRange;
		/// <summary>
		/// 是否显示技能范围
		/// <summary>
		public bool CanShowSkillRange;
		/// <summary>
		/// 放技能时人是否可以转动
		/// <summary>
		public bool CanRotate;
		/// <summary>
		/// 放技能时人是否可以移动
		/// <summary>
		public bool CanMove;
		/// <summary>
		/// 序列字典集合
		/// <summary>
		public Dictionary<int, Sequence> SequenceDict = new Dictionary<int, Sequence>();

		public override void Write(TextWriter _1)
		{
			base.Write(_1);
			Write(_1, "SkillExpireTime", this.SkillExpireTime);
			Write(_1, "SkillEndTime", this.SkillEndTime);
			Write(_1, "CanInterrupt", this.CanInterrupt);
			Write(_1, "LockType", (int)this.LockType);
			Write(_1, "SkillRange", this.SkillRange);
			Write(_1, "CanShowSkillRange", this.CanShowSkillRange);
			Write(_1, "CanRotate", this.CanRotate);
			Write(_1, "CanMove", this.CanMove);
			Write(_1, "SequenceDict", this.SequenceDict);
		}

		public override void Read(XmlNode _1)
		{
			base.Read(_1);
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "SkillExpireTime": this.SkillExpireTime = ReadFloat(_2); break;
				case "SkillEndTime": this.SkillEndTime = ReadFloat(_2); break;
				case "CanInterrupt": this.CanInterrupt = ReadBool(_2); break;
				case "LockType": this.LockType = (XmlCfg.Skill.LockObjectType)ReadInt(_2); break;
				case "SkillRange": this.SkillRange = ReadFloat(_2); break;
				case "CanShowSkillRange": this.CanShowSkillRange = ReadBool(_2); break;
				case "CanRotate": this.CanRotate = ReadBool(_2); break;
				case "CanMove": this.CanMove = ReadBool(_2); break;
				case "SequenceDict": GetChilds(_2).ForEach (_3 => this.SequenceDict.Add(ReadInt(GetOnlyChild(_3, "Key")), ReadObject<XmlCfg.Skill.Sequence>(GetOnlyChild(_3, "Value"), "XmlCfg.Skill.Sequence"))); break;
			}
		}
	}
}
