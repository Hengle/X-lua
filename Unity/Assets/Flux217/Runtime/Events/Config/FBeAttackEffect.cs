using UnityEngine;
using System.Collections;

namespace Flux
{
    [FEvent("基础配置/受击效果")]
    public class FBeAttackEffect : FEvent
    {
        [SerializeField, HideInInspector]
        private AnimationClip _beAttackClip;
        [SerializeField, HideInInspector]
        private int _effectId;
    }
}
