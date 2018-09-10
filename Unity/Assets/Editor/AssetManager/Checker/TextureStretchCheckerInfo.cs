namespace AssetManager
{
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEditor;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using Sirenix.Utilities.Editor;
    using System.Drawing;

    [Serializable]
    public class TextureStretchCheckerInfo : CheckerInfo
    {
        [LabelText("检查目录列表")]
        public List<Folder> PathList = new List<Folder>();

        [Serializable]
        public class Folder
        {
            [FolderPath(RequireValidPath = true)]
            public string Path;
        }

        class TextureInfo
        {
            public string Path;
            public string Before;
            public string After;
        }

        [OnInspectorGUI]
        void OnInspectorGUI()
        {
            GUILayout.Space(15f);

            if (_textureInfos != null)
                SirenixEditorGUI.MessageBox(string.Format("异常文件数量{0}个", _textureInfos.Count), MessageType.Info);

            SirenixEditorGUI.BeginHorizontalToolbar();
            GUILayout.Label("文件名", SirenixGUIStyles.BoldLabel, GUILayout.Width(650));
            GUILayout.Label("原始像素尺寸", SirenixGUIStyles.BoldLabel);
            GUILayout.Label("Unity像素尺寸", SirenixGUIStyles.BoldLabel);
            SirenixEditorGUI.EndHorizontalToolbar();

            if (_textureInfos == null) return;
            GUILayout.Space(-5);
            SirenixEditorGUI.BeginBox();
            for (int i = 0; i < _textureInfos.Count; i++)
            {
                var item = _textureInfos[i];
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(item.Path, SirenixGUIStyles.Label, GUILayout.Width(650));

                GUILayout.Label(item.Before, SirenixGUIStyles.Label);
                GUILayout.Label(item.After, SirenixGUIStyles.Label);
                EditorGUILayout.EndHorizontal();
                if (i + 1 != _textureInfos.Count)
                {
                    GUILayout.Space(3);
                    SirenixEditorGUI.HorizontalLineSeparator();
                }
            }
            SirenixEditorGUI.EndBox();
        }

        public TextureStretchCheckerInfo(string name) : base(name) { }


        static string[] _pattern = { "*.png", "*.jpg", "*.tga" };
        List<TextureInfo> _textureInfos = new List<TextureInfo>();
        public override void CheckAsset()
        {
            ClearAsset();

            List<string> files = new List<string>();
            for (int j = 0; j < PathList.Count; j++)
            {
                string path = GetAbsPath(PathList[j].Path);
                if (!Directory.Exists(path)) continue;
                for (int i = 0; i < _pattern.Length; i++)
                {
                    files.AddRange(Directory.GetFiles(path, _pattern[i], SearchOption.AllDirectories));
                }
                EditorUtility.DisplayProgressBar("获取文件", PathList[j].Path, (j + 1f) / _pattern.Length);
            }

            float count = 0;
            foreach (var f in files)
            {
                string path = GetUnityPath(f);
                EditorUtility.DisplayProgressBar("图片拉伸检查", path, ++count / files.Count);
                int bWidth, bHeight, aWidth, aHeight;
                using (FileStream fs = new FileStream(f, FileMode.Open, FileAccess.Read))
                {
                    Image image = Image.FromStream(fs);
                    bWidth = image.Width;
                    bHeight = image.Height;
                    Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                    aWidth = texture.width;
                    aHeight = texture.height;

                    if (bHeight > aHeight || bWidth > aWidth)
                    {
                        var info = new TextureInfo();
                        info.Path = path;
                        info.Before = string.Format("{0}x{1}", bWidth, bHeight);
                        info.After = string.Format("{0}x{1}", aWidth, aHeight);
                        _textureInfos.Add(info);
                    }
                }
            }

            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
        public override void ClearAsset()
        {
            base.ClearAsset();
            if (_textureInfos == null)
                _textureInfos = new List<TextureInfo>();
            _textureInfos.Clear();
        }
    }
}