using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Flux;


namespace FluxEditor
{
    [FEditor(typeof(FHitZone))]
    public class FSingleFrameEditor : FEventEditor
    {
        private int _offsetFrames;
        private GUIStyle _singleFrameStyle = null;
        public override void Init(FObject obj, FEditor owner)
        {
            base.Init(obj, owner);
            if (Evt.FrameRange.Length!= 1)
            {
                FrameRange range = Evt.FrameRange;
                range.Length = 1;
                Evt.FrameRange = range;
            }
        }
        protected override void RenderEvent(FrameRange viewRange, FrameRange validKeyframeRange)
        {
            Rect rect = _eventRect;
            rect.width = 30;
            rect.x = SequenceEditor.GetXForFrame(Evt.Start);

            switch (Event.current.type)
            {
                case EventType.Repaint:
                    {
                        if (!viewRange.Overlaps(Evt.FrameRange))
                            break;

                        GUIStyle evtStyle = GetEventStyle();
                        GUI.backgroundColor = GetColor();                        
                        evtStyle.Draw(rect, _isSelected, _isSelected, false, false);

                        string text = Evt.Text;
                        if (text != null)
                            GUI.Label(rect, text, GetTextStyle());
                        break;
                    }
                case EventType.MouseDown:
                    if (EditorGUIUtility.hotControl == 0 && IsSelected && !Event.current.control && !Event.current.shift)
                    {
                        Vector2 mousePos = Event.current.mousePosition;
                        if (Event.current.button == 0)
                        {
                            if (rect.Contains(mousePos))
                            {
                                EditorGUIUtility.hotControl = GuiId;
                                _offsetFrames = SequenceEditor.GetFrameForX(mousePos.x) - Evt.Start;

                                if (IsSelected)
                                {
                                    if (Event.current.control)
                                    {
                                        SequenceEditor.Deselect(this);
                                    }
                                }
                                else
                                {
                                    if (Event.current.shift)
                                        SequenceEditor.Select(this);
                                    else if (!Event.current.control)
                                        SequenceEditor.SelectExclusive(this);
                                }
                                Event.current.Use();
                            }
                        }
                        else if (Event.current.button == 1 && rect.Contains(Event.current.mousePosition)) // right click?
                        {
                            CreateEventContextMenu().ShowAsContext();
                            Event.current.Use();
                        }
                    }
                    break;

                case EventType.MouseUp:
                    if (EditorGUIUtility.hotControl == GuiId)
                    {
                        EditorGUIUtility.hotControl = 0;
                        Event.current.Use();
                        SequenceEditor.Repaint();
                        FinishMovingEventEditors();
                    }
                    break;

                case EventType.MouseDrag:
                    if (EditorGUIUtility.hotControl == GuiId)
                    {
                        int t = SequenceEditor.GetFrameForX(Event.current.mousePosition.x) - _offsetFrames;

                        int delta = t - Evt.Start;

                        SequenceEditor.MoveEvents(delta);
                        Event.current.Use();
                    }
                    break;
                case EventType.Ignore:
                    if (EditorGUIUtility.hotControl == GuiId)
                    {
                        EditorGUIUtility.hotControl = 0;
                        SequenceEditor.Repaint();
                        FinishMovingEventEditors();
                    }
                    break;
            }
        }

        public override GUIStyle GetEventStyle()
        {
            if (_singleFrameStyle == null)
                _singleFrameStyle = FUtility.GetFluxSkin().GetStyle("SingleFrame");
            return _singleFrameStyle;
        }
        public override Color GetColor()
        {
            return Color.white;
        }
    }
}
