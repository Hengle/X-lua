using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class SkillAction : XmlObject
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
		/// 是否需要目标
		/// <summary>
		public bool NeedTarget;
		/// <summary>
		/// 是否可被打断
		/// <summary>
		public bool CanInterrupt;
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
		/// 开始位移时间，如果改值为-1 则改值等于技能开始时间
		/// <summary>
		public float StartMoveTime;
		/// <summary>
		/// 结束位移时间，如果改值为-1 则改值等于技能结束时间
		/// <summary>
		public float EndMoveTime;
		/// <summary>
		/// 施放相对位置(1 自己 ,2目标)
		/// <summary>
		public XmlCode.Skill.SkillRelateType RelateType;
		/// <summary>
		/// 打击点组列表
		/// <summary>
		public List<HitPointGroup> HitPoints;
		/// <summary>
		/// 打击区域列表
		/// <summary>
		public List<HitZone> HitZones;
		/// <summary>
		/// 被击效果列表
		/// <summary>
		public List<BeAttackEffect> BeAttackEffects;

		public override void Write(TextWriter _1)
		{
			Write(_1, "SkillExpireTime", this.SkillExpireTime);
			Write(_1, "SkillEndTime", this.SkillEndTime);
			Write(_1, "NeedTarget", this.NeedTarget);
			Write(_1, "CanInterrupt", this.CanInterrupt);
			Write(_1, "SkillRange", this.SkillRange);
			Write(_1, "CanShowSkillRange", this.CanShowSkillRange);
			Write(_1, "CanRotate", this.CanRotate);
			Write(_1, "CanMove", this.CanMove);
			Write(_1, "StartMoveTime", this.StartMoveTime);
			Write(_1, "EndMoveTime", this.EndMoveTime);
			Write(_1, "RelateType", (int)this.RelateType);
			Write(_1, "HitPoints", this.HitPoints);
			Write(_1, "HitZones", this.HitZones);
			Write(_1, "BeAttackEffects", this.BeAttackEffects);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "SkillExpireTime": this.SkillExpireTime = ReadFloat(_2); break;
				case "SkillEndTime": this.SkillEndTime = ReadFloat(_2); break;
				case "NeedTarget": this.NeedTarget = ReadBool(_2); break;
				case "CanInterrupt": this.CanInterrupt = ReadBool(_2); break;
				case "SkillRange": this.SkillRange = ReadFloat(_2); break;
				case "CanShowSkillRange": this.CanShowSkillRange = ReadBool(_2); break;
				case "CanRotate": this.CanRotate = ReadBool(_2); break;
				case "CanMove": this.CanMove = ReadBool(_2); break;
				case "StartMoveTime": this.StartMoveTime = ReadFloat(_2); break;
				case "EndMoveTime": this.EndMoveTime = ReadFloat(_2); break;
				case "RelateType": this.RelateType = (XmlCode.Skill.SkillRelateType)ReadInt(_2); break;
				case "HitPoints": GetChilds(_2).ForEach (_3 => this.HitPoints.Add(ReadObject<XmlCode.Skill.HitPointGroup>(_3, "XmlCode.Skill.HitPointGroup"))); break;
				case "HitZones": GetChilds(_2).ForEach (_3 => this.HitZones.Add(ReadObject<XmlCode.Skill.HitZone>(_3, "XmlCode.Skill.HitZone"))); break;
				case "BeAttackEffects": GetChilds(_2).ForEach (_3 => this.BeAttackEffects.Add(ReadObject<XmlCode.Skill.BeAttackEffect>(_3, "XmlCode.Skill.BeAttackEffect"))); break;
			}
		}
	}
}
