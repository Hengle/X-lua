using UnityEngine;
using System.Collections;

namespace Flux
{
    [FEvent("特效/音频音量")]
    public class FVolumeAudioEvent : FTweenEvent<FTweenFloat>
    {
        private AudioSource _source;

        public override string Text
        {
            get
            {
                string str = "Miss";
                if (_source != null && _source.clip != null)
                    str = _source.clip.name;
                return str;
            }
            set { }
        }

        protected override void OnInit()
        {
            _source = Owner.GetComponent<AudioSource>();
            if (_source == null)
                _source = Owner.gameObject.AddComponent<AudioSource>();
        }

        protected override void ApplyProperty(float t)
        {
            _source.volume = _tween.GetValue(t);
        }
    }
}
