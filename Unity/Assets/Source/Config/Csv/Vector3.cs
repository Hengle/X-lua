using System;
using System.Collections.Generic;
using Cfg;

namespace Cfg
{
	public class Vector3 : CfgObject
	{
		/// <summary>
		/// 
		/// <summary>
		public readonly float X;
		/// <summary>
		/// 
		/// <summary>
		public readonly float Y;
		/// <summary>
		/// 
		/// <summary>
		public readonly float Z;

		public Vector3(DataStream data)
		{
			this.X = data.GetFloat();
			this.Y = data.GetFloat();
			this.Z = data.GetFloat();
		}
	}
}
