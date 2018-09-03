using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XmlCode.Skill
{
	public  class ShakeScreen : XmlCode.Skill.Action
	{
		/// <summary>
		/// 震屏方式
		/// <summary>
		public string Type;
		/// <summary>
		/// 每秒震动的次数
		/// <summary>
		public int Frequency;
		/// <summary>
		/// 初始频率维持时间
		/// <summary>
		public float FrequencyDuration;
		/// <summary>
		/// 频率衰减
		/// <summary>
		public float FrequencyAtten;
		/// <summary>
		/// 单次振幅
		/// <summary>
		public float Amplitude;
		/// <summary>
		/// 单次震动的衰减幅度
		/// <summary>
		public float AmplitudeAtten;
		/// <summary>
		/// 本次震动持续时间，-1表持续到下一次震动触发或技能结束为止
		/// <summary>
		public float Life;
		/// <summary>
		/// 最小完整影响范围
		/// <summary>
		public float MinRange;
		/// <summary>
		/// 最大影响范围
		/// <summary>
		public float MaxRange;

		public override void Write(TextWriter _1)
		{
			Write(_1, "Type", this.Type);
			Write(_1, "Frequency", this.Frequency);
			Write(_1, "FrequencyDuration", this.FrequencyDuration);
			Write(_1, "FrequencyAtten", this.FrequencyAtten);
			Write(_1, "Amplitude", this.Amplitude);
			Write(_1, "AmplitudeAtten", this.AmplitudeAtten);
			Write(_1, "Life", this.Life);
			Write(_1, "MinRange", this.MinRange);
			Write(_1, "MaxRange", this.MaxRange);
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
				case "Type": this.Type = ReadString(_2); break;
				case "Frequency": this.Frequency = ReadInt(_2); break;
				case "FrequencyDuration": this.FrequencyDuration = ReadFloat(_2); break;
				case "FrequencyAtten": this.FrequencyAtten = ReadFloat(_2); break;
				case "Amplitude": this.Amplitude = ReadFloat(_2); break;
				case "AmplitudeAtten": this.AmplitudeAtten = ReadFloat(_2); break;
				case "Life": this.Life = ReadFloat(_2); break;
				case "MinRange": this.MinRange = ReadFloat(_2); break;
				case "MaxRange": this.MaxRange = ReadFloat(_2); break;
			}
		}
	}
}
