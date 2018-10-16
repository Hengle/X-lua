namespace CS.ActorConfig
{
    using XmlCfg.Skill;
    using UnityEngine;
    using UnityEditor;
    using System.Linq;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using Sirenix.OdinInspector.Editor;

    internal class ActorCfgWindow : OdinMenuEditorWindow
    {
        [MenuItem("Window/Action Designer/Load Action")]
        private static void Open()
        {
            var window = GetWindow<ActorCfgWindow>("模型配置窗口");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            window.minSize = new Vector2(400, 500);
            ActionHomeConfig.LoadInstanceIfAssetExists();
            HomeConfigPreview.Instance.Init();
        }

        ModelActionEditor _modelAction;
        GUIContent _createConfig = new GUIContent("创建角色");
        Rect _windowRect = new Rect(100, 100, 200, 200);
   
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: false);
            tree.Config.DrawSearchToolbar = true;
            foreach (var item in ActionHomeConfig.MenuItems)
            {
                if (item.Key != GroupType.None)
                    tree.Add(item.Value, null, EditorIcons.Table);
                else
                    tree.Add("主页", HomeConfigPreview.Instance, EditorIcons.House);
            }
            foreach (var group in HomeConfigPreview.Instance.ModelGroupDict)
            {
                foreach (var item in group.Value)
                {
                    tree.Add(item.MenuItemName, item);
                }
            }

            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            if (MenuTree == null) return;

            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    var old = GUI.color;
                    GUI.color = Color.cyan;
                    GUILayout.Label(selected.Name, SirenixGUIStyles.BoldLabel);
                    GUI.color = old;
                }

                if (SirenixEditorGUI.ToolbarButton(_createConfig))
                {
                    HomeConfigPreview.Instance.Create((model) =>
                    {
                        HomeConfigPreview.Instance.AddActor(model);
                        MenuTree.AddObjectAtPath(model.MenuItemName, model);
                    });
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            HomeConfigPreview.Instance.Destroy();

            EditorUtility.UnloadUnusedAssetsImmediate(true);
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();

            Debug.Log("[模型配置窗口]关闭~~");
        }

    }
}