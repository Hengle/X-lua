using UnityEngine;
using System.Collections.Generic;
using XLua;
using System.IO;
using System.Text;
using System.Linq;
using CSObjectWrapEditor;
using System.Diagnostics;
using FairyGUI;

public class LuaApiGen
{
    /// <summary>
    /// 一个Api脚本信息
    /// </summary>
    public class Info
    {
        public string ClassName;
        public string InhertName;
        public List<FieldInfo> Fields = new List<FieldInfo>();
        public List<FuncInfo> Funcs = new List<FuncInfo>();
    }
    public class FieldInfo
    {
        public string Name;
        public string Type;
    }
    public class FuncInfo
    {
        public string Name;
        public List<FieldInfo> ParamNames = new List<FieldInfo>();
        public string ReturnType;
        public List<FuncInfo> Overload = new List<FuncInfo>();
    }

    static readonly string codeDir = Application.dataPath + "/../../Code";
    static readonly string apiDir = codeDir + "/Api";
    static readonly string cacheDir = codeDir + "/Cache";
    static readonly string winRAR = Application.dataPath + "/../../Tool/WinRAR.exe";
    static List<Info> infoList = new List<Info>();

    /// <summary>
    /// Lua Api生成
    /// </summary>
    /// <param name="name">Zip名称</param>
    /// <param name="ls">类型信息列表</param>
    protected static void GenLuaApi(string name, List<Info> ls)
    {
        if (!Directory.Exists(cacheDir))
            Directory.CreateDirectory(cacheDir);
        string[] fs = Directory.GetFiles(cacheDir, "*.lua");
        for (int i = 0; i < fs.Length; i++)
            File.Delete(fs[i]);

        for (int i = 0; i < ls.Count; i++)
        {
            StringBuilder sb = new StringBuilder();
            string className = ls[i].ClassName;
            var fileds = ls[i].Fields;
            for (int j = 0; j < fileds.Count; j++)
                sb.AppendFormat("---@field public {0} {1}\n", fileds[j].Name, fileds[j].Type);
            if (string.IsNullOrEmpty(ls[i].InhertName))
                sb.AppendFormat("---@class {0}\n", className);
            else
                sb.AppendFormat("---@class {0} : {1}\n", className, ls[i].InhertName);
            sb.AppendLine("local m = {}");
            sb.AppendLine();
            var funcs = ls[i].Funcs;
            for (int j = 0; j < funcs.Count; j++)
            {
                var overrides = funcs[i].Overload;
                for (int k = 0; k < overrides.Count; k++)
                {
                    var ps = overrides[i].ParamNames;
                    StringBuilder paramls = new StringBuilder();
                    for (int l = 0; l < ps.Count - 1; l++)
                    {
                        if (ps.Count - 1 == l)
                            paramls.AppendFormat("{0} : {1}", ps[i].Name, ps[l].Type);
                        else
                            paramls.AppendFormat("{0} : {1},", ps[i].Name, ps[l].Type);
                    }

                    sb.AppendFormat("---@overload fun({0}) : {1}\n", paramls.ToString(), overrides[j].ReturnType);
                }
                var cps = funcs[i].ParamNames;
                StringBuilder args = new StringBuilder();
                for (int l = 0; l < cps.Count - 1; l++)
                {
                    sb.AppendFormat("---@param {0} {1}\n", cps[i].Name, cps[i].Type);
                    if (cps.Count - 1 == l)
                        args.AppendFormat("{0}", cps[i].Name);
                    else
                        args.AppendFormat("{0},", cps[i].Name);
                }
                if (!string.IsNullOrEmpty(funcs[i].ReturnType))
                    sb.AppendFormat("---@return {0}\n", funcs[i].ReturnType);
                sb.AppendFormat("function m:{0}({1})end\n", funcs[i].Name, args.ToString());
            }
            string[] nodes = className.Split('.');
            StringBuilder combine = new StringBuilder();
            for (int j = 0; j < nodes.Length; j++)
            {
                if (j == 0)
                {
                    combine.AppendFormat("{0}", nodes[j]);
                    sb.AppendFormat("{0} = {{}}\n", combine);
                }
                else if (0 < j && j < nodes.Length - 1)
                {
                    combine.AppendFormat(".{0}", nodes[j]);
                    sb.AppendFormat("{0} = {{}}\n", combine);
                }
                else
                {
                    combine.AppendFormat(".{0}", nodes[j]);
                    sb.AppendFormat("{0} = m\n", combine);
                }
            }
            sb.AppendFormat("return m\n", className);

            string path = string.Format("{0}/{1}.lua", cacheDir, className);
            File.WriteAllText(path, sb.ToString());
        }

        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = winRAR;
        info.Arguments = string.Format("a -ep {0}/{1}.zip {2}", codeDir, name, cacheDir);
        //UnityEngine.Debug.Log(info.Arguments);
        info.WindowStyle = ProcessWindowStyle.Hidden;
        info.UseShellExecute = Application.platform == RuntimePlatform.WindowsEditor;
        info.ErrorDialog = true;
        Process pro = Process.Start(info);
        pro.WaitForExit();
    }
}
