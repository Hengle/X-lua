using System;
using System.Collections.Generic;
using Csv;

namespace Csv.Skill
{
	public  class SkillAction : CfgObject
	{
		/// <summary>
		/// 默认后续技能使用期限,用于单个技能多段输出
		/// <summary>
		public const float EXPIRE_TIME = 1f;
		/// <summary>
		/// 后续技能使用期限,用于单个技能多段输出
		/// <summary>
		public readonly float SkillExpireTime;
		/// <summary>
		/// 技能结束时间
		/// <summary>
		public readonly float SkillEndTime;
		/// <summary>
		/// 是否需要目标
		/// <summary>
		public readonly bool NeedTarget;
		/// <summary>
		/// 是否可被打断
		/// <summary>
		public readonly bool CanInterrupt;
		/// <summary>
		/// 技能作用范围[半径]
		/// <summary>
		public readonly float SkillRange;
		/// <summary>
		/// 是否显示技能范围
		/// <summary>
		public readonly bool CanShowSkillRange;
		/// <summary>
		/// 放技能时人是否可以转动
		/// <summary>
		public readonly bool CanRotate;
		/// <summary>
		/// 放技能时人是否可以移动
		/// <summary>
		public readonly bool CanMove;
		/// <summary>
		/// 开始位移时间，如果改值为-1 则改值等于技能开始时间
		/// <summary>
		public readonly float StartMoveTime;
		/// <summary>
		/// 结束位移时间，如果改值为-1 则改值等于技能结束时间
		/// <summary>
		public readonly float EndMoveTime;
		/// <summary>
		/// 施放相对位置(1 自己 ,2目标)
		/// <summary>
		public readonly int RelateType;
		/// <summary>
		/// 打击点组列表
		/// <summary>
		public readonly List<HitPointGroup> HitPoints = new List<HitPointGroup>();
		/// <summary>
		/// 打击区域列表
		/// <summary>
		public readonly List<HitZone> HitZones = new List<HitZone>();
		/// <summary>
		/// 被击效果列表
		/// <summary>
		public readonly List<BeAttackEffect> BeAttackEffects = new List<BeAttackEffect>();

		public SkillAction(DataStream data)
		{
			this.SkillExpireTime = data.GetFloat();
			this.SkillEndTime = data.GetFloat();
			this.NeedTarget = data.GetBool();
			this.CanInterrupt = data.GetBool();
			this.SkillRange = data.GetFloat();
			this.CanShowSkillRange = data.GetBool();
			this.CanRotate = data.GetBool();
			this.CanMove = data.GetBool();
			this.StartMoveTime = data.GetFloat();
			this.EndMoveTime = data.GetFloat();
			this.RelateType = data.GetInt();
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.HitPoints.Add(new HitPointGroup(data));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.HitZones.Add(new HitZone(data));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.BeAttackEffects.Add(new BeAttackEffect(data));
			}
		}
	}
}
