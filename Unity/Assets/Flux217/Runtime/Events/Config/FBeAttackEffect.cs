using UnityEngine;
using System.Collections;

namespace Flux
{
    [FEvent("基础配置/受击效果")]
    public class FBeAttackEffect : FEvent
    {
        [SerializeField, HideInInspector]
        [FEventField("受击动画")]
        private AnimationClip _beAttackClip;
        [SerializeField, HideInInspector]
        [FEventField("特效组ID")]
        private int _effectGroupId;

    }
}
