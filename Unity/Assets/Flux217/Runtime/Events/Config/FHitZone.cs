using UnityEngine;
using System.Collections;
using XmlCode.Skill;

namespace Flux
{
    [FEvent("基础配置/碰撞区域")]
    public class FHitZone : FEvent
    {
        [SerializeField, HideInInspector]
        [FEventField("区域ID")]
        private int _zoneId;
        [SerializeField, HideInInspector]
        private HitSharpType _sharpType;
        [SerializeField, HideInInspector]
        [FEventField("偏移量")]
        private Vector3 _offset;
        [SerializeField, HideInInspector]
        [FEventField("最大数量", Tip = "可碰撞到对象的最大数量")]
        private int _maxNum;

        [SerializeField, HideInInspector]
        private Vector3 _scale;
        [SerializeField, HideInInspector]
        private float _radius;
        [SerializeField, HideInInspector]
        private float _height;
        [SerializeField, HideInInspector]
        private float _angle;
    }
}
