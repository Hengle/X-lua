using UnityEngine;
using System.Collections;
using XmlCode.Skill;

namespace Flux
{
    [FEvent("控制器/直线位移")]
    public class FCastObject : FController
    {
        [SerializeField, HideInInspector]
        private int _curveId;
        [SerializeField, HideInInspector]
        private bool _isTraceTarget;
        [SerializeField, HideInInspector]
        private bool _passBody;
        [SerializeField, HideInInspector]
        private Vector3 _position;
        [SerializeField, HideInInspector]
        private Vector3 _eulerAngles;

        private Transform _targetObj;
        private float _startTime;

        protected override void OnTrigger(float timeSinceTrigger)
        {
            base.OnTrigger(timeSinceTrigger);

            var go = FUtility.FindGameObject(_path);
            if (go != null)
            {
                _targetObj = go.transform;
                _targetObj.localPosition += _offset;
                Vector3 eulerAngles = _targetObj.localEulerAngles;
                eulerAngles.y = _angle;
                _targetObj.localEulerAngles = eulerAngles;
            }
            _startTime = timeSinceTrigger;
        }

        protected override void OnUpdateEvent(float timeSinceTrigger)
        {
            base.OnUpdateEvent(timeSinceTrigger);

            if (_targetObj == null) return;
            float t = timeSinceTrigger - _startTime;
            Vector3 dir = Vector3.zero;
            switch (_moveType)
            {
                case MoveType.MoveToTarget:
                    break;
                case MoveType.MoveInDirection:
                    dir = _targetObj.forward; 
                    break;
                default:
                    break;
            }

        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
