namespace CS.VFXConfig
{
    using System;
    using System.ComponentModel;
    using UnityEngine.Timeline;

    /// <summary>
    /// 与时间无关的配置
    /// </summary>
    [Serializable, DisplayName("基础配置组")]
    [TrackClipType(typeof(ConfigTrack)), TrackMediaType(TimelineAsset.MediaType.Group)]
    public class ConfigTrackGroup : BaseTrackGroup<ConfigTrack>
    {

    }
}