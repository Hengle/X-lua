namespace AssetManager
{
    using UnityEditor;
    using UnityEngine;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities.Editor;
    using Sirenix.Utilities;
    using System.Collections.Generic;
    using System.Linq;

    public class AssetRefCounter : OdinMenuEditorWindow
    {
        [MenuItem("资源管理器/资源引用统计", false, 2)]
        private static void Open()
        {
            var window = GetWindow<AssetRefCounter>("资源引用统计");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            RefCounterConfig.LoadInstanceIfAssetExists();
            window._refresh = new GUIContent(EditorIcons.Refresh.Raw);
            //初始化引用基础数据
            //TODO
        }

        const string TAB_ATLASSPRITE = "界面精灵";
        const string TAB_TEXTUREUI = "界面图片";
        const string TAB_TEXTURESFX = "特效图片";
        const string TAB_TEXTURECHARACTER = "角色贴图";
        const string TAB_ANIMATIONCLIP = "动画剪辑";
        const string TAB_AUDIOCLIP = "音频剪辑";

        GUIContent _refresh;
        GUIContent _sortName = new GUIContent("名称排序");
        GUIContent _sortNum = new GUIContent("数量排序");
        GUIContent _sortSize = new GUIContent("大小排序");

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;

            CreateListItem(tree, TAB_ATLASSPRITE, RefCounterConfig.Instance.AtlasSprite);
            CreateListItem(tree, TAB_TEXTUREUI, RefCounterConfig.Instance.TextureUI);
            CreateListItem(tree, TAB_TEXTURESFX, RefCounterConfig.Instance.TextureSFX);
            CreateListItem(tree, TAB_TEXTURECHARACTER, RefCounterConfig.Instance.TextureCharacter);
            CreateListItem(tree, TAB_ANIMATIONCLIP, RefCounterConfig.Instance.AnimationClip);
            CreateListItem(tree, TAB_AUDIOCLIP, RefCounterConfig.Instance.AudioClip);

            return tree;
        }

        private void CreateListItem(OdinMenuTree tree, string tabName, RefCounterInfo refCounter)
        {
            if (refCounter == null)
                refCounter = new RefCounterInfo(tabName);
            tree.AddObjectAtPath(tabName, refCounter);
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

                if (SirenixEditorGUI.ToolbarButton(_sortName))
                {
                    (selected.ObjectInstance as RefCounterInfo).SortName();
                }
                if (SirenixEditorGUI.ToolbarButton(_sortNum))
                {
                    (selected.ObjectInstance as RefCounterInfo).SortRefNum();
                }
                if (SirenixEditorGUI.ToolbarButton(_sortSize))
                {
                    (selected.ObjectInstance as RefCounterInfo).SortSize();
                }

                if (SirenixEditorGUI.ToolbarButton(EditorIcons.X))
                {
                    (selected.ObjectInstance as RefCounterInfo).ClearAsset();
                }
                if (SirenixEditorGUI.ToolbarButton(_refresh))
                {
                    switch (selected.Name)
                    {
                        case TAB_ATLASSPRITE:
                            RefCounterInfo.CountAtlasSprite(selected.ObjectInstance as RefCounterInfo);
                            break;
                        case TAB_TEXTUREUI:
                            RefCounterInfo.CountTextureUI(selected.ObjectInstance as RefCounterInfo);
                            break;
                        case TAB_TEXTURESFX:
                            RefCounterInfo.CountTextureSFX(selected.ObjectInstance as RefCounterInfo);
                            break;
                        case TAB_TEXTURECHARACTER:
                            RefCounterInfo.CountTextureCharacter(selected.ObjectInstance as RefCounterInfo);
                            break;
                        case TAB_ANIMATIONCLIP:
                            RefCounterInfo.CountAnimationClip(selected.ObjectInstance as RefCounterInfo);
                            break;
                        case TAB_AUDIOCLIP:
                            RefCounterInfo.CountAudioClip(selected.ObjectInstance as RefCounterInfo);
                            break;
                        default:
                            break;
                    }
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            RefCounterConfig.Instance.ClearAll();
            RefCounterConfig.Instance.ClearInvalidPath();

            EditorUtility.SetDirty(RefCounterConfig.Instance);
            EditorUtility.UnloadUnusedAssetsImmediate();
            AssetDatabase.Refresh();
        }
    }
}
