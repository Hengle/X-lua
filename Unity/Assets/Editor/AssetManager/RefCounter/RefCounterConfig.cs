namespace AssetManager
{
    using System;
    using System.IO;
    using System.Text;
    using UnityEditor;
    using UnityEngine;
    using Sirenix.Utilities;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using Sirenix.Utilities.Editor;

    [GlobalConfig("Editor/AssetManager/Config", UseAsset = true)]
    public class RefCounterConfig : GlobalConfig<RefCounterConfig>
    {
        public RefCounterInfo AtlasSprite;
        public RefCounterInfo TextureUI;
        public RefCounterInfo TextureSFX;
        public RefCounterInfo TextureCharacter;
        public RefCounterInfo AnimationClip;
        public RefCounterInfo AudioClip;

        private List<RefCounterInfo> _list
        {
            get
            {
                List<RefCounterInfo> list = new List<RefCounterInfo>();
                list.Add(AtlasSprite);
                list.Add(TextureUI);
                list.Add(TextureSFX);
                list.Add(TextureCharacter);
                list.Add(AnimationClip);
                list.Add(AudioClip);
                return list;
            }
        }

        [Button("Clear Invalid Path", ButtonSizes.Large)]
        public void ClearInvalidPath()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (!Directory.Exists(_list[i].Path))
                {
                    _list[i].ClearAsset();
                    _list[i].Path = string.Empty;
                    _list[i].OptPath = string.Empty;
                    _list[i].OnlyShowZeroRef = false;
                }
            }
        }
        [Button("Clear All", ButtonSizes.Large)]
        public void ClearAll()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].ClearAsset();
            }
        }
    }
    [Serializable]
    public class RefCounterInfo : AssetOption
    {
        [ShowInInspector, PropertyOrder(-100), OnValueChanged("OnValueChanged"), LabelText("路径")]
        [FolderPath(RequireValidPath = true)]
        public override string Path { get { return base.Path; } set { base.Path = value; } }
        [FolderPath(RequireValidPath = true), HorizontalGroup("Option"), LabelText("额外路径")]
        public string OptPath;
        [HorizontalGroup("Option", 20, 10), LabelText("仅显示0引用"), ToggleLeft, LabelWidth(50)]
        public bool OnlyShowZeroRef;

        [HideInInspector]
        public string Description = string.Empty;
        List<RefCounter> _refInfoList = new List<RefCounter>();

        private void OnValueChanged()
        {
            _refInfoList = new List<RefCounter>();
        }

        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            if (!string.IsNullOrEmpty(Description))
                SirenixEditorGUI.MessageBox(Description, MessageType.Info);

            SirenixEditorGUI.BeginHorizontalToolbar();
            GUILayout.Label("文件名", SirenixGUIStyles.BoldLabel);
            GUILayout.Label("引用数量", SirenixGUIStyles.BoldLabel, GUILayout.Width(90));
            GUILayout.Label("内存大小", SirenixGUIStyles.BoldLabel, GUILayout.Width(70));
            SirenixEditorGUI.EndHorizontalToolbar();

            if (_refInfoList == null) return;
            GUILayout.Space(-5);
            SirenixEditorGUI.BeginBox();
            for (int i = 0; i < _refInfoList.Count; i++)
            {
                var item = _refInfoList[i];
                if (OnlyShowZeroRef && item.RefNum != 0) continue;

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(item.FileName, SirenixGUIStyles.Label);
                GUILayout.Label(item.RefNum.ToString(), SirenixGUIStyles.Label, GUILayout.Width(70));
                GUILayout.Label(EditorUtility.FormatBytes(item.Size), SirenixGUIStyles.Label, GUILayout.Width(70));
                EditorGUILayout.EndHorizontal();
                if (i + 1 != _refInfoList.Count)
                {
                    GUILayout.Space(3);
                    SirenixEditorGUI.HorizontalLineSeparator();
                }
            }
            SirenixEditorGUI.EndBox();
        }

        public override void ClearAsset()
        {
            _refInfoList.Clear();
            Description = string.Empty;
        }

        public RefCounterInfo(string name)
        {
            _name = name;
        }

        [Serializable]
        class RefCounter
        {
            [FilePath(RequireValidPath = true), ReadOnly]
            public string FileName;
            [ReadOnly]
            public int RefNum;
            [ReadOnly]
            public int Size;
        }

        public void SortName()
        {
            int count = 0;
            int all = (int)(_refInfoList.Count * Mathf.Log(_refInfoList.Count, 2)) + _refInfoList.Count;
            _refInfoList.Sort((a, b) =>
            {
                EditorUtility.DisplayProgressBar("按照名称排序", count.ToString(), (++count * 1f) / all);
                return string.Compare(a.FileName, b.FileName);
            });
            EditorUtility.ClearProgressBar();
        }
        public void SortRefNum()
        {
            int count = 0;
            int all = (int)(_refInfoList.Count * Mathf.Log(_refInfoList.Count, 2)) + _refInfoList.Count;
            _refInfoList.Sort((a, b) =>
            {
                EditorUtility.DisplayProgressBar("按照引用数量排序", count.ToString(), (++count * 1f) / all);
                return a.RefNum - b.RefNum;
            });
            EditorUtility.ClearProgressBar();
        }
        public void SortSize()
        {
            int count = 0;
            int all = (int)(_refInfoList.Count * Mathf.Log(_refInfoList.Count, 2)) + _refInfoList.Count;
            _refInfoList.Sort((a, b) =>
            {
                EditorUtility.DisplayProgressBar("按照内存大小排序", count.ToString(), (++count * 1f) / all);
                return a.Size - b.Size;
            });
            EditorUtility.ClearProgressBar();
        }

        //StringBuilder builder = new StringBuilder();
        //Transform parent = sprite.transform.parent;
        //string nodePtah = string.Format("{0}/{1}", parent.name, sprite.name);
        //while (parent.parent != null)
        //{
        //    parent = parent.parent;
        //    nodePtah = string.Format("{0}/{1}", parent.name, nodePtah);
        //}
        //builder.AppendFormat("预制节点路径:{0} 图集图片:{1}\n", nodePtah, file);

        //Profiler.GetRuntimeMemorySize(sprite.mainTexture);内存大小
        //---------各种资源的统计方法
        public static void CountAtlasSprite(RefCounterInfo atlasSprite)
        {
            atlasSprite._refInfoList.Clear();
            Dictionary<string, RefCounter> pairs = new Dictionary<string, RefCounter>();
            HashSet<string> atlasHash = new HashSet<string>();
            HashSet<string> spriteHash = new HashSet<string>();
            string path = GetAbsPath(atlasSprite.Path);
            string optPath = GetAbsPath(atlasSprite.OptPath);
            string[] atlasFile = Directory.GetFiles(optPath, "*.prefab", SearchOption.AllDirectories);
            string[] uiFile = Directory.GetFiles(path, "*.prefab", SearchOption.AllDirectories);

#if ASSET_MGR_NGUI
            foreach (var file in atlasFile)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(GetUnityPath(file));
                UIAtlas atlas = prefab.GetComponent<UIAtlas>();
                foreach (var sprite in atlas.spriteList)
                {
                    string fileName = string.Format("{0}/{1}", atlas.name, sprite.name);
                    RefCounter counter = new RefCounter();
                    counter.FileName = fileName;
                    counter.RefNum = 0;
                    counter.Size = 0;
                    pairs.Add(fileName, counter);
                }
            }

            for (int i = 0; i < uiFile.Length; i++)
            {
                path = GetUnityPath(uiFile[i]);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab == null) continue;
                UISprite[] sprites = prefab.GetComponentsInChildren<UISprite>();
                for (int j = 0; j < sprites.Length; j++)
                {
                    UISprite sprite = sprites[j];
                    if (sprite.atlas == null) continue;
                    if (!atlasHash.Contains(sprite.atlas.name)) atlasHash.Add(sprite.atlas.name);
                    if (!spriteHash.Contains(sprite.name)) spriteHash.Add(sprite.name);

                    string file = string.Format("{0}/{1}", sprite.atlas.name, sprite.spriteName);
                    if (pairs.ContainsKey(file))
                    {
                        pairs[file].RefNum++;
                    }
                }
                EditorUtility.DisplayProgressBar("解析预制", path, (i * 1f) / uiFile.Length);
            }
#elif ASSET_MGR_UGUI
            
#endif
            EditorUtility.ClearProgressBar();
            atlasSprite.Description = string.Format("图集预制总数:{0}\t 图集预制实际引用数:{1}\t 图集精灵总数量:{2}", atlasFile.Length, atlasHash.Count, spriteHash.Count);
            atlasSprite._refInfoList.AddRange(pairs.Values);
            atlasSprite.SortRefNum();
            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
        public static void CountTextureUI(RefCounterInfo atlasSprite)
        {

        }
        public static void CountTextureSFX(RefCounterInfo atlasSprite)
        {

        }
        public static void CountTextureCharacter(RefCounterInfo atlasSprite)
        {

        }
        public static void CountAnimationClip(RefCounterInfo atlasSprite)
        {

        }
        public static void CountAudioClip(RefCounterInfo atlasSprite)
        {

        }
    }
}