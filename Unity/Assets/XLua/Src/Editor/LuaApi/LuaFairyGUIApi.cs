﻿using FairyGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LuaFairyGUIApi : LuaApiGen
{
    static string fairyGUIDir = Application.dataPath + "/Interface/GUI";
    static void GenSelectedFairyGUIApi()
    {

    }
    [MenuItem("XLua/Gen FairyGUI Api", false, 500)]
    static void GenFairyGUIApi()
    {
        List<LuaApi> infos = new List<LuaApi>();
        string[] fs = Directory.GetFiles(fairyGUIDir, "*.bytes");
        for (int i = 0; i < fs.Length; i++)
        {
            string path = EditorUtil.FilePath2UnityPath(fs[i]);
            int index = path.LastIndexOf('_');
            string f = EditorUtil.StandardlizePath(path).Substring(0, index);
            infos.AddRange(CreateInfo(f));
        }
        GenLuaApi("FairyGUIApi", infos);
    }

    static List<LuaApi> CreateInfo(string path)
    {
        List<LuaApi> infos = new List<LuaApi>();
        try
        {
            var pkg = UIPackage.AddPackage(path);
            var items = pkg.GetItems();
            foreach (var item in items)
            {
                GComponent gcomp = pkg.CreateObject(item.name).asCom;
                LuaApi info = new LuaApi();
                info.ClassName = string.Format("UI.{0}.{1}", pkg.name, item.name);
                CollectUIElements(item.name, gcomp, info);
                if (info.Fields.Count > 0)
                    infos.Add(info);
            }
        }
        catch (Exception e)
        {
            UIPackage.RemoveAllPackages();
            Debug.LogError(e.Message + "\n" + e.StackTrace);
        }
        return infos;
    }
    static Dictionary<string, int> prefixs = new Dictionary<string, int>()
    {
        {"Label", 1},
        {"Image", 1},
        {"Button", 1},
        {"ProgressBar", 1},
        {"Slider", 1},
        {"TextField", 1},
        {"RichTextField", 1},
        {"TextInput", 1},
        {"Loader", 1},
        {"Graph", 1},
        {"MovieClip", 1},

        { "ComboBox", 2},
        {"Group", 2},
        {"List", 2},
    };
    static void CollectUIElements(string dlg, GComponent comp, LuaApi info)
    {
        GObject[] children = comp.GetChildren();
        for (int i = 0; i < children.Length; i++)
        {
            GObject child = children[i];
            string name = child.name;
            int pos = name.IndexOf('_');
            bool isGroup = false;
            if (pos > 1)
            {
                string prefix = name.Substring(0, pos);
                if (prefixs.ContainsKey(prefix))
                {
                    int typev = prefixs[prefix];
                    isGroup = typev == 2;
                    Type type = child.GetType();
                    var property = type.GetProperty(prefix);
                    FieldInfo field = new FieldInfo();
                    field.Name = name;
                    string cType = child.GetType().ToString();
                    field.Type = cType;
                    string mType = "FairyGUI.G" + prefix;
                    if (cType == "MyLoader" && mType != "GLoader")
                        Debug.LogErrorFormat("[{0}]{1}的类型({2})与实际需求({3})不匹配", dlg, name, cType, mType);
                    else if (cType != mType)
                        Debug.LogErrorFormat("[{0}]{1}的类型({2})与实际需求({3})不匹配", dlg, name, cType, mType);
                    info.Fields.Add(field);
                }
            }
            if (!isGroup && child.asCom != null && child.asCom.numChildren > 0)
                CollectUIElements(dlg + 1, child.asCom, info);
            child.Dispose();
        }
        comp.Dispose();
    }
}