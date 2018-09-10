namespace AssetManager
{
    using System;
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEditor;
    using System.IO;
    using System.Text;
    using System.Linq;

    [Serializable]
    public class AudioClipImportSetting : AssetOption
    {
        [ShowInInspector, ReadOnly, PropertyOrder(-1)]
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
        const string OGG = ".ogg";
        const string MP3 = ".mp3";
        readonly string PLATFORM = BuildTarget.StandaloneWindows.ToString();

        [FoldoutGroup("导入设置")]
        public AudioClipLoadType LoadType = AudioClipLoadType.DecompressOnLoad;
        [FoldoutGroup("导入设置")]
        public bool PreloadAudioData = true;
        [FoldoutGroup("导入设置")]
        public AudioCompressionFormat CompressionFormat = AudioCompressionFormat.Vorbis;
        [FoldoutGroup("导入设置"), Range(0, 100)]
        public int Quality = 100;
        [FoldoutGroup("导入设置")]
        public AudioSampleRateSetting SampleRateSetting = AudioSampleRateSetting.PreserveSampleRate;

        [Serializable]
        class AssetInfo
        {
            [TableColumnWidth(280), ReadOnly]
            public string FileName;
            [TableColumnWidth(150), ReadOnly]
            public AudioClipLoadType LoadType;
            [TableColumnWidth(100), ReadOnly]
            public string PreloadData;
            [TableColumnWidth(120), ReadOnly]
            public AudioCompressionFormat CompressFormat;
            [TableColumnWidth(70), ReadOnly]
            public string Quality;
            [TableColumnWidth(150), ReadOnly]
            public AudioSampleRateSetting SampleSetting;
            [ReadOnly]
            public string Other;
        }

        /// <summary>
        /// 按照配置重新导入资源
        /// </summary>
        public override void ReimportAsset()
        {
            base.ReimportAsset();

            ClearAsset();
            List<string> files = new List<string>(Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(OGG) || f.EndsWith(MP3)));
            for (int i = 0; i < files.Count; i++)
            {
                string refPath = files[i].Replace(Application.dataPath, "Assets").Replace("\\", "/");
                AudioImporter importer = AudioImporter.GetAtPath(refPath) as AudioImporter;
                AudioImporterSampleSettings aiss = importer.GetOverrideSampleSettings(PLATFORM);

                aiss.loadType = LoadType;
                importer.preloadAudioData = PreloadAudioData;
                aiss.compressionFormat = CompressionFormat;
                aiss.quality = Quality / 100f;
                aiss.sampleRateSetting = SampleRateSetting;

                importer.SetOverrideSampleSettings(BuildTarget.StandaloneWindows.ToString(), aiss);
                importer.SetOverrideSampleSettings(BuildTarget.Android.ToString(), aiss);
                importer.SetOverrideSampleSettings(BuildTarget.iOS.ToString(), aiss);
                importer.SaveAndReimport();
                EditorUtility.DisplayProgressBar("按配置导入音频", refPath, (i + 1f) / files.Count);
            }
            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
    }
}