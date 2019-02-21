using NodeEditorFramework;
using NodeEditorFramework.Standard;
using NodeEditorFramework.Utilities;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ActorEditor
{
    public class ActorEditorWindow : NodeMenuWindow
    {
        [MenuItem("Node Editor/Open Actor Editor", false, 1)]
        private static ActorEditorWindow OpenActorEditor()
        {
            _ins = GetWindow<ActorEditorWindow>("资源导入设置");
            _ins.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);

            NodeEditor.ReInit(false);
            Texture iconTexture = ResourceManager.LoadTexture(EditorGUIUtility.isProSkin ? "Textures/Icon_Dark.png" : "Textures/Icon_Light.png");
            _ins.titleContent = new GUIContent("Actor Editor", iconTexture);

            return _ins;
        }
        [MenuItem("Node Editor/Close Actor Editor", false, 2)]
        private static void CloseActorEditor()
        {
            if (_ins != null)
                _ins.Close();
        }
        public static void AssureEditor()
        {
            if (_ins == null)
                OpenActorEditor();
        }


        public static ActorEditorWindow Ins { get { return _ins; } }
        private static ActorEditorWindow _ins;

        private const string AssetPath = "Assets/Node_Editor/Resources/Saves";

        // Canvas cache
        private NodeEditorUserCache _canvasCache;
        private ActorEditorInterface _editorInterface;
        private float _menuWith;
        private Rect CanvasWindowRect { get { return new Rect(MenuWidth, _editorInterface.toolbarHeight, position.width - MenuWidth - 2, position.height - _editorInterface.toolbarHeight); } }

        #region GUI
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Config.SearchToolbarHeight = (int)_editorInterface.toolbarHeight + 2;

            string[] assets = Directory.GetFiles(AssetPath, "*.asset");
            tree.AddAllAssetsAtPath(null, AssetPath, typeof(NodeCanvas));
            tree.SortMenuItemsByName();

            tree.Selection.SelectionConfirmed += SelectionConfirmed;

            return tree;
        }

        void SelectionConfirmed(OdinMenuTreeSelection items)
        {
            if (items != null && items.Count > 0)
            {
                var selected = items[0].Value as NodeCanvas;
                _ins._canvasCache.LoadNodeCanvas(selected.savePath);
            }
        }

        protected override void OnGUI()
        {
            base.OnGUI();

            // Initiation
            NodeEditor.checkInit(true);
            if (NodeEditor.InitiationError)
            {
                GUILayout.Label("Node Editor Initiation failed! Check console for more information!");
                return;
            }
            AssureEditor();
            AssureSetup();

            // ROOT: Start Overlay GUI for popups
            OverlayGUI.StartOverlayGUI("ActorEditorWindow");

            // Begin Node Editor GUI and set canvas rect
            NodeEditorGUI.StartNodeGUI(true);
            _canvasCache.editorState.canvasRect = CanvasWindowRect;

            try
            { // Perform drawing with error-handling
                NodeEditor.DrawCanvas(_canvasCache.nodeCanvas, _canvasCache.editorState);
            }
            catch (UnityException e)
            { // On exceptions in drawing flush the canvas to avoid locking the UI
                _canvasCache.NewNodeCanvas();
                NodeEditor.ReInit(true);
                Debug.LogError("Unloaded Canvas due to an exception during the drawing phase!");
                Debug.LogException(e);
            }

            // Draw Interface
            _editorInterface.DrawToolbarGUI(new Rect(MenuWidth - 5, 0, Screen.width - MenuWidth + 5, 30));
            _editorInterface.DrawModalPanel();

            // End Node Editor GUI
            NodeEditorGUI.EndNodeGUI();

            // END ROOT: End Overlay GUI and draw popups
            OverlayGUI.EndOverlayGUI();

        }
        #endregion

        #region General 
        private void OnEnable()
        {
            _ins = this;
            NormalReInit();

            // Subscribe to events
            NodeEditor.ClientRepaints -= Repaint;
            NodeEditor.ClientRepaints += Repaint;
            EditorLoadingControl.justLeftPlayMode -= NormalReInit;
            EditorLoadingControl.justLeftPlayMode += NormalReInit;
            EditorLoadingControl.justOpenedNewScene -= NormalReInit;
            EditorLoadingControl.justOpenedNewScene += NormalReInit;
        }



        private void OnDestroy()
        {
            // Unsubscribe from events
            NodeEditor.ClientRepaints -= Repaint;
            EditorLoadingControl.justLeftPlayMode -= NormalReInit;
            EditorLoadingControl.justOpenedNewScene -= NormalReInit;

            // Clear Cache
            _canvasCache.ClearCacheEvents();
        }

        private void OnLostFocus()
        { // Save any changes made while focussing this window
          // Will also save before possible assembly reload, scene switch, etc. because these require focussing of a different window
            _canvasCache.SaveCache();
        }

        private void OnFocus()
        { // Make sure the canvas hasn't been corrupted externally
            NormalReInit();
        }

        private void NormalReInit()
        {
            NodeEditor.ReInit(false);
            AssureSetup();
            if (_canvasCache.nodeCanvas)
                _canvasCache.nodeCanvas.Validate();
        }

        private void AssureSetup()
        {
            if (_canvasCache == null)
            { // Create cache
                _canvasCache = new NodeEditorUserCache(Path.GetDirectoryName(AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this))));
            }
            _canvasCache.AssureCanvas();
            if (_editorInterface == null)
            { // Setup editor interface
                _editorInterface = new ActorEditorInterface();
                _editorInterface.canvasCache = _canvasCache;
                _editorInterface.ShowNotificationAction = ShowNotification;
            }
        }
        #endregion


    }
}