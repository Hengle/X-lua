namespace AssetManager
{
    using System;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UnityEditor;
    using System.IO;
    using System.Text;
    using System.Linq;

    [Serializable]
    public class TextureImportSetting : AssetOption
    {
        [ShowInInspector, ReadOnly, LabelText("路径"), PropertyOrder(-1)]
        public override string Path
        {
            get
            {
                return base.Path;
            }

            set
            {
                base.Path = value;
            }
        }
        readonly int[] MAX_SIZE = { 32, 64, 128, 256, 512, 1024, 2048, 4096 };
        readonly string[] PLATFORM = { "Window", "Android", "IOS" };

        [BoxGroup("导入设置")]
        public TextureImporterType ImportType = TextureImporterType.Advanced;
        [BoxGroup("导入设置")]
        public TextureImporterNPOTScale TextureNPOT = TextureImporterNPOTScale.ToNearest;
        [BoxGroup("导入设置"), LabelText("Read/Write Enabled")]
        public bool IsReadable = false;
        [BoxGroup("导入设置")]
        public bool IsTransparency = false;
        [BoxGroup("导入设置")]
        public bool GenerateMipmap = false;
        [BoxGroup("导入设置")]
        public TextureWrapMode WrapMode = TextureWrapMode.Clamp;
        [BoxGroup("导入设置")]
        public FilterMode Filter = FilterMode.Bilinear;
        [BoxGroup("导入设置"), Range(0, 16)]
        public int AnisoLevel = 1;

        //---Window
        [TabGroup("导入设置/Platform", "Window"), ValueDropdown("MAX_SIZE"), LabelText("MaxSize")]
        public int WindowMaxSize = 1024;
        [TabGroup("导入设置/Platform", "Window"), LabelText("Format")]
        public TextureImporterFormat WindowImporterFormat = TextureImporterFormat.RGBA32;

        //---Android
        [TabGroup("导入设置/Platform", "Android"), ValueDropdown("MAX_SIZE"), LabelText("MaxSize")]
        public int AndroidMaxSize = 1024;
        [TabGroup("导入设置/Platform", "Android"), LabelText("Format")]
        public TextureImporterFormat AndroidImporterFormat = TextureImporterFormat.RGBA32;

        //---IOS
        [TabGroup("导入设置/Platform", "IOS"), ValueDropdown("MAX_SIZE"), LabelText("MaxSize")]
        public int IOSMaxSize = 1024;
        [TabGroup("导入设置/Platform", "IOS"), LabelText("Format")]
        public TextureImporterFormat IOSImporterFormat = TextureImporterFormat.RGBA32;

        /// <summary>
        /// 按照配置重新导入资源
        /// </summary>
        public override void ReimportAsset()
        {
            base.ReimportAsset();

            ClearAsset();
            List<string> files = new List<string>(Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".png") || f.EndsWith(".jpg") || f.EndsWith(".tga") || f.EndsWith(".psd")));
            for (int i = 0; i < files.Count; i++)
            {
                string refPath = files[i].Replace(Application.dataPath, "Assets").Replace("\\", "/");
                UnityEditor.TextureImporter importer = UnityEditor.TextureImporter.GetAtPath(refPath) as UnityEditor.TextureImporter;
                importer.textureType = ImportType;
                importer.npotScale = TextureNPOT;
                importer.isReadable = IsReadable;
                importer.alphaIsTransparency = IsTransparency;
                importer.mipmapEnabled = GenerateMipmap;
                importer.wrapMode = WrapMode;
                importer.filterMode = Filter;
                importer.anisoLevel = AnisoLevel;

                importer.SetPlatformTextureSettings(BuildTarget.StandaloneWindows.ToString(), WindowMaxSize, WindowImporterFormat);
                importer.SetPlatformTextureSettings(BuildTarget.Android.ToString(), AndroidMaxSize, AndroidImporterFormat, true);
                importer.SetPlatformTextureSettings(BuildTarget.iOS.ToString(), IOSMaxSize, IOSImporterFormat);
                importer.SaveAndReimport();
                EditorUtility.DisplayProgressBar("按配置导入纹理", refPath, (i + 1f) / files.Count);
            }
            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

    }
}