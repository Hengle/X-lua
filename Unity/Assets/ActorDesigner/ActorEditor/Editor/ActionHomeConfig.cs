namespace CS.ActorConfig
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using Sirenix.Utilities;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities.Editor;
    using XmlCfg.Skill;
    using Cfg.Character;
    using Cfg;

    [GlobalConfig("ActorDesigner/ModelEditor/Editor", UseAsset = true)]
    internal class ActionHomeConfig : GlobalConfig<ActionHomeConfig>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public static readonly Dictionary<GroupType, string> MenuItems = new Dictionary<GroupType, string>()
        {
            {GroupType.None, "主页" },
            {GroupType.Base, "基础类型" },
            {GroupType.Player, "玩家" },
            {GroupType.Monster, "怪物" },
            {GroupType.NPC, "NPC" },
        };
        public static string[] MenuItemNames = { "主页", "基础类型", "玩家", "怪物", "NPC" };




        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [LabelText("Xml存储目录"), PropertyOrder(-100)]
        public string ActionConfigPath;
        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [LabelText("Csv存储目录"), PropertyOrder(-99)]
        public string ConfigRelativeDir = "";
        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [LabelText("角色资源目录"), PropertyOrder(-98)]
        public string CharacterRelativeDir = "";
        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [LabelText("Avatar资源目录"), PropertyOrder(-98)]
        public string AvatarRelativeDir = "";
        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [LabelText("特效资源目录"), PropertyOrder(-98)]
        public string EffectRelativeDir = "";
        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [LabelText("碰撞体资源目录"), PropertyOrder(-98)]
        public string ColliderRelativeDir = "";
    }

    [Serializable]
    internal class HomeConfig
    {
        private static HomeConfig _instance;
        public static HomeConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HomeConfig();
                return _instance;
            }
        }
        private ActionHomeConfig _config;

        #region 界面设计        
        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [ShowInInspector, ReadOnly, LabelText("Xml存储目录"), PropertyOrder(-100)]
        private string ActionConfigPath { get { return string.Format("{0}/../{1}/", Application.dataPath, _config.ActionConfigPath); } }
        [FolderPath(RequireExistingPath = true), BoxGroup("Config", showLabel: false)]
        [ShowInInspector, ReadOnly, LabelText("Csv存储目录"), PropertyOrder(-99)]
        private string ConfigDir { get { return string.Format("{0}/../{1}/", Application.dataPath, _config.ConfigRelativeDir); } }
        [ButtonGroup("Config/Btns")]
        [Button("加载所有动作", ButtonSizes.Large)]
        public void LoadAll()
        {
            ClearAll();

            _config = ActionHomeConfig.Instance;
            CfgManager.ConfigDir = ConfigDir;
            CfgManager.LoadAll();

            //--初始化资源路径
            EUtil.GetAssetsInSubFolderRecursively(_config.CharacterRelativeDir, "*.prefab", ref AllCharacter);
            EUtil.GetAssetsInSubFolderRecursively(_config.AvatarRelativeDir, "*.prefab", ref AllAvatar);
            EUtil.GetAssetsInSubFolderRecursively(_config.EffectRelativeDir, "*.prefab", ref AllEffects);
            foreach (var item in AllCharacter)
            {
                string searchDir = item.Value.Substring(0, item.Value.LastIndexOf(@"/prefab")) + "/clips";
                var selfClips = new Dictionary<string, string>();
                EUtil.GetAssetsInSubFolderRecursively(searchDir, "*.anim", ref selfClips);
                AllCharacterClips[item.Key] = selfClips;
            }

            //--加载行为配置
            foreach (var model in CfgManager.Model)
            {
                var modelName = model.Key;
                string path = string.Format("{0}/{1}.xml", ActionConfigPath, modelName);
                if (!File.Exists(path))
                {
                    var config = new ActorConfig() { ModelName = modelName };
                    string savePath = string.Format("{0}/{1}.xml", ActionHomeConfig.Instance.ActionConfigPath, modelName);
                    var actor = new ActorConfigEditor(savePath, config);
                    actor.Save();
                    Debug.LogFormat("<color=orange>新建Actor - {0}</color>", modelName);
                }
            }
            string[] files = Directory.GetFiles(ActionConfigPath, "*.xml", SearchOption.TopDirectoryOnly);
            foreach (var path in files)
            {
                var actor = new ActorConfigEditor(path);
                AddActor(actor);
            }
            foreach (var item in _modelDict)
                item.Value.Init();

            var window = ActorCfgWindow.GetWindow<ActorCfgWindow>();
            window.RefreshTree();
            Debug.Log("加载所有动作 完毕!");
        }
        [ButtonGroup("Config/Btns")]
        [Button("保存所有配置", ButtonSizes.Large)]
        public void SaveAll()
        {
            foreach (var group in ModelGroupDict)
            {
                float count = 0;
                foreach (var cfg in group.Value)
                {
                    if (group.Key != GroupType.None)
                        cfg.Save();
                    EditorUtility.DisplayProgressBar("导出所有配置>" + cfg.Group, cfg.Path, count / group.Value.Count);
                }
            }
            EditorUtility.ClearProgressBar();
            Debug.Log("所有配置保存完毕~~");
        }

        [Title("辅助功能区"), OnInspectorGUI]
        private void SelectTarget()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("设置受击对象"))
            {
                string targetName = "";
                SimplePopupCreator.ShowDialog(new List<string>(CfgManager.Model.Keys), (name) =>
                {
                    targetName = name;
                    Target = LoadModel(name);
                });
                EditorGUILayout.SelectableLabel(targetName);
            }
            EditorGUILayout.EndHorizontal();
        }
        #endregion



        /// <summary>
        /// 剪切板
        /// </summary>
        private List<ModelActionEditor> _clipboard = new List<ModelActionEditor>();
        /// <summary>
        /// 所有模型数据 key-模型名称 value-模型编辑器
        /// </summary>
        private Dictionary<string, ActorConfigEditor> _modelDict = new Dictionary<string, ActorConfigEditor>();
        /// <summary>
        /// 模型分组信息 key-分组类型 value-模型行为编辑器
        /// </summary>
        private Dictionary<GroupType, List<ActorConfigEditor>> _modelGroupDict = new Dictionary<GroupType, List<ActorConfigEditor>>();

        public Dictionary<GroupType, List<ActorConfigEditor>> ModelGroupDict
        {
            get
            {
                if (_config == null)
                    LoadAll();
                return _modelGroupDict;
            }
        }
        public Dictionary<string, Dictionary<string, string>> AllCharacterClips = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, string> AllCharacter = new Dictionary<string, string>();
        public Dictionary<string, string> AllAvatar = new Dictionary<string, string>();
        public Dictionary<string, string> AllEffects = new Dictionary<string, string>();
        public GameObject Self { get; private set; }
        public GameObject Target { get; private set; }

        public void AddActor(ActorConfigEditor model)
        {
            if (ModelGroupDict.ContainsKey(model.GroupType))
                ModelGroupDict[model.GroupType].Add(model);
            else
            {
                ModelGroupDict.Add(model.GroupType, new List<ActorConfigEditor>());
                ModelGroupDict[model.GroupType].Add(model);
            }
            if (!_modelDict.ContainsKey(model.ModelName))
                _modelDict.Add(model.ModelName, model);
        }
        public void RemoveActor(ActorConfigEditor model)
        {
            if (!ModelGroupDict.ContainsKey(model.GroupType))
            {
                Debug.LogError("未定义组类型 " + model.GroupType);
                return;
            }
            if (!ModelGroupDict[model.GroupType].Remove(model))
                Debug.LogErrorFormat("{0} 无法从分组中移除", model.MenuItemName);
            if (_modelDict.ContainsKey(model.ModelName))
                _modelDict.Remove(model.ModelName);
        }
        public ActorConfigEditor GetActorEditor(string modelName)
        {
            return _modelDict[modelName];
        }
        public List<string> GetAllActorList()
        {
            return new List<string>(_modelDict.Keys);
        }

        //-- 加载角色行为时加载资源
        public GameObject LoadModel(string modelName)
        {
            if (!AllCharacter.ContainsKey(modelName))
            {
                Debug.LogErrorFormat("[加载]{0}模型不存在", modelName);
                return GameObject.CreatePrimitive(PrimitiveType.Sphere);
            }
            string path = EUtil.FilePath2UnityPath(AllCharacter[modelName]);
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return model;
        }
        public GameObject LoadCollider(string collider)
        {
            string path = string.Format("{0}/{1}.fbx", _config.ColliderRelativeDir, collider);
            GameObject col = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return col;
        }


        public void Destroy()
        {
            //保存所有配置
            SaveAll();
            ClearAll();
            _instance = null;
        }
        private void ClearAll()
        {
            _modelGroupDict.Clear();
            CfgManager.Clear();
            _clipboard.Clear();
            _modelDict.Clear();
            AllCharacter.Clear();
            AllAvatar.Clear();
            AllCharacterClips.Clear();
            AllEffects.Clear();
        }



        public void UpdateClipbord(List<ModelActionEditor> editors)
        {
            _clipboard = editors;
        }
        /// <summary>
        /// 创建角色动作行为配置文本
        /// 每个角色只能有一套配置
        /// </summary>
        /// <returns></returns>
        private void Create()
        {
            ActorConfigEditor model = null;
            var models = CfgManager.Model.Keys;
            SimplePopupCreator.ShowDialog(new List<string>(models), (name) =>
            {
                var config = new ActorConfig() { ModelName = name };
                string path = string.Format("{0}/{1}.xml", ActionHomeConfig.Instance.ActionConfigPath, name);
                model = new ActorConfigEditor(path, config);
                model.Save();
            });
        }
    }
}