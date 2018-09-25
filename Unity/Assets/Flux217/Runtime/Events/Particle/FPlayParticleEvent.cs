﻿using UnityEngine;
using System.Collections;

namespace Flux
{
    [FEvent("粒子系统/播放粒子特效", typeof(FParticleTrack))]
    public class FPlayParticleEvent : FEvent
    {

        [SerializeField, HideInInspector]
        [Tooltip("True: ParticleSystem playback speed will be adjusted to match event length"
                 + "\nFalse: ParticleSystem plays at normal speed, i.e. doesn't scale based on event length")]
        private bool _normalizeToEventLength = false;

        [SerializeField]
        [HideInInspector]
        [Tooltip("Seed to randomize the particle system, 0 = always randomize")]
        private uint _randomSeed = 1;

        [SerializeField]
        [HideInInspector]
        private ParticleSystem _particleSystem = null;

        private float _previousTimeSinceTrigger = 0;

        private float _previousSpeed = 0;

        protected override void OnInit()
        {
            if (_particleSystem != null)
            {
                if (!_particleSystem.isPlaying)//--播放粒子时设置随机种子会报错,但不影响配置
                    _particleSystem.randomSeed = _randomSeed;
                ParticleSystem.MainModule mainModule = _particleSystem.main;
                mainModule.simulationSpeed = Sequence.Speed;
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("FParticleEvent 绑定对象无 ParticleSystem");
#endif
            }
            _previousTimeSinceTrigger = 0;
            _previousSpeed = Sequence.Speed;
        }

        protected override void OnTrigger(float timeSinceTrigger)
        {
            //			if( _particleSystem != null && Sequence.IsPlaying && Sequence.IsPlayingForward )
            _particleSystem.Play(true);
        }

        protected override void OnFinish()
        {
            if (_particleSystem != null)
                _particleSystem.Stop(true);
        }

        protected override void OnStop()
        {
            if (_particleSystem != null)
            {
                _particleSystem.Stop(true);
                //_particleSystem.Clear(true);
            }
        }

        protected override void OnPause()
        {
            if (_particleSystem != null)
                _particleSystem.Pause();
        }

        protected override void OnResume()
        {
            if (_particleSystem != null && Sequence.IsPlayingForward)
                _particleSystem.Play(true);
        }

        protected override void OnUpdateEvent(float timeSinceTrigger)
        {
            if (_particleSystem == null)
                return;
            if (!Sequence.IsPlaying || !Sequence.IsPlayingForward)
            {
                _previousSpeed = 1;
                ParticleSystem.MainModule mainModule = _particleSystem.main;
                mainModule.simulationSpeed = _previousSpeed;
                float delta = timeSinceTrigger - _previousTimeSinceTrigger;
                _previousTimeSinceTrigger = timeSinceTrigger;
                if (Sequence.IsPlayingForward && delta > 0)
                {
                    _particleSystem.Simulate(delta, true, false);
                }
                else
                {
                    float t = _normalizeToEventLength ? (timeSinceTrigger / LengthTime) * _particleSystem.main.duration : Mathf.Clamp(timeSinceTrigger, 0, _particleSystem.main.duration);
                    _particleSystem.Simulate(t, true, true);
                }
            }
            else if (_previousSpeed != Sequence.Speed)
            {
                _previousSpeed = Sequence.Speed;
                ParticleSystem.MainModule mainModule = _particleSystem.main;
                mainModule.simulationSpeed = Mathf.Abs(_previousSpeed);
            }
        }
    }
}