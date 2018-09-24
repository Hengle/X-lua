using UnityEngine;
using System.Collections;

namespace Flux
{
    [FEvent("基础配置/碰撞区域", true)]
    public class FHitZone : FEvent
    {
        [SerializeField, HideInInspector]
        [FEventField("区域ID")]
        private int _zoneId;
    }
}
