namespace CS.ActorConfig
{
    using System;
    using UnityEngine;
    using UnityEditor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using Sirenix.OdinInspector.Editor;
    using System.Collections.Generic;

    internal class ActionWindow : OdinEditorWindow
    {
        public static void Init(ActorConfigEditor actorEditor, ModelActionEditor modelAction, Action<ModelActionEditor> result = null)
        {
            var window = GetWindow<ActionWindow>(true, "行为配置窗口", true);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(400, 250);
            window.minSize = new Vector2(400, 250);
            window.maxSize = new Vector2(400, 250);

            window._nameStyle = SirenixGUIStyles.BoldTitleCentered;
            window._nameStyle.fontSize = 30;
            window._nameStyle.fontStyle = FontStyle.Bold;
            window._nameStyle.normal.textColor = Color.white;

            window._selfActorEditor = actorEditor;
            window._modelAction = modelAction;
            window._actionName = modelAction.ActionName;
            window._clipName = modelAction.ActionClip;
            window._otherModelName = modelAction.OtherModelName;
            window._result = result;
            if (!modelAction.IsFromOther)
                window.LoadSelfAction();
            else
                window.LoadOtherAction(modelAction.OtherModelName);
        }

        readonly Color _color = new Color(0.3f, 0.8f, 0.8f);

        GUIStyle _nameStyle;
        ActorConfigEditor _otherActorEditor;
        ActorConfigEditor _selfActorEditor;
        Action<ModelActionEditor> _result;

        ModelActionEditor _modelAction;
        string _actionName = string.Empty;
        string _clipName = string.Empty;
        string _otherModelName = string.Empty;
        List<string> _actClipList;
        bool IsFromOther { get { return _modelAction.IsFromOther; } }

        private void LoadSelfAction()
        {
            _otherModelName = string.Empty;
            _otherActorEditor = null;
            _actClipList = _selfActorEditor.GetActionClips();
        }
        private void LoadOtherAction(string name)
        {
            _otherModelName = name;
            _otherActorEditor = HomeConfig.Instance.GetActorEditor(name);
            _actClipList = _otherActorEditor.GetActionClips();
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            if (EditorApplication.isCompiling)
            {
                Debug.Log("[行为窗口]脚本正在编译,所以自动关闭窗口!");
                Close();
                return;
            }

            SirenixEditorGUI.BeginBox();
            {
                SirenixEditorGUI.BeginBoxHeader();
                GUILayout.Label(_selfActorEditor.ModelName, _nameStyle);
                SirenixEditorGUI.EndBoxHeader();
                switch (_modelAction.ActState)
                {
                    case ModelActionEditor.ActionState.New:
                        _actionName = EditorGUILayout.TextField("动作行为", _actionName);
                        break;
                    case ModelActionEditor.ActionState.Inherit:
                    case ModelActionEditor.ActionState.Override:
                        GUILayout.Label("动作行为");
                        GUILayout.Label(_actionName, EditorStyles.textField, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        break;
                }

                EditorGUILayout.BeginHorizontal();
                {
                    GUIHelper.PushColor(_color);
                    GUILayout.Label("其他角色", SirenixGUIStyles.BoldLabel, GUILayout.Width(148));
                    GUILayout.Label(_otherModelName, EditorStyles.textField);
                    if (GUILayout.Button("修改", GUILayout.Width(45)))
                    {
                        var list = HomeConfig.Instance.GetAllActorList();
                        list.Remove(_selfActorEditor.ModelName);
                        SimplePopupCreator.ShowDialog(list, (name) => LoadOtherAction(name));
                    }
                    if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                    {
                        LoadSelfAction();
                    }
                    GUIHelper.PopColor();
                }
                EditorGUILayout.EndHorizontal();

                //EditorGUILayout.BeginHorizontal();
                //{
                //    GUILayout.Label("动画来源", GUILayout.Width(145));
                //    if (GUILayout.Button("自己", IsFromOther ? SirenixGUIStyles.ButtonLeft : SirenixGUIStyles.ButtonLeftSelected))
                //    {
                //        _clipName = string.Empty;
                //        _otherModel = string.Empty;
                //        _actClipList = _selfModelEditor.GetActionClips();
                //    }
                //    GUILayout.Button("其他", IsFromOther ? SirenixGUIStyles.ButtonRightSelected : SirenixGUIStyles.ButtonRight);
                //}
                //EditorGUILayout.EndHorizontal();

                if (IsFromOther)
                    GUIHelper.PushGUIEnabled(!string.IsNullOrEmpty(_otherModelName));
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label("动画文件");
                    if (GUILayout.Button(_clipName, SirenixGUIStyles.Popup))
                        SimplePopupCreator.ShowDialog(_actClipList, (name) =>
                        {
                            _clipName = name;
                        });
                    if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                        _clipName = string.Empty;
                }
                EditorGUILayout.EndHorizontal();
                if (IsFromOther)
                    GUIHelper.PopGUIEnabled();


                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("修改行为", GUILayout.Height(50)))
                    {
                        _modelAction.ActionName = _actionName;
                        _modelAction.ActionClip = _clipName;
                        _modelAction.OtherModelName = _otherModelName;

                        bool hasSame = false;
                        if (!_modelAction.IsSkillAction)
                        {
                            foreach (var item in _selfActorEditor.ModelActions)
                            {
                                if (item.ActionName.Equals(_actionName))
                                {
                                    hasSame = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in _selfActorEditor.SkillActions)
                            {
                                if (item.ActionName.Equals(_actionName))
                                {
                                    hasSame = true;
                                    break;
                                }
                            }
                        }
                        if (!hasSame)
                        {
                            if (_result != null) _result(_modelAction);
                            Close();
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("动作配置错误", "动作名重复,请重新配置!!!", "确定");
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
        protected override void OnDestroy()
        {
            _modelAction = null;
            _selfActorEditor = null;
            _otherActorEditor = null;
            _result = null;
            _actClipList = null;
        }
    }
}