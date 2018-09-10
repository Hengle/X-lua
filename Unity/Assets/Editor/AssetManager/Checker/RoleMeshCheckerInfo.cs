namespace AssetManager
{
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEditor;
    using System.Linq;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using Sirenix.Utilities.Editor;

    [Serializable]
    public class RoleMeshCheckerInfo : CheckerInfo
    {
        [ShowInInspector, BoxGroup("限制条件"), PropertyOrder(-100)]
        [FolderPath(RequireValidPath = true), LabelText("角色网格目录")]
        public override string Path
        {
            get { return base.Path; }
            set { base.Path = value; }
        }
        [BoxGroup("限制条件"), Range(3, 60000)]
        public int LimitVerts = 2500;
        [BoxGroup("限制条件"), Range(1, 5)]
        public int LimitSubmesh = 1;
        [BoxGroup("限制条件"), Range(1, 5)]
        public int LimitSkin = 1;
        [BoxGroup("限制条件")]
        public ChannelType Channel = ChannelType.UV;

        [Flags]
        public enum ChannelType
        {
            None = 0,
            UV = 1 << 1,
            UV2 = 1 << 3,
            UV3 = 1 << 4,
            UV4 = 1 << 5,
            Color = 1 << 6,
        }

        class MeshInfo
        {
            public string Path;
            public int Verts;
            public int SubmeshNum;
            public int SkinNum;
            public ChannelType Channel;
        }

        [OnInspectorGUI]
        void OnInspectorGUI()
        {
            if (_meshInfos != null)
                SirenixEditorGUI.MessageBox(string.Format("异常文件数量{0}个", _meshInfos.Count), MessageType.Info);

            SirenixEditorGUI.BeginHorizontalToolbar();
            GUILayout.Label("文件名", SirenixGUIStyles.BoldLabel, GUILayout.Width(650));
            GUILayout.Label("顶点数量", SirenixGUIStyles.BoldLabel, GUILayout.Width(60));
            GUILayout.Label("子网格数", SirenixGUIStyles.BoldLabel, GUILayout.Width(60));
            GUILayout.Label("蒙皮数量", SirenixGUIStyles.BoldLabel, GUILayout.Width(60));
            GUILayout.Label("通道", SirenixGUIStyles.BoldLabelCentered);
            SirenixEditorGUI.EndHorizontalToolbar();



            if (_meshInfos == null) return;
            GUILayout.Space(-5);
            SirenixEditorGUI.BeginBox();
            for (int i = 0; i < _meshInfos.Count; i++)
            {
                var item = _meshInfos[i];
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(item.Path, SirenixGUIStyles.Label, GUILayout.Width(650));

                GUILayout.Label(item.Verts.ToString(), SirenixGUIStyles.LabelCentered, GUILayout.Width(50));
                GUILayout.Label(item.SubmeshNum.ToString(), SirenixGUIStyles.LabelCentered, GUILayout.Width(56));
                GUILayout.Label(item.SkinNum.ToString(), SirenixGUIStyles.LabelCentered, GUILayout.Width(60));
                GUILayout.Label(item.Channel.ToString(), SirenixGUIStyles.LabelCentered);
                EditorGUILayout.EndHorizontal();
                if (i + 1 != _meshInfos.Count)
                {
                    GUILayout.Space(3);
                    SirenixEditorGUI.HorizontalLineSeparator();
                }
            }
            SirenixEditorGUI.EndBox();
        }

        public RoleMeshCheckerInfo(string name) : base(name) { }


        List<MeshInfo> _meshInfos = new List<MeshInfo>();
        public override void CheckAsset()
        {
            base.CheckAsset();

            ClearAsset();

            float count = 0;
            string[] files = Directory.GetFiles(GetAbsPath(Path), "*.fbx", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                string path = GetUnityPath(f);
                EditorUtility.DisplayProgressBar("角色网格信息检查", path, ++count / files.Length);
                GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                SkinnedMeshRenderer[] skins = go.GetComponentsInChildren<SkinnedMeshRenderer>();
                if (skins.Length == 0)
                {
                    EditorUtility.DisplayDialog("网格检查", path + "  资源无蒙皮信息!", "确定");
                    continue;
                }

                int verts = 0;
                int submeshs = 0;
                ChannelType channel = ChannelType.None;
                for (int i = 0; i < skins.Length; i++)
                {
                    Mesh mesh = skins[i].sharedMesh;
                    verts += mesh.vertexCount;
                    submeshs += mesh.subMeshCount;

                    if ((ChannelType.Color & Channel) == 0 && mesh.colors.Length > 0)
                        channel |= ChannelType.Color;
                    if ((ChannelType.UV & Channel) == 0 && mesh.uv.Length > 0)
                        channel |= ChannelType.UV;
                    if ((ChannelType.UV2 & Channel) == 0 && mesh.uv2.Length > 0)
                        channel |= ChannelType.UV2;
                    if ((ChannelType.UV3 & Channel) == 0 && mesh.uv3.Length > 0)
                        channel |= ChannelType.UV3;
                    if ((ChannelType.UV4 & Channel) == 0 && mesh.uv4.Length > 0)
                        channel |= ChannelType.UV4;
                }
                int skinNum = skins.Length;
                bool isMultiSkin = skins.Length > 1;

                if (verts > LimitVerts || submeshs > LimitSubmesh || skinNum > LimitSkin || channel != ChannelType.None)
                {
                    MeshInfo info = new MeshInfo();
                    info.Path = path;
                    info.Verts = verts;
                    info.SubmeshNum = submeshs;
                    info.SkinNum = skinNum;
                    info.Verts = verts;
                    info.Channel = channel;
                    _meshInfos.Add(info);
                }
            }

            EditorUtility.UnloadUnusedAssetsImmediate();
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }
        public override void ClearAsset()
        {
            base.ClearAsset();
            if (_meshInfos == null)
                _meshInfos = new List<MeshInfo>();
            _meshInfos.Clear();
        }
    }
}