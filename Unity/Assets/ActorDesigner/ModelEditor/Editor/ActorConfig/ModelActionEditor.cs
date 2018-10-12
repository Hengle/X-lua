namespace CS.ActorConfig
{
    using System;
    using XmlCfg.Skill;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities.Editor;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [Serializable]
    public class ModelActionEditor
    {
        public const int INHERIT_WIDTH = 50;
        public const int ACT_NAME_WIDTH = 100;
        public const int ACT_SRC_WIDTH = 100;
        public const int ACT_CLIP_WIDTH = 100;

        private GeneralAction _modelAction;
        private bool _isSkillAction = false;
        private ModelActionConfigEditor _modelEditor;

        [OnInspectorGUI]
        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            Color color = Color.green;
            string actState = "新建";
            switch (ActState)
            {
                case ActionState.New:
                    break;
                case ActionState.Inherit:
                    color = Color.yellow;
                    actState = "继承";
                    break;
                case ActionState.Override:
                    color = Color.red;
                    actState = "重写";
                    break;
            }
            GUIHelper.PushColor(color);
            GUILayout.Label(actState, SirenixGUIStyles.LabelCentered, GUILayout.Width(INHERIT_WIDTH));
            GUIHelper.PopColor();

            GUILayout.Label(ActionName, SirenixGUIStyles.LabelCentered, GUILayout.Width(ACT_NAME_WIDTH));
            GUILayout.Label(IsFromOther ? "其他" : "自己", SirenixGUIStyles.LabelCentered, GUILayout.Width(ACT_SRC_WIDTH));

            bool isClipMiss = string.IsNullOrEmpty(ActionClip);
            GUIHelper.PushColor(isClipMiss ? Color.red : Color.green);
            GUILayout.Label(isClipMiss ? "Error" : ActionClip, SirenixGUIStyles.LabelCentered, GUILayout.Width(ACT_CLIP_WIDTH));
            GUIHelper.PopColor();

            GUIHelper.PushGUIPositionOffset(Vector2.up * -4f);
            if (GUILayout.Button("加载", SirenixGUIStyles.ButtonLeft))
            {
                LoadTimeline();
            }
            if (GUILayout.Button("修改", SirenixGUIStyles.ButtonRight))
            {
                Modifier();
            }
            bool isSelected = IsSelected;
            if (isSelected)
                GUIHelper.PushColor(Color.green);
            if (GUILayout.Button("选择", SirenixGUIStyles.Button, GUILayout.Width(40)))
            {
                IsSelected = !IsSelected;
            }
            if (isSelected)
                GUIHelper.PopColor();
            GUIHelper.PopGUIPositionOffset();

            EditorGUILayout.EndHorizontal();
        }
        private void LoadTimeline()
        {

        }
        private void Modifier()
        {
            ActionWindow.Init(_modelEditor, this);
        }

        public enum ActionState
        {
            New,
            Inherit,
            Override,
        }
        public ActionState ActState { get; set; }
        public ModelActionEditor() { }
        public ModelActionEditor(ModelActionConfigEditor modelEditor, GeneralAction modelAction, bool isSkill)
        {
            _modelEditor = modelEditor;
            _modelAction = modelAction;
            _isSkillAction = isSkill;
        }
        public ModelActionEditor(ModelActionEditor actionEditor)
        {
            _modelEditor = actionEditor._modelEditor;
            _modelAction = actionEditor._modelAction;
            _isSkillAction = actionEditor._isSkillAction;
        }
        public override bool Equals(object obj)
        {
            ModelActionEditor other = obj as ModelActionEditor;
            if (other == null) return false;

            bool result = other.IsFromOther.Equals(IsFromOther);
            result &= other.OtherModelName.Equals(OtherModelName);
            result &= other.ActionClip.Equals(ActionClip);

            return result;
        }

        public GeneralAction ModelAction { get { return _modelAction; } }
        public string ActionName { get { return string.IsNullOrEmpty(_modelAction.ActionName) ? string.Empty : _modelAction.ActionName; } set { _modelAction.ActionName = value; } }
        public bool IsFromOther { get { return _modelAction.IsFromOther; } set { _modelAction.IsFromOther = value; } }
        public string OtherModelName { get { return string.IsNullOrEmpty(_modelAction.OtherModelName) ? string.Empty : _modelAction.OtherModelName; } set { _modelAction.OtherModelName = value; } }
        public string ActionClip { get { return string.IsNullOrEmpty(_modelAction.ActionFile) ? string.Empty : _modelAction.ActionFile; } set { _modelAction.ActionFile = value; } }
        public bool IsSelected { get; set; }
        public bool IsSkillAction { get { return _isSkillAction; } }


        private string DrawName(string name, GUIContent content)
        {
            List<string> array = new List<string> { "跑", "站立", "攻击" };
            int index = array.FindIndex(a => a == name);
            index = EditorGUILayout.Popup("", 0, array.ToArray());
            return array[index];
        }
    }
}
