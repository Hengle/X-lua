using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.Skill
{
	public class SkillAction : Cfg.Skill.GeneralAction
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
		/// 是否可被打断
		/// <summary>
		public readonly bool CanInterrupt;
		/// <summary>
		/// 技能锁定对象类型(0不需要目标 1敌方目标 ,2己方目标 3自己 4中立方)
		/// <summary>
		public readonly int LockType;
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
		/// 序列字典集合
		/// <summary>
		public readonly Dictionary<int, Sequence> SequenceDict = new Dictionary<int, Sequence>();

		public SkillAction(DataStream data) : base(data)
		{
			this.SkillExpireTime = data.GetFloat();
			this.SkillEndTime = data.GetFloat();
			this.CanInterrupt = data.GetBool();
			this.LockType = data.GetInt();
			this.SkillRange = data.GetFloat();
			this.CanShowSkillRange = data.GetBool();
			this.CanRotate = data.GetBool();
			this.CanMove = data.GetBool();
			for (int n = data.GetInt(); n-- > 0;)
			{
				int k = data.GetInt();
				this.SequenceDict[k] = new Sequence(data);
			}
		}
	}
}
