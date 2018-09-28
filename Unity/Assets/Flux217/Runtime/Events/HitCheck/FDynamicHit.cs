using UnityEngine;
using System.Collections;
using XmlCode.Skill;

namespace Flux
{
    [FEvent("静态碰撞检测")]
    public class FDynamicHit : FStaticHit
    {
        [SerializeField, HideInInspector]
        private string _target;
     
        protected override void OnInit()
        {
            base.OnInit();
        }
        protected override void OnTrigger(float timeSinceTrigger)
        {
            base.OnTrigger(timeSinceTrigger);
        }

        protected override void OnUpdateEvent(float timeSinceTrigger)
        {
            base.OnUpdateEvent(timeSinceTrigger);
        }

        protected override void OnPause()
        {
            base.OnPause();
        }
        protected override void OnResume()
        {
            base.OnResume();
        }
        protected override void OnFinish()
        {
            base.OnFinish();
        }
        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
