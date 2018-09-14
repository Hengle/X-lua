namespace AssetManager
{
    using UnityEditor;
    using UnityEngine;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;
    using System.Collections.Generic;
    using System.Linq;

    public class AssetImporter : OdinMenuEditorWindow
    {
        [MenuItem("资源管理器/资源导入设置", false, 1)]
        private static void Open()
        {
            Instance = GetWindow<AssetImporter>("资源导入设置");
            Instance.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            ImporterConfig.LoadInstanceIfAssetExists();
            Instance._import = new GUIContent(EditorIcons.Refresh.Raw);
            Instance._importAll = new GUIContent("导入当前配置");
        }

        public static AssetImporter Instance;

        public void RefreshTree()
        {
            OdinMenuTree MenuTree = Instance.MenuTree;
            var items = MenuTree.MenuItems;
            for (int i = 0; i < items.Count; i++)
            {
                items[i].ChildMenuItems.Clear();
            }
            SetMenuTree(MenuTree);
            MenuTree.UpdateMenuTree();
        }

        readonly HashSet<string> settings = new HashSet<string>() { TAB_TEXTURE, TAB_MODEL, TAB_AUDIO };
        const string TAB_TEXTURE = "#贴图配置";
        const string TAB_MODEL = "#模型配置";
        const string TAB_AUDIO = "#音效配置";

        GUIContent _import;
        GUIContent _importAll;
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;

            SetMenuTree(tree);

            EditorUtility.ClearProgressBar();
            return tree;
        }

        public void SetMenuTree(OdinMenuTree tree)
        {
            var texConfigs = ImporterConfig.Instance.TextureConfigs;
            ImporterConfig.Instance.LoadTexture(tree, TAB_TEXTURE);
            var texOverView = new AssetImpCfgOverview<TextureImpConfig>(texConfigs);
            tree.Add(TAB_TEXTURE, texOverView, EditorIcons.SettingsCog);

            var modelConfigs = ImporterConfig.Instance.ModelConfigs;
            ImporterConfig.Instance.LoadModel(tree, TAB_MODEL);
            var modelOverview = new AssetImpCfgOverview<ModelImpConfig>(modelConfigs);
            tree.Add(TAB_MODEL, modelOverview, EditorIcons.SettingsCog);

            var audioClipConfigs = ImporterConfig.Instance.AudioClipConfigs;
            ImporterConfig.Instance.LoadAudioClip(tree, TAB_AUDIO);
            var audioClipOverview = new AssetImpCfgOverview<AudioClipImpConfig>(audioClipConfigs);
            tree.Add(TAB_AUDIO, audioClipOverview, EditorIcons.SettingsCog);
        }

        private void ClearTree()
        {
            var items = MenuTree.MenuItems;
            for (int i = 0; i < items.Count; i++)
            {
                items[i].ChildMenuItems.Clear();
            }
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            if (selected == null) return;

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (!settings.Contains(selected.SmartName))
                {
                    if (SirenixEditorGUI.ToolbarButton(_import))
                    {
                        (selected.ObjectInstance as AssetOption).ReimportAsset();
                    }
                }
                else
                {
                    if (SirenixEditorGUI.ToolbarButton(_importAll))
                    {
                        var child = selected.ChildMenuItems;
                        for (int i = 0; i < child.Count; i++)
                            (child[i].ObjectInstance as AssetOption).ReimportAsset();
                    }
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ImporterConfig.Instance.ClearInvalidPath();
            EditorUtility.SetDirty(ImporterConfig.Instance);
            EditorUtility.UnloadUnusedAssetsImmediate();
            AssetDatabase.Refresh();
        }
    }
}
