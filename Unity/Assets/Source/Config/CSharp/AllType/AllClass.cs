using System;
using System.Collections.Generic;
using Cfg;

namespace AllType
{
	/// <summary>
	/// 
	/// <summary>
	public class AllClass : CfgObject
	{
		/// <summary>
		/// 
		/// <summary>
		public  readonly string ConstString = @"Hello World";
		/// <summary>
		/// 
		/// <summary>
		public  readonly float ConstFloat = 3.141527f;
		/// <summary>
		/// 
		/// <summary>
		public readonly int ID;
		/// <summary>
		/// 
		/// <summary>
		public readonly long VarLong;
		/// <summary>
		/// 
		/// <summary>
		public readonly float VarFloat;
		/// <summary>
		/// 
		/// <summary>
		public readonly string VarString;
		/// <summary>
		/// 
		/// <summary>
		public readonly bool VarBool;
		/// <summary>
		/// 
		/// <summary>
		public readonly AllType.CardElement VarEnum;
		/// <summary>
		/// 
		/// <summary>
		public readonly AllType.SingleClass VarClass;
		/// <summary>
		/// 
		/// <summary>
		public readonly List<string> VarListBase = new List<string>();
		/// <summary>
		/// 
		/// <summary>
		public readonly List<AllType.SingleClass> VarListClass = new List<AllType.SingleClass>();
		/// <summary>
		/// 
		/// <summary>
		public readonly List<string> VarListCardElem = new List<string>();
		/// <summary>
		/// 
		/// <summary>
		public readonly Dictionary<int, string> VarDictBase = new Dictionary<int, string>();
		/// <summary>
		/// 
		/// <summary>
		public readonly Dictionary<long, AllType.CardElement> VarDictEnum = new Dictionary<long, AllType.CardElement>();
		/// <summary>
		/// 
		/// <summary>
		public readonly Dictionary<string, AllType.SingleClass> VarDictClass = new Dictionary<string, AllType.SingleClass>();
		
		public AllClass(DataStream data)
		{
			ID = data.GetInt();
			VarLong = data.GetLong();
			VarFloat = data.GetFloat();
			VarString = data.GetString();
			VarBool = data.GetBool();
			VarEnum = (AllType.CardElement)data.GetInt();
			VarClass = (AllType.SingleClass)data.GetObject(data.GetString());
			for (int n = data.GetInt(); n-- > 0;)
			{
				string v = data.GetString();
				VarListBase.Add(v);
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				AllType.SingleClass v = (AllType.SingleClass)data.GetObject(data.GetString());
				VarListClass.Add(v);
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				string v = data.GetString();
				VarListCardElem.Add(v);
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				int k = data.GetInt();
				VarDictBase[k] = data.GetString();
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				long k = data.GetLong();
				VarDictEnum[k] = (AllType.CardElement)data.GetInt();
			}
			for (int n = data.GetInt(); n-- > 0;)
			{
				string k = data.GetString();
				VarDictClass[k] = (AllType.SingleClass)data.GetObject(data.GetString());
			}
		}
	}
}
