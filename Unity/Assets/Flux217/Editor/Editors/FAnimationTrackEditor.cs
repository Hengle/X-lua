using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System;

using Flux;

namespace FluxEditor
{
    [FEditor(typeof(FAnimationTrack))]
    public class FAnimationTrackEditor : FTrackEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            //SceneView.onSceneGUIDelegate -= OnSceneGUI;

            FAnimationTrack animTrack = (FAnimationTrack)Obj;
            DestroyImmediate(animTrack.AnimatorController);
            animTrack.AnimatorController = null;
            Resources.UnloadUnusedAssets();

            //if (SyncWithAnimationWindow)
            //    SyncWithAnimationWindow = false;
        }

        private void SetAnimator()
        {
            FAnimationTrack animTrack = (FAnimationTrack)Track;
            if (animTrack.Owner.GetComponent<Animator>() == null)
            {
                Animator animator = animTrack.Owner.gameObject.AddComponent<Animator>();
                Undo.RegisterCreatedObjectUndo(animator, string.Empty);
            }
            if (animTrack.AnimatorController == null)
            {
                var temp = FUtility.GetFluxAssets<UnityEditor.Animations.AnimatorController>("EditorAnimatorController.controller");
                var animatorController = Instantiate(temp);
                animTrack.LayerId = 0;
                animTrack.LayerName = "Base Layer";
                var layers = animatorController.layers;
                layers[animTrack.LayerId].name = animTrack.LayerName;
                layers[animTrack.LayerId].stateMachine.name = animTrack.LayerName;
                animatorController.layers = layers;
                animTrack.AnimatorController = animatorController;
                FAnimationTrackInspector.RebuildStateMachine(animTrack);
            }
        }

        public override void Init(FObject obj, FEditor owner)
        {
            base.Init(obj, owner);

            SetAnimator();
        }

        public override void OnTrackChanged()
        {
            FAnimationTrack animTrack = (FAnimationTrack)Track;
            FAnimationTrackInspector.RebuildStateMachine((FAnimationTrack)Track);

            SetAnimator();
        }

        public override void Render(Rect rect, float headerWidth)
        {
            base.Render(rect, headerWidth);

            switch (Event.current.type)
            {
                case EventType.DragUpdated:
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        int numAnimationsDragged = FAnimationEventInspector.NumAnimationsDragAndDrop(Track.Sequence.FrameRate);
                        int frame = SequenceEditor.GetFrameForX(Event.current.mousePosition.x);

                        DragAndDrop.visualMode = numAnimationsDragged > 0 && Track.CanAddAt(frame) ? DragAndDropVisualMode.Copy : DragAndDropVisualMode.Rejected;
                        Event.current.Use();
                    }
                    break;
                case EventType.DragPerform:
                    if (rect.Contains(Event.current.mousePosition))
                    {
                        AnimationClip animClip = FAnimationEventInspector.GetAnimationClipDragAndDrop(Track.Sequence.FrameRate);

                        if (animClip && Mathf.Approximately(animClip.frameRate, Track.Sequence.FrameRate))
                        {
                            int frame = SequenceEditor.GetFrameForX(Event.current.mousePosition.x);
                            int maxLength;

                            if (Track.CanAddAt(frame, out maxLength))
                            {
                                FPlayAnimationEvent animEvt = FEvent.Create<FPlayAnimationEvent>(new FrameRange(frame, frame + Mathf.Min(maxLength, Mathf.RoundToInt(animClip.length * animClip.frameRate))));
                                Track.Add(animEvt);
                                FAnimationEventInspector.SetAnimationClip(animEvt, animClip);
                                DragAndDrop.AcceptDrag();
                            }
                        }

                        Event.current.Use();
                    }
                    break;
            }
        }

        public override void UpdateEventsEditor(int frame, float time)
        {
            if (Track.RequiresEditorCache && !Track.HasCache && Track.CanCreateCache())
            {
                OnToggle(true);
            }

            base.UpdateEventsEditor(frame, time);

            //if (_syncWithAnimationWindow)
            //{
            //    FEvent[] evts = new FEvent[2];
            //    int numEvts = Track.GetEventsAt(frame, evts);
            //    if (numEvts > 0)
            //    {
            //        if (numEvts == 1)
            //        {
            //            _previousEvent = evts[0];
            //        }
            //        else if (numEvts == 2)
            //        {
            //            if (_previousEvent != evts[0] && _previousEvent != evts[1])
            //            {
            //                _previousEvent = evts[1];
            //            }
            //        }

            //        FPlayAnimationEvent animEvt = (FPlayAnimationEvent)_previousEvent;
            //        if (animEvt._animationClip != AnimationWindowProxy.GetSelectedAnimationClip())
            //        {
            //            AnimationWindowProxy.SelectAnimationClip(animEvt._animationClip);
            //        }

            //        AnimationWindowProxy.SetCurrentFrame(frame - animEvt.Start, time - animEvt.StartTime);
            //    }
            //}
        }
    }
}
