namespace CS.VFXConfig
{
    using System.Collections.Generic;
    using UnityEngine.Timeline;

    [System.Serializable]
    public abstract class BaseTrackGroup<T> : GroupTrack where T: TrackAsset
    {
        public List<T> TrackList = new List<T>();
    }
}