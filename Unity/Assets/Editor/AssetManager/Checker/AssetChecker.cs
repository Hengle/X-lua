namespace AssetManager
{
    using UnityEditor;
    using UnityEngine;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;
    using System.Linq;

    public class AssetChecker : OdinMenuEditorWindow
    {
        [MenuItem("资源管理器/资源检查器", false, 3)]
        private static void Open()
        {
            var window = GetWindow<AssetChecker>("资源检查器");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1200, 600);
            CheckerConfig.LoadInstanceIfAssetExists();
            window._check = new GUIContent(EditorIcons.Refresh.Raw);
        }

        const string TAB_ATLAS_WITHOUT_SPRITE = "未打入图集中的精灵";
        const string TAB_ROLE_MESH = "角色网格";
        const string TAB_FILE_NAME = "非法文件命名";
        const string TAB_TEXTURE_STRETCH = "图片拉伸模糊";
        GUIContent _check;

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;

            if (CheckerConfig.Instance.BitmapChecker == null)
                CheckerConfig.Instance.BitmapChecker = new BitmapCheckerInfo(TAB_ATLAS_WITHOUT_SPRITE);
            tree.AddObjectAtPath(TAB_ATLAS_WITHOUT_SPRITE, CheckerConfig.Instance.BitmapChecker);

            if (CheckerConfig.Instance.RoleMeshChecker == null)
                CheckerConfig.Instance.RoleMeshChecker = new RoleMeshCheckerInfo(TAB_ROLE_MESH);
            tree.AddObjectAtPath(TAB_ROLE_MESH, CheckerConfig.Instance.RoleMeshChecker);

            if (CheckerConfig.Instance.FileNameChecker == null)
                CheckerConfig.Instance.FileNameChecker = new FileNameCheckerInfo(TAB_FILE_NAME);
            tree.AddObjectAtPath(TAB_FILE_NAME, CheckerConfig.Instance.FileNameChecker);

            if (CheckerConfig.Instance.TextureStretchChecker == null)
                CheckerConfig.Instance.TextureStretchChecker = new TextureStretchCheckerInfo(TAB_TEXTURE_STRETCH);
            tree.AddObjectAtPath(TAB_TEXTURE_STRETCH, CheckerConfig.Instance.TextureStretchChecker);

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            if (selected == null) return;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(_check))
                {
                    (selected.ObjectInstance as CheckerInfo).CheckAsset();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            EditorUtility.SetDirty(CheckerConfig.Instance);
            EditorUtility.UnloadUnusedAssetsImmediate();
            AssetDatabase.Refresh();
        }
    }
}
