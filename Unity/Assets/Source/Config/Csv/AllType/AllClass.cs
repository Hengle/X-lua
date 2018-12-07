using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg.AllType
{
	public class AllClass : CfgObject
	{
		/// <summary>
		/// 常量字符串
		/// <summary>
		public const string ConstString = @"Hello World";
		/// <summary>
		/// 常量浮点值
		/// <summary>
		public const float ConstFloat = 3.141527f;
		/// <summary>
		/// ID
		/// <summary>
		public readonly int ID;
		/// <summary>
		/// 长整型
		/// <summary>
		public readonly long VarLong;
		/// <summary>
		/// 浮点型
		/// <summary>
		public readonly float VarFloat;
		/// <summary>
		/// 字符串
		/// <summary>
		public readonly string VarString;
		/// <summary>
		/// 布尔型
		/// <summary>
		public readonly bool VarBool;
		/// <summary>
		/// 枚举类型
		/// <summary>
		public readonly int VarEnum;
		/// <summary>
		/// 类类型
		/// <summary>
		public Cfg.AllType.SingleClass VarClass;
		/// <summary>
		/// 字符串列表
		/// <summary>
		public readonly List<string> VarListBase = new List<string>();
		/// <summary>
		/// Class列表
		/// <summary>
		public readonly List<SingleClass> VarListClass = new List<SingleClass>();
		/// <summary>
		/// 字符串列表
		/// <summary>
		public readonly List<string> VarListCardElem = new List<string>();
		/// <summary>
		/// 基础类型字典
		/// <summary>
		public readonly Dictionary<int, string> VarDictBase = new Dictionary<int, string>();
		/// <summary>
		/// 枚举类型字典
		/// <summary>
		public readonly Dictionary<long, CardElement> VarDictEnum = new Dictionary<long, CardElement>();
		/// <summary>
		/// 类类型字典
		/// <summary>
		public readonly Dictionary<string, SingleClass> VarDictClass = new Dictionary<string, SingleClass>();

		public AllClass(DataStream data)
		{
			this.ID = data.GetInt();
			this.VarLong = data.GetLong();
			this.VarFloat = data.GetFloat();
			this.VarString = data.GetString();
			this.VarBool = data.GetBool();
			this.VarEnum = data.GetInt();
			this.VarClass = (AllType.SingleClass)data.GetObject(data.GetString());
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.VarListBase.Add(data.GetString());
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.VarListClass.Add((SingleClass)data.GetObject(data.GetString()));
			}
			for (int n = data.GetInt(); n-- > 0; )
			{
				this.VarListCardElem.Add(data.GetString());
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				int k = data.GetInt();
				this.VarDictBase[k] = data.GetString();
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				long k = data.GetLong();
				this.VarDictEnum[k] = (AllType.CardElement)data.GetInt();
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				string k = data.GetString();
				this.VarDictClass[k] = (SingleClass)data.GetObject(data.GetString());
			}
		}
	}
}
