namespace CS.VFXConfig
{
    using System;
    using System.ComponentModel;
    using UnityEngine.Timeline;

    /// <summary>
    /// 配置与时间相关
    /// </summary>
    [Serializable, DisplayName("时间事件组")]
    [TrackClipType(typeof(TrackAsset)), TrackMediaType(TimelineAsset.MediaType.Group)]
    public class TimeEventTrackGroup : BaseTrackGroup<TrackAsset>
    {
        

    }
}