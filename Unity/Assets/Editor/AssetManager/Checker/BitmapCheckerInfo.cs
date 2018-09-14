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

    [Serializable]
    public class BitmapCheckerInfo : CheckerInfo
    {
        [ShowInInspector, PropertyOrder(-100)]
        [FolderPath(RequireValidPath = true), LabelText("图集路径")]
        public override string Path
        {
            get { return base.Path; }
            set { base.Path = value; }
        }
#if ASSET_MGR_NGUI
        [FolderPath(RequireValidPath = true)]
        [ShowInInspector, PropertyOrder(-99), LabelText("位图路径")]
        public string BitmapPath;
#endif
        class BitmapDelete
        {
            public string Path;
            public bool AtlasExist;

            /// <summary>
            /// 移出项目
            /// </summary>
            public void Delete()
            {
                string srcPath = GetAbsPath(Path);
                string dstPath = srcPath.Replace(Application.dataPath, CheckerConfig.Instance.AssetMoveToPath);
                CreateFolder(dstPath);
                if (AtlasExist && File.Exists(srcPath))
                {
                    if (File.Exists(dstPath))
                        File.Delete(dstPath);
                    File.Move(srcPath, dstPath);
                }
                if (!AtlasExist && Directory.Exists(srcPath))
                {
                    if (Directory.Exists(dstPath))
                        Directory.Delete(dstPath);
                    Directory.Move(srcPath, dstPath);
                }
                Path = null;
            }
        }

        //[ButtonGroup("Delete", -80), Button(ButtonSizes.Medium)]
        //public void SelectedDelete()
        //{
        //    for (int i = 0; i < spriteBitmaps.Count; i++)
        //    {
        //        BitmapDelete delete = spriteBitmaps[i];
        //        if (!delete.AtlasExist && delete.IsDeletable)
        //            delete.Delete();
        //        if (delete.AtlasExist && delete.IsDeletable)
        //            delete.Delete();
        //    }
        //    RefreshAsset();
        //    AssetDatabase.Refresh();
        //}
        [ButtonGroup("Delete", -80), Button(ButtonSizes.Medium), LabelText("删除文件")]
        public void DeleteFileImmediate()
        {
            for (int i = 0; i < _spriteBitmaps.Count; i++)
            {
                BitmapDelete delete = _spriteBitmaps[i];
                if (delete.AtlasExist)
                    delete.Delete();
            }
            RefreshAsset();
            AssetDatabase.Refresh();
        }
        [ButtonGroup("Delete", -80), Button(ButtonSizes.Medium), LabelText("删除文件夹")]
        public void DeleteFolderImmediate()
        {
            for (int i = 0; i < _spriteBitmaps.Count; i++)
            {
                BitmapDelete delete = _spriteBitmaps[i];
                if (!delete.AtlasExist)
                    delete.Delete();
            }
            RefreshAsset();
            AssetDatabase.Refresh();
        }

        List<BitmapDelete> _spriteBitmaps = new List<BitmapDelete>();

        [OnInspectorGUI]
        void OnInspectorGUI()
        {

            if (_spriteBitmaps != null)
                SirenixEditorGUI.MessageBox(string.Format("异常文件数量{0}个", _spriteBitmaps.Count), MessageType.Info);

            SirenixEditorGUI.BeginHorizontalToolbar();
            GUILayout.Label("文件名", SirenixGUIStyles.BoldLabel);
            GUILayout.Label("图集是否存在", SirenixGUIStyles.BoldLabel, GUILayout.Width(90));
            GUILayout.Label("操作", SirenixGUIStyles.BoldLabelCentered, GUILayout.Width(70));
            SirenixEditorGUI.EndHorizontalToolbar();

            if (_spriteBitmaps == null) return;
            GUILayout.Space(-5);
            SirenixEditorGUI.BeginBox();
            for (int i = 0; i < _spriteBitmaps.Count; i++)
            {
                var item = _spriteBitmaps[i];
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(item.Path, SirenixGUIStyles.Label);

                GUIHelper.PushColor(item.AtlasExist ? Color.green : Color.red);
                GUILayout.Label(item.AtlasExist ? "有" : "无", SirenixGUIStyles.Label, GUILayout.Width(70));
                GUIHelper.PopColor();
                GUILayout.Space(-9);

                GUILayout.Button("删除", SirenixGUIStyles.Button, GUILayout.Width(70));
                EditorGUILayout.EndHorizontal();
                if (i + 1 != _spriteBitmaps.Count)
                {
                    GUILayout.Space(3);
                    SirenixEditorGUI.HorizontalLineSeparator();
                }
            }
            SirenixEditorGUI.EndBox();
        }

        public BitmapCheckerInfo(string name) : base(name) { }

        public override void CheckAsset()
        {
            base.CheckAsset();

#if ASSET_MGR_NGUI
            if (!Directory.Exists(BitmapPath))
            EditorUtility.DisplayDialog("错误", "位图目录路径不存在", "确定");
            ClearAsset();
            Dictionary<string, HashSet<string>> pairs = new Dictionary<string, HashSet<string>>();
            string[] prefabs = Directory.GetFiles(GetAbsPath(Path), "*.prefab");
            foreach (var file in prefabs)
            {
                string unityPath = GetUnityPath(file);
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(unityPath);
                UIAtlas atlas = go.GetComponent<UIAtlas>();
                var sprites = atlas.GetListOfSprites();
                pairs.Add(atlas.name, new HashSet<string>(sprites.ToArray()));
            }

            string[] bitmaps = Directory.GetDirectories(GetAbsPath(BitmapPath));
            foreach (var item in bitmaps)
            {
                List<string> sprites = new List<string>(Directory.GetFiles(item, "*.*", SearchOption.AllDirectories)
                    .Where(f => f.EndsWith(".png")));
                string atlasName = System.IO.Path.GetFileName(item);
                if (!pairs.ContainsKey(atlasName))
                {
                    BitmapDelete delete = new BitmapDelete();
                    delete.Path = GetUnityPath(item);
                    delete.AtlasExist = false;
                    _spriteBitmaps.Add(delete);
                }
                else
                {
                    HashSet<string> atlas = pairs[atlasName];
                    foreach (var sprite in sprites)
                    {
                        string name = System.IO.Path.GetFileNameWithoutExtension(sprite);
                        if (!atlas.Contains(name))
                        {
                            BitmapDelete delete = new BitmapDelete();
                            delete.Path = GetUnityPath(sprite);
                            delete.AtlasExist = true;
                            _spriteBitmaps.Add(delete);
                        }
                    }
                }
            }
#elif ASSET_MGR_UGUI
            
#endif
            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
        public override void ClearAsset()
        {
            base.ClearAsset();
            if (_spriteBitmaps == null)
                _spriteBitmaps = new List<BitmapDelete>();
            _spriteBitmaps.Clear();
        }
    }
}