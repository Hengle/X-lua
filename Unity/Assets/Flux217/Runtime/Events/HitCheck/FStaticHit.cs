using UnityEngine;
using System.Collections;
using XmlCode.Skill;

namespace Flux
{
    [FEvent("静态碰撞检测")]
    public class FStaticHit : FEvent
    {
        [SerializeField, HideInInspector]
        private FHitZone _zone;
        [SerializeField, HideInInspector]
        private FPlaySequenceEvent _sequence;

        public override string Text { get { return string.Format("检测区域{0}", _zone.ZoneID); } set { } }

        protected bool CheckCube()
        {
            return false;
        }
        protected bool CheckSphere()
        {
            return false;
        }
        protected bool CheckCylinder()
        {
            return false;
        }


        protected virtual bool HasData()
        {
            bool hasZone = _zone != null;
            if (hasZone)
                Debug.LogError("未配置碰撞区域数据");
            bool hasSequence = _sequence != null;
            if (hasSequence)
                Debug.LogError("未配置序列信息");

            return hasZone && hasSequence;
        }
        

        protected override void OnUpdateEvent(float timeSinceTrigger)
        {
            base.OnUpdateEvent(timeSinceTrigger);

            if (!HasData()) return;
           
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (!HasData()) return;
        }
        protected override void OnResume()
        {
            base.OnResume();
            if (!HasData()) return;
        }
        protected override void OnStop()
        {
            base.OnStop();
            if (!HasData()) return;
        }
        protected override void OnFinish()
        {
            base.OnFinish();
            if (!HasData()) return;
        }
    }
}
