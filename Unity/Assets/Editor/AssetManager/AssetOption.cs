namespace AssetManager
{
    using System;
    using System.IO;
    using UnityEngine;

    [Serializable]
    public class AssetOption
    {
        [SerializeField, HideInInspector]
        protected string _path;
        [SerializeField, HideInInspector]
        protected string _name;

        public virtual string Path { get { return _path; } set { _path = value; } }
        public virtual string Name
        {
            get
            {
                string n = System.IO.Path.GetFileName(Path);
                return string.IsNullOrEmpty(n) ? "未命名" : n;
            }
        }
        public bool IncludeSub { get; set; }

        public virtual void ClearAsset() { }
        public virtual void RefreshAsset()
        {

        }
        public virtual void ReimportAsset()
        {

        }

        public static string GetUnityPath(string path)
        {
            return path.Replace("\\", "/").Replace(Application.dataPath, "Assets");
        }
        public static string GetAbsPath(string path)
        {
            return path.Replace("\\", "/").Replace("Assets", Application.dataPath);
        }
        public static void CreateFolder(string path)
        {
            string folder = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }
}
