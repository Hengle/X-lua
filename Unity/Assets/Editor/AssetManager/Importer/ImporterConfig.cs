namespace AssetManager
{
    using System;
    using UnityEngine;
    using System.Collections;
    using Sirenix.Utilities;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using Sirenix.Serialization;
    using Sirenix.OdinInspector.Editor;

    //ScriptableObject
    [GlobalConfig("Editor/AssetManager/Config", UseAsset = true)]
    public class ImporterConfig : GlobalConfig<ImporterConfig>
    {
        [BoxGroup("路径配置组"), ListDrawerSettings(IsReadOnly = true)]
        public List<TextureImpConfig> TextureConfigs = new List<TextureImpConfig>();

        [BoxGroup("路径配置组"), ListDrawerSettings(IsReadOnly = true)]
        public List<ModelImpConfig> ModelConfigs = new List<ModelImpConfig>();

        [BoxGroup("路径配置组"), ListDrawerSettings(IsReadOnly = true)]
        public List<AudioClipImpConfig> AudioClipConfigs = new List<AudioClipImpConfig>();


        [Button("Clear Invalid Path", ButtonSizes.Large)]
        public void ClearInvalidPath()
        {
            TextureConfigs = InvalidPath(TextureConfigs);
            ModelConfigs = InvalidPath(ModelConfigs);
            AudioClipConfigs = InvalidPath(AudioClipConfigs);
        }

        [Button("Clear All", ButtonSizes.Large)]
        private void ClearAll()
        {
            TextureConfigs.Clear();
            ModelConfigs.Clear();
            AudioClipConfigs.Clear();
        }


        private List<T> InvalidPath<T>(List<T> list) where T : AssetImpConfig
        {
            var newList = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null) continue;

                string path = list[i].GetAbsPath();
                if (string.IsNullOrEmpty(list[i].FolderPath) || !Directory.Exists(path)) continue;

                newList.Add(list[i]);
            }
            return newList;
        }
        private void LoadConfig<T>(OdinMenuTree tree, AssetImpConfig cfg, ref List<T> setting, string tabName) where T : AssetOption, new()
        {
            if (!Directory.Exists(cfg.GetAbsPath())) return;

            UpdateConfig<T>(cfg, ref setting);
            for (int i = 0; i < setting.Count; i++)
            {
                var imp = setting[i] as T;
                tree.AddObjectAtPath(string.Format("{0}/{1}", tabName, imp.Name), imp).SortMenuItemsByName();
                EditorUtility.DisplayProgressBar(tabName, setting[i].Path, (i + 1f) / setting.Count);
            }
            EditorUtility.ClearProgressBar();
        }
        private void UpdateConfig<T>(AssetImpConfig cfg, ref List<T> Setting) where T : AssetOption, new()
        {
            string absFolderPath = cfg.GetAbsPath();
            string[] dirs = null;

            if (cfg.FlattenSubFolder)
            {//多项
                SearchOption option = cfg.IncludeSubFolder ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                dirs = Directory.GetDirectories(absFolderPath, "*", option);
                List<T> newImports = new List<T>();
                if (dirs.Length == 0)
                {
                    string path = AssetOption.GetUnityPath(absFolderPath);
                    T newImp = new T();
                    newImp.Path = path;
                    newImports.Add(newImp);
                }
                else
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        string path = AssetOption.GetUnityPath(dirs[i]);
                        T oldImp = Setting.Find(imp => imp.Path.Equals(path)) as T;
                        if (oldImp != null)
                        {
                            newImports.Add(oldImp);
                        }
                        else
                        {
                            T newImp = new T();
                            newImp.Path = path;
                            newImports.Add(newImp);
                        }
                    }
                Setting = newImports;
            }
            else
            {
                string path = AssetOption.GetUnityPath(absFolderPath);
                T impSet = Setting.Find(imp => imp.Path.Equals(path)) as T;
                if (impSet == null)
                {
                    impSet = new T();
                    impSet.Path = path;
                }
                Setting.Clear();
                Setting.Add(impSet);
            }
        }

        //--资源配数据加载
        public void LoadTexture(OdinMenuTree tree, string tabName)
        {
            for (int i = 0; i < TextureConfigs.Count; i++)
            {
                if (TextureConfigs[i] == null) continue;
                LoadConfig(tree, TextureConfigs[i], ref TextureConfigs[i].Setting, tabName);
            }
        }
        public void LoadModel(OdinMenuTree tree, string tabName)
        {
            for (int i = 0; i < ModelConfigs.Count; i++)
            {
                if (ModelConfigs[i] == null) continue;
                LoadConfig(tree, ModelConfigs[i], ref ModelConfigs[i].Setting, tabName);
            }
        }
        public void LoadAudioClip(OdinMenuTree tree, string tabName)
        {
            for (int i = 0; i < AudioClipConfigs.Count; i++)
            {
                if (AudioClipConfigs[i] == null) continue;
                LoadConfig(tree, AudioClipConfigs[i], ref AudioClipConfigs[i].Setting, tabName);
            }
        }

    }
    class AssetImpCfgOverview<T> where T : AssetImpConfig, new()
    {
        [ListDrawerSettings(ShowItemCount = true), LabelText("配置列表"), OnValueChanged("OnValueChanged")]
        public List<AssetImpConfig> Configs;
        private List<T> _configs;
        public AssetImpCfgOverview(List<T> configs)
        {
            Configs = new List<AssetImpConfig>();
            configs.ForEach(cfg => Configs.Add(cfg));
            _configs = configs;
        }
        private void OnValueChanged()
        {
            _configs.Clear();
            Configs.ForEach(cfg =>
            {
                T item = new T();
                item.FolderPath = cfg.FolderPath;
                item.IncludeSubFolder = cfg.IncludeSubFolder;
                item.FlattenSubFolder = cfg.FlattenSubFolder;
                _configs.Add(item);
            });
            AssetImporter.Instance.RefreshTree();
        }
    }
    [Serializable]
    public class AssetImpConfig
    {
        [HorizontalGroup("Spilt"), HideLabel, OnValueChanged("OnValueChanged")]
        [FolderPath(ParentFolder = "Assets", RequireValidPath = true)]
        public string FolderPath;
        [HorizontalGroup("Spilt"), LabelWidth(120), ToggleLeft, LabelText("包含子目录"), OnValueChanged("OnValueChanged")]
        public bool IncludeSubFolder = false;
        [HorizontalGroup("Spilt"), LabelWidth(120), ToggleLeft, LabelText("作为同级目录显示"), OnValueChanged("OnValueChanged")]
        public bool FlattenSubFolder = false;

        private void OnValueChanged()
        {
            AssetImporter.Instance.RefreshTree();
        }
        public string GetAbsPath()
        {
            return string.Format("{0}/{1}", Application.dataPath, FolderPath);
        }
    }
    [Serializable]
    public class TextureImpConfig : AssetImpConfig
    {
        [SerializeField]
        public List<TextureImportSetting> Setting = new List<TextureImportSetting>();
    }
    [Serializable]
    public class ModelImpConfig : AssetImpConfig
    {
        [SerializeField]
        public List<ModelImportSetting> Setting = new List<ModelImportSetting>();
    }
    [Serializable]
    public class AudioClipImpConfig : AssetImpConfig
    {
        [SerializeField]
        public List<AudioClipImportSetting> Setting = new List<AudioClipImportSetting>();
    }
}