namespace CS.VFXConfig
{
    using System;
    using System.ComponentModel;
    using UnityEngine.Timeline;

    /// <summary>
    /// 配置与时间相关.是否与当前时间线重合(时间线的开始时刻相同),取决于具体配置
    /// </summary>
    [Serializable, DisplayName("特效组")]
    [TrackClipType(typeof(TrackAsset)), TrackMediaType(TimelineAsset.MediaType.Group)]
    public class EffectTrackGroup : BaseTrackGroup<TrackAsset>
    {

    }
}