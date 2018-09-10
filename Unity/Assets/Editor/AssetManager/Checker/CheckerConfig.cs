namespace AssetManager
{
    using System;
    using UnityEngine;
    using Sirenix.Utilities;
    using Sirenix.OdinInspector;
    using UnityEditor;
    using System.IO;

    [GlobalConfig("Editor/AssetManager/Config", UseAsset = true)]
    public class CheckerConfig : GlobalConfig<CheckerConfig>
    {
        [ShowInInspector]
        public string AssetMoveToPath { get { return Application.dataPath + "/../../AssetsBackup"; } }   
        public BitmapCheckerInfo BitmapChecker;  
        public RoleMeshCheckerInfo RoleMeshChecker;
        public FileNameCheckerInfo FileNameChecker;
        public TextureStretchCheckerInfo TextureStretchChecker;

        [Button("Clear Invalid Path", ButtonSizes.Large)]
        public void ClearInvalidPath()
        {

        }
        [Button("Clear All", ButtonSizes.Large)]
        public void ClearAll()
        {
            BitmapChecker.ClearAsset();
            RoleMeshChecker.ClearAsset();
        }
    }
    [Serializable]
    public class CheckerInfo : AssetOption
    {     
        public override string Name { get { return _name; } }

        public CheckerInfo(string name)
        {
            _name = name;
        }
        public virtual void CheckAsset() {
            if (!Directory.Exists(Path))
                EditorUtility.DisplayDialog("错误", "目录路径不存在", "确定");
        }
    }
}