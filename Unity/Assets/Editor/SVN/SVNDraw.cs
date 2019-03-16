using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SVNDraw
{
    public static void DrawGenericMenu(ref GenericMenu menu, string rootPath, Func<string> selected)
    {
        menu.AddItem(new GUIContent("全部更新"), false, () => SVNTool.UpdateAtPath(rootPath));
        menu.AddItem(new GUIContent("全部提交"), false, () => SVNTool.CommitAtPath(rootPath));
        menu.AddItem(new GUIContent("全部还原"), false, () => SVNTool.RevertAtPath(rootPath));
        menu.AddItem(new GUIContent("全部解决"), false, () => SVNTool.ResolveAtPath(rootPath));
        if (selected != null)
        {
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("更新当前"), false, () => SVNTool.UpdateAtPath(selected()));
            menu.AddItem(new GUIContent("提交当前"), false, () => SVNTool.CommitAtPath(selected()));
            menu.AddItem(new GUIContent("还原当前"), false, () => SVNTool.RevertAtPath(selected()));
            menu.AddItem(new GUIContent("解决当前"), false, () => SVNTool.ResolveAtPath(selected()));
        }
    }
}
