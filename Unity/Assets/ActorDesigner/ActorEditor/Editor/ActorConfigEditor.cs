namespace CS.ActorConfig
{
    using System;
    using System.IO;
    using XmlCfg.Skill;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using Sirenix.OdinInspector.Editor;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using Sirenix.Utilities.Editor;

    [Serializable]
    public class ActorConfigEditor
    {
        public ActorConfig ActorCfg { get { return _actorCfg; } }
        public ActorConfigEditor() { }
        public ActorConfigEditor(string path)
        {           
            _path = path;
            _actorCfg = new ActorConfig();
            if (!File.Exists(path))
            {
                File.Create(path);
                _actorCfg.SaveAConfig(path);
            }
            _actorCfg.LoadAConfig(path);
            if (_actorCfg != null)
            {
                foreach (var item in _actorCfg.GeneralActions)
                {
                    var action = new ModelActionEditor(this, item, false);
                    action.ActState = ModelActionEditor.ActionState.New;
                    ModelActions.Add(action);
                }
                foreach (var item in _actorCfg.SkillActions)
                {
                    var action = new ModelActionEditor(this, item, true);
                    action.ActState = ModelActionEditor.ActionState.New;
                    SkillActions.Add(action);
                }
            }
        }
        public void Init()
        {
            if (string.IsNullOrEmpty(BaseName)) return;
            AddBaseModelAction(BaseName);
        }
        public string[] GetActionClips()
        {
            return _actClips;
        }

        private ActorConfig _actorCfg;
        private string _path;
        private string[] _actClips = new string[] { "idel", "run", "attack" };

        public string Path { get { return _path; } }
        public string MenuItemName { get { return string.Format("{0}/{1}", ActionHomeConfig.MenuItems[GroupType], ModelName); } }

        [BoxGroup("BaseGroup", showLabel: false, order: -100)]
        [VerticalGroup("BaseGroup/Info"), CustomValueDrawer("DrawGroupType")]
        public GroupType GroupType
        {
            get
            {
                if (_actorCfg == null)
                    return GroupType.None;
                return _actorCfg.GroupType;
            }
            set
            {
                ActorCfgWindow window = ActorCfgWindow.GetWindow<ActorCfgWindow>();
                OdinMenuItem item = window.MenuTree.Selection.FirstOrDefault();
                if (item != null)
                {
                    ActorConfigEditor model = item.ObjectInstance as ActorConfigEditor;
                    HomeConfigPreview.Instance.RemoveActor(model);
                    item.Parent.ChildMenuItems.Remove(item);

                    _actorCfg.GroupType = value;
                    HomeConfigPreview.Instance.AddActor(model);
                    var group = window.MenuTree.GetMenuItem(Group);
                    group.ChildMenuItems.Add(item);
                    item.MenuTree.Selection.Clear();
                    item.Select();
                    item.MenuTree.UpdateMenuTree();
                    item.MenuTree.DrawMenuTree();
                }
            }
        }
        public string Group
        {
            get
            {
                return ActionHomeConfig.MenuItems[_actorCfg.GroupType];
            }
        }
        [VerticalGroup("BaseGroup/Info"), LabelText("模型名称"), InlineButton("ModifyModelName", "更换")]
        public string ModelName { get { return _actorCfg == null ? "" : _actorCfg.ModelName; } set { } }
        [VerticalGroup("BaseGroup/Info"), LabelText("继承模型"), InlineButton("ModifyBaseName", "更换")]
        public string BaseName { get { return _actorCfg == null ? "" : _actorCfg.BaseModelName; } set { } }




        [ButtonGroup("Button"), Button("复制", ButtonSizes.Large)]
        private void CopyActions()
        {
            List<ModelActionEditor> copy = new List<ModelActionEditor>();
            foreach (var item in ModelActions)
                if (item.IsSelected) copy.Add(item);
            foreach (var item in SkillActions)
                if (item.IsSelected) copy.Add(item);
            Clipboard.Copy(copy, CopyModes.DeepCopy);
        }
        [ButtonGroup("Button"), Button("粘贴", ButtonSizes.Large)]
        private void PasteActions()
        {
            List<ModelActionEditor> paste = new List<ModelActionEditor>();
            if (Clipboard.TryPaste(out paste))
            {
                foreach (var item in paste)
                {
                    if (item.IsSkillAction)
                        SkillActions.Add(item);
                    else
                        ModelActions.Add(item);
                    item.IsSelected = false;
                }
                Clipboard.Clear();
            }
            else
            {
                EditorUtility.DisplayDialog("粘贴异常", "行为数据粘贴失败,或者无数据可张贴~~", "确定");
            }
        }
        [ButtonGroup("Button"), Button("保存文件", ButtonSizes.Large)]
        public void Save()
        {
            _actorCfg.GeneralActions.Clear();
            _actorCfg.SkillActions.Clear();
            foreach (var item in ModelActions)
                _actorCfg.GeneralActions.Add(item.ModelAction);
            foreach (var item in SkillActions)
                _actorCfg.SkillActions.Add(item.ModelAction as SkillAction);
            _actorCfg.SaveAConfig(_path);
        }
        [ButtonGroup("Button"), Button("删除文件", ButtonSizes.Large)]
        public void Delete()
        {
            if (EditorUtility.DisplayDialog("删除操作", "确定要删除文件 -> " + ModelName, "确定", "取消"))
            {
                if (File.Exists(_path))
                    File.Delete(_path);

                ActorCfgWindow window = ActorCfgWindow.GetWindow<ActorCfgWindow>();
                OdinMenuItem item = window.MenuTree.Selection.FirstOrDefault();
                if (item != null && item.ObjectInstance != null)
                {
                    ActorConfigEditor model = item.ObjectInstance as ActorConfigEditor;
                    HomeConfigPreview.Instance.RemoveActor(model);
                    item.Parent.ChildMenuItems.Remove(item);
                    item.MenuTree.Selection.Clear();
                    item.Parent.Select();
                    item.MenuTree.UpdateMenuTree();
                    item.MenuTree.DrawMenuTree();
                }
            }
        }

        private static GroupType DrawGroupType(GroupType type, GUIContent content)
        {
            return (GroupType)EditorGUILayout.Popup(content.text, (int)type, ActionHomeConfig.MenuItemNames);
        }
        private void ModifyModelName()
        {
            var models = new List<string>(Csv.CfgManager.Model.Keys);
            models.Remove(ModelName);
            SimplePopupCreator.ShowDialog(new List<string>(models), (name) =>
            {
                _actorCfg.ModelName = name;

                ActorCfgWindow window = ActorCfgWindow.GetWindow<ActorCfgWindow>();
                OdinMenuItem item = window.MenuTree.Selection.FirstOrDefault();
                item.Name = name;
                item.SearchString = name;
            });
        }
        private void ModifyBaseName()
        {
            List<string> models = HomeConfigPreview.Instance.GetAllActorList();
            models.Remove(ModelName);
            SimplePopupCreator.ShowDialog(new List<string>(models), (name) =>
            {
                _actorCfg.BaseModelName = name;
                AddBaseModelAction(name);
            });
        }
        private void AddBaseModelAction(string name)
        {
            var baseModel = HomeConfigPreview.Instance.GetActorEditor(name);
            var modelDict = GetModelActionDict();
            foreach (var item in baseModel.ModelActions)
            {
                if (modelDict.ContainsKey(item.ActionName))
                {
                    modelDict[item.ActionName].ActState = modelDict[item.ActionName].Equals(item) ?
                        ModelActionEditor.ActionState.Inherit : ModelActionEditor.ActionState.Override;
                }
                else
                {
                    var action = new ModelActionEditor(item);
                    action.ActState = ModelActionEditor.ActionState.Inherit;
                    ModelActions.Add(action);
                }
            }
            var skillDict = GetModelActionDict();
            foreach (var item in baseModel.SkillActions)
            {
                if (skillDict.ContainsKey(item.ActionName))
                {
                    skillDict[item.ActionName].ActState = skillDict[item.ActionName].Equals(item) ?
                        ModelActionEditor.ActionState.Inherit : ModelActionEditor.ActionState.Override;
                }
                else
                {
                    var action = new ModelActionEditor(item);
                    action.ActState = ModelActionEditor.ActionState.Inherit;
                    SkillActions.Add(action);
                }
            }
        }

        [Title("普通动作列表", bold: true), OnInspectorGUI, PropertyOrder(10)]
        private void OnModelActionsHeader() { OnHeaderGUI(); }
        [LabelText(" "), Space(-5f), PropertyOrder(15)]
        [ListDrawerSettings(OnTitleBarGUI = "OnGUIModelAction", CustomAddFunction = "CreateModelAction")]
        public List<ModelActionEditor> ModelActions = new List<ModelActionEditor>();
        [Title("技能动作列表", bold: true), OnInspectorGUI, PropertyOrder(20)]
        private void OnSkillActionsHeader() { OnHeaderGUI(); }
        [LabelText(" "), Space(-5f), PropertyOrder(25)]
        [ListDrawerSettings(OnTitleBarGUI = "OnGUISkilllAction", CustomAddFunction = "CreateSkillAction")]
        public List<ModelActionEditor> SkillActions = new List<ModelActionEditor>();

        Dictionary<string, ModelActionEditor> GetModelActionDict()
        {
            var pairs = new Dictionary<string, ModelActionEditor>();
            foreach (var item in ModelActions)
            {
                pairs.Add(item.ActionName, item);
            }
            return pairs;
        }
        Dictionary<string, ModelActionEditor> GetSkillActionDict()
        {
            var pairs = new Dictionary<string, ModelActionEditor>();
            foreach (var item in SkillActions)
            {
                pairs.Add(item.ActionName, item);
            }
            return pairs;
        }



        bool _isSelected_Model = false;
        bool _isSelected_Skill = false;
        private void CreateModelAction()
        {
            var action = new ModelActionEditor(this, new GeneralAction(), false);
            action.ActState = ModelActionEditor.ActionState.New;
            ActionWindow.Init(this, action, (act) => ModelActions.Add(act));
        }
        private void CreateSkillAction()
        {
            var action = new ModelActionEditor(this, new SkillAction(), true);
            action.ActState = ModelActionEditor.ActionState.New;
            ActionWindow.Init(this, action, (act) => ModelActions.Add(act));
        }
        private void OnGUIModelAction()
        {
            OnTitleBarGUI(ModelActions, ref _isSelected_Model);
        }
        private void OnGUISkilllAction()
        {
            OnTitleBarGUI(SkillActions, ref _isSelected_Skill);
        }
        private void OnHeaderGUI()
        {
            SirenixEditorGUI.BeginHorizontalToolbar(SirenixGUIStyles.BoxHeaderStyle);
            GUILayout.Label(" ", GUILayout.Width(17f));
            GUILayout.Label("继承", SirenixGUIStyles.LabelCentered, GUILayout.Width(ModelActionEditor.INHERIT_WIDTH));
            GUILayout.Label("动作名称", SirenixGUIStyles.LabelCentered, GUILayout.Width(ModelActionEditor.ACT_NAME_WIDTH));
            GUILayout.Label("动画来源", SirenixGUIStyles.LabelCentered, GUILayout.Width(ModelActionEditor.ACT_SRC_WIDTH));
            GUILayout.Label("动画文件", SirenixGUIStyles.LabelCentered, GUILayout.Width(ModelActionEditor.ACT_CLIP_WIDTH));
            GUILayout.Label("操作", SirenixGUIStyles.LabelCentered, GUILayout.ExpandWidth(true));
            SirenixEditorGUI.EndHorizontalToolbar();
        }
        private void OnTitleBarGUI(List<ModelActionEditor> actionEditors, ref bool state)
        {
            EditorIcon icon = state ? EditorIcons.X : EditorIcons.Checkmark;
            if (SirenixEditorGUI.ToolbarButton(icon))
            {
                state = !state;
                foreach (var item in actionEditors)
                    item.IsSelected = state;
            }
        }

    }
}
