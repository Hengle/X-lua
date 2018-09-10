namespace CS.VFXConfig
{
    using UnityEngine.Playables;
    using UnityEngine.Timeline;

    [System.Serializable]
    public abstract class BasePlayable : PlayableAsset, ITimelineClipAsset
    {
        public virtual ClipCaps clipCaps
        {
            get
            {
                return ClipCaps.All;
            }
        }
    }
}