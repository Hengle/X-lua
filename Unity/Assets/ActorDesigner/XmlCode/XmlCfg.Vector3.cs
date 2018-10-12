using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace XmlCfg
{
	public  class Vector3 : XmlObject
	{

		public override void Write(TextWriter _1)
		{
		}

		public override void Read(XmlNode _1)
		{
			foreach (System.Xml.XmlNode _2 in GetChilds (_1))
			switch (_2.Name)
			{
			}
		}
	}
}
