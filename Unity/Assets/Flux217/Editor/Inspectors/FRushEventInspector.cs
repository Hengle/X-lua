using UnityEngine;
using UnityEditor;

using Flux;

namespace FluxEditor
{
    [CustomEditor(typeof(FRushEvent), true)]
    public class FRushEventInspector : FEventInspector
    {

        protected SerializedProperty _angle;
        protected SerializedProperty _length;

        private GUIContent _angleUI;
        private GUIContent _lengthUI;

        protected override void OnEnable()
        {
            base.OnEnable();

            _angle = serializedObject.FindProperty("_angle");
            _length = serializedObject.FindProperty("_length");

            _angleUI = new GUIContent("角度", "绕Y轴顺时针旋转");
            _lengthUI = new GUIContent("距离", "距离长度");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.IntSlider(_angle, 0, 360, _angleUI);
            EditorGUILayout.PropertyField(_length, _lengthUI);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
