namespace AssetManager
{
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEditor;
    using System.Linq;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using Sirenix.Utilities.Editor;
    using System.Text.RegularExpressions;
    using System.Text;

    [Serializable]
    public class FileNameCheckerInfo : CheckerInfo
    {
        [LabelText("检查目录列表")]
        public List<Folder> PathList = new List<Folder>();

        [Serializable]
        public class Folder
        {
            [FolderPath(RequireValidPath = true)]
            public string Path;
        }

        class IllegalFile
        {
            public string Path;
            public bool HasEmpty;//包含空字符
            public StringBuilder IllegalFlag; //非法字符
        }



        [OnInspectorGUI]
        void OnInspectorGUI()
        {
            GUILayout.Space(15f);

            if (_illegalFileInfos != null)
                SirenixEditorGUI.MessageBox(string.Format("异常文件数量{0}个", _illegalFileInfos.Count), MessageType.Info);

            SirenixEditorGUI.BeginHorizontalToolbar();
            GUILayout.Label("文件名", SirenixGUIStyles.BoldLabel, GUILayout.Width(650));
            GUILayout.Label("包含空字符", SirenixGUIStyles.BoldLabel);
            GUILayout.Label("非法字符", SirenixGUIStyles.BoldLabel);
            SirenixEditorGUI.EndHorizontalToolbar();

            if (_illegalFileInfos == null) return;
            GUILayout.Space(-5);
            SirenixEditorGUI.BeginBox();
            for (int i = 0; i < _illegalFileInfos.Count; i++)
            {
                var item = _illegalFileInfos[i];
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(item.Path, SirenixGUIStyles.Label, GUILayout.Width(650));

                GUILayout.Label(item.HasEmpty.ToString(), SirenixGUIStyles.LabelCentered, GUILayout.Width(50));
                GUILayout.Label(item.IllegalFlag.ToString(), SirenixGUIStyles.LabelCentered);
                EditorGUILayout.EndHorizontal();
                if (i + 1 != _illegalFileInfos.Count)
                {
                    GUILayout.Space(3);
                    SirenixEditorGUI.HorizontalLineSeparator();
                }
            }
            SirenixEditorGUI.EndBox();
        }

        public FileNameCheckerInfo(string name) : base(name) { }

        const string _regex1 = @"[\s]";
        const string _regex2 = @"[^A-Za-z_0-9]";
        List<IllegalFile> _illegalFileInfos = new List<IllegalFile>();
        public override void CheckAsset()
        {
            ClearAsset();

            List<string> files = new List<string>();
            for (int i = 0; i < PathList.Count; i++)
            {
                string path = GetAbsPath(PathList[i].Path);
                if (!Directory.Exists(path)) continue;
                files.AddRange(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories));
                EditorUtility.DisplayProgressBar("获取文件", PathList[i].Path, (i + 1f) / PathList.Count);
            }
            files.RemoveAll(f => f.EndsWith(".meta"));

            float count = 0;
            foreach (var f in files)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(f);
                EditorUtility.DisplayProgressBar("文件命名检查", fileName, ++count / files.Count);

                var resutlt1 = Regex.Matches(fileName, _regex1);
                var resutlt2 = Regex.Matches(fileName, _regex2);
                if (resutlt1.Count == 0 && resutlt2.Count == 0) continue;

                IllegalFile illegal = new IllegalFile();
                illegal.Path = GetUnityPath(f);
                illegal.HasEmpty = resutlt1.Count > 0;
                foreach (var item in resutlt2)
                {
                    if (illegal.IllegalFlag == null)
                    {
                        illegal.IllegalFlag = new StringBuilder();
                        illegal.IllegalFlag.AppendFormat("{0}", item);
                    }
                    else
                    {
                        illegal.IllegalFlag.AppendFormat(" {0}", item);
                    }
                }
                _illegalFileInfos.Add(illegal);
            }

            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
        public override void ClearAsset()
        {
            base.ClearAsset();
            if (_illegalFileInfos == null)
                _illegalFileInfos = new List<IllegalFile>();
            _illegalFileInfos.Clear();
        }
    }
}