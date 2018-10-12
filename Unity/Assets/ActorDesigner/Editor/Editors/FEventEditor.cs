using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Flux;
using System;

namespace FluxEditor
{
    public class FEventEditor : FEditor
    {
        public FTrackEditor TrackEditor { get { return (FTrackEditor)Owner; } }
        public FEvent Evt { get { return (FEvent)Obj; } }

        private int _mouseOffsetFrames;
        private bool _isSingleFrame = false;
        private GUIStyle _singleFrameStyle = null;
        private Vector2 _singleSize;

        protected Rect _eventRect;

        public override FSequenceEditor SequenceEditor { get { return TrackEditor != null ? TrackEditor.SequenceEditor : null; } }

        public override float Height { get { return FTrackEditor.DEFAULT_TRACK_HEIGHT; } }

        public override void Init(FObject obj, FEditor owner)
        {
            base.Init(obj, owner);

            Type evtType = obj.GetType();
            object[] customAttributes = evtType.GetCustomAttributes(typeof(FEventAttribute), false);
            if (customAttributes.Length > 0)
                _isSingleFrame = ((FEventAttribute)customAttributes[0]).isSingleFrame;

            if (_isSingleFrame && Evt.FrameRange.Length != 1)
            {
                FrameRange range = Evt.FrameRange;
                range.Length = 1;
                Evt.FrameRange = range;
            }

            if (_singleFrameStyle == null)
                _singleFrameStyle = FUtility.GetFluxSkin().GetStyle("SingleFrame");

            GUIContent singleWidth = new GUIContent(FUtility.GetFluxTexture("SingleFrame_Active.png"));
            _singleSize = _singleFrameStyle.CalcSize(singleWidth);
        }

        public override void Render(Rect rect, float headerWidth)
        {

        }

        // pixelsPerFrame can be calculated from rect and viewRange, but being cached on a higher level
        public void Render(Rect rect, FrameRange viewRange, float pixelsPerFrame, FrameRange validKeyframeRange)
        {
            Rect = rect;

            _eventRect = rect;

            int eventStartFrame = Mathf.Max(Evt.Start, viewRange.Start);
            int eventEndFrame = Mathf.Min(Evt.End, viewRange.End);

            //			_eventRect.xMin = Evt.Start * pixelsPerFrame;
            //			_eventRect.xMax = Evt.End * pixelsPerFrame;
            _eventRect.xMin = SequenceEditor.GetXForFrame(eventStartFrame);
            _eventRect.xMax = SequenceEditor.GetXForFrame(eventEndFrame);

            //			_eventRect.xMin += (eventStartFrame-viewRange.Start) * pixelsPerFrame; // first set the start
            //			_eventRect.xMax = _eventRect.xMin + Mathf.Max( 4, (eventEndFrame-eventStartFrame) * pixelsPerFrame ); // then width

            if (_eventRect.Contains(Event.current.mousePosition))
                SequenceEditor.SetMouseHover(Event.current.mousePosition.x, this);

            RenderEvent(viewRange, validKeyframeRange);
        }

        private int _leftHandleGuiId = 0;
        private int _rightHandleGuiId = 0;
  

        public override void ReserveGuiIds()
        {
            base.ReserveGuiIds();
            _leftHandleGuiId = EditorGUIUtility.GetControlID(FocusType.Passive);
            _rightHandleGuiId = EditorGUIUtility.GetControlID(FocusType.Passive);
        }

        protected virtual void RenderEvent(FrameRange viewRange, FrameRange validKeyframeRange)
        {
            if (_isSingleFrame)
                SingleFrameRender(viewRange, validKeyframeRange);
            else
                RangeFrameRender(viewRange, validKeyframeRange);
        }

        private void RangeFrameRender(FrameRange viewRange, FrameRange validKeyframeRange)
        {
            bool leftHandleVisible = viewRange.Contains(Evt.Start);
            bool rightHandleVisible = viewRange.Contains(Evt.End);

            Rect leftHandleRect = _eventRect;
            leftHandleRect.width = 4;
            Rect rightHandleRect = _eventRect;
            rightHandleRect.xMin = rightHandleRect.xMax - 4;

            if (leftHandleVisible && IsSelected)
                EditorGUIUtility.AddCursorRect(leftHandleRect, MouseCursor.ResizeHorizontal, _leftHandleGuiId);

            if (rightHandleVisible && IsSelected)
                EditorGUIUtility.AddCursorRect(rightHandleRect, MouseCursor.ResizeHorizontal, _rightHandleGuiId);

            //			if( SequenceEditor.EditorDragged == this )
            //			{
            //				Rect editorDraggedRect = _eventRect;
            //				editorDraggedRect.y = -Offset.value.y;
            //
            //				SequenceEditor.EditorDraggedRect = editorDraggedRect;
            //
            //				SequenceEditor.Repaint();
            //			}

            switch (Event.current.type)
            {
                case EventType.Repaint:
                    if (!viewRange.Overlaps(Evt.FrameRange))
                    {
                        break;
                    }

                    GUIStyle evtStyle = GetEventStyle();

                    GUI.backgroundColor = GetColor();

                    evtStyle.Draw(_eventRect, _isSelected, _isSelected, false, false);

                    string text = Evt.Text;
                    if (text != null)
                    {
                        GUIStyle style = GetTextStyle();
                        Vector2 size = style.CalcSize(new GUIContent(text));
                        Rect rect = _eventRect;
                        rect.size = size;
                        GUI.Label(rect, text, GetTextStyle());
                    }
                    break;

                case EventType.MouseDown:
                    if (EditorGUIUtility.hotControl == 0 && IsSelected && !Event.current.control && !Event.current.shift)
                    {
                        Vector2 mousePos = Event.current.mousePosition;

                        if (Event.current.button == 0) // left click?
                        {
                            if (rightHandleVisible && rightHandleRect.Contains(mousePos))
                            {
                                EditorGUIUtility.hotControl = _rightHandleGuiId;
                                //						keyframeOnSelect = evt.Start;
                                Event.current.Use();
                            }
                            else if (leftHandleVisible && leftHandleRect.Contains(mousePos))
                            {
                                EditorGUIUtility.hotControl = _leftHandleGuiId;
                                //						keyframeOnSelect = evt.End;
                                Event.current.Use();
                            }
                            else if (_eventRect.Contains(mousePos))
                            {
                                EditorGUIUtility.hotControl = GuiId;
                                _mouseOffsetFrames = SequenceEditor.GetFrameForX(mousePos.x) - Evt.Start;

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
                        else if (Event.current.button == 1 && _eventRect.Contains(Event.current.mousePosition)) // right click?
                        {
                            CreateEventContextMenu().ShowAsContext();
                            Event.current.Use();
                        }
                    }
                    break;

                case EventType.MouseUp:
                    if (EditorGUIUtility.hotControl != 0)
                    {
                        if (EditorGUIUtility.hotControl == GuiId
                           || EditorGUIUtility.hotControl == _leftHandleGuiId
                           || EditorGUIUtility.hotControl == _rightHandleGuiId)
                        {
                            EditorGUIUtility.hotControl = 0;
                            Event.current.Use();
                            SequenceEditor.Repaint();
                            FinishMovingEventEditors();
                        }
                    }
                    break;

                case EventType.MouseDrag:
                    if (EditorGUIUtility.hotControl != 0)
                    {
                        if (EditorGUIUtility.hotControl == GuiId)
                        {

                            //						if( !TrackEditor.Rect.Contains( Event.current.mousePosition )  )
                            //						{
                            //							SequenceEditor.StartDraggingEditor( this );
                            //						}
                            //						else
                            {
                                int t = SequenceEditor.GetFrameForX(Event.current.mousePosition.x) - _mouseOffsetFrames;

                                int delta = t - Evt.Start;

                                SequenceEditor.MoveEvents(delta);
                            }
                            Event.current.Use();

                        }
                        else if (EditorGUIUtility.hotControl == _leftHandleGuiId || EditorGUIUtility.hotControl == _rightHandleGuiId)
                        {
                            int leftLimit = 0;
                            int rightLimit = 0;

                            bool draggingStart = EditorGUIUtility.hotControl == _leftHandleGuiId;

                            if (draggingStart)
                            {
                                leftLimit = validKeyframeRange.Start;
                                rightLimit = Evt.End - 1;
                            }
                            else
                            {
                                leftLimit = Evt.Start + 1;
                                rightLimit = validKeyframeRange.End;
                            }


                            int t = SequenceEditor.GetFrameForX(Event.current.mousePosition.x);

                            t = Mathf.Clamp(t, leftLimit, rightLimit);

                            int delta = t - (draggingStart ? Evt.Start : Evt.End);

                            if (draggingStart)
                            {
                                int newLength = Evt.Length - delta;
                                if (newLength < Evt.GetMinLength())
                                {
                                    delta += newLength - Evt.GetMinLength();
                                }
                                if (newLength > Evt.GetMaxLength())
                                {
                                    delta += newLength - Evt.GetMaxLength();
                                }
                            }
                            else
                            {
                                int newLength = Evt.Length + delta;
                                if (newLength < Evt.GetMinLength())
                                {
                                    delta -= newLength - Evt.GetMinLength();
                                }
                                if (newLength > Evt.GetMaxLength())
                                {
                                    delta -= newLength - Evt.GetMaxLength();
                                }
                            }

                            if (delta != 0)
                            {
                                if (draggingStart)
                                    SequenceEditor.ResizeEventsLeft(delta);
                                else
                                    SequenceEditor.ResizeEventsRight(delta);
                            }

                            Event.current.Use();
                        }
                    }
                    break;

                case EventType.Ignore:
                    if (EditorGUIUtility.hotControl == GuiId
                       || EditorGUIUtility.hotControl == _leftHandleGuiId
                       || EditorGUIUtility.hotControl == _rightHandleGuiId)
                    {
                        EditorGUIUtility.hotControl = 0;
                        SequenceEditor.Repaint();
                        FinishMovingEventEditors();
                    }
                    break;
            }
        }
        private void SingleFrameRender(FrameRange viewRange, FrameRange validKeyframeRange)
        {
            Rect rect = _eventRect;
            rect.width = 15;
            rect.x = SequenceEditor.PixelsPerFrame * Evt.Start;
            FrameRange range = Evt.FrameRange;
            range.Start = SequenceEditor.GetFrameForX(rect.x);
            range.End = range.Start + 1 + _mouseOffsetFrames + SequenceEditor.GetFrameForX(rect.width);
            Evt.FrameRange = range;
            switch (Event.current.type)
            {
                case EventType.Repaint:
                    {
                        if (!viewRange.Overlaps(Evt.FrameRange))
                            break;

                        _mouseOffsetFrames = 0;                       

                        GUIStyle evtStyle = _singleFrameStyle;
                        GUI.backgroundColor = Color.white;
                        Rect renderRect = rect;
                        renderRect.x -= 2.5f;
                        evtStyle.Draw(renderRect, _isSelected, _isSelected, false, false);

                        string text = Evt.Text;
                        if (text != null)
                        {
                            GUIStyle style = GetTextStyle();
                            Vector2 size = style.CalcSize(new GUIContent(text));
                            rect.size = size;
                            GUI.Label(rect, text, GetTextStyle());
                        }
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
                                _mouseOffsetFrames = SequenceEditor.GetFrameForX(mousePos.x) - Evt.Start;

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
                        int t = SequenceEditor.GetFrameForX(Event.current.mousePosition.x) - _mouseOffsetFrames;

                        int delta = t - Evt.Start;
                        if (t < viewRange.Start)
                            t = viewRange.Start;
                        else if (t > viewRange.End - 1)
                            t = viewRange.End - 1;
                        Evt.SingleFrame = t + 1;

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
        public virtual void OnEventMoved(FrameRange oldFrameRange)
        {
            AddEventEditorBeingMoved(this, oldFrameRange);
        }

        public virtual void OnEventFinishedMoving(FrameRange oldFrameRange)
        {

        }

        public virtual Color GetColor()
        {
            return FluxEditor.FUtility.GetEventColor(Evt.GetType().ToString());
        }

        public virtual GUIStyle GetEventStyle()
        {
            return FUtility.GetEventStyle();
        }

        public virtual GUIStyle GetTextStyle()
        {
            return EditorStyles.label;
        }

        protected virtual GenericMenu CreateEventContextMenu()
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("复制"), false, Copy);
            menu.AddItem(new GUIContent("剪切"), false, Cut);
            menu.AddItem(new GUIContent("删除"), false, Delete);

            return menu;
        }

        private void Copy()
        {
            SequenceEditor.CopyEditor(this);
        }

        private void Cut()
        {
            SequenceEditor.CutEditor(this);
        }

        private void Delete()
        {
            List<FEventEditor> eventEditors = new List<FEventEditor>();
            eventEditors.Add(this);
            SequenceEditor.DestroyEvents(eventEditors);
        }

        #region Event editors being moved

        // holds the event editors being moved / resized, so that we can call a function at the end of the move
        // with the full change and not just the delta each update. Useful when you have other assets that have
        // to be changed but you only want to do it at the end (e.g. resize animations, to minimize errors)
        private static Dictionary<FEventEditor, FrameRange> _eventEditorsBeingMoved = new Dictionary<FEventEditor, FrameRange>();

        private static void AddEventEditorBeingMoved(FEventEditor evtEditor, FrameRange oldFrameRange)
        {
            if (!_eventEditorsBeingMoved.ContainsKey(evtEditor))
            {
                _eventEditorsBeingMoved.Add(evtEditor, oldFrameRange);
            }
        }

        public static void FinishMovingEventEditors()
        {
            Dictionary<FEventEditor, FrameRange>.Enumerator e = _eventEditorsBeingMoved.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Key.OnEventFinishedMoving(e.Current.Value);
            }
            _eventEditorsBeingMoved.Clear();
        }

        #endregion
    }
}
