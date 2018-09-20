using UnityEngine;
using System.Collections;

namespace Flux
{
	[FEvent("音效/音频音量")]
	public class FVolumeAudioEvent : FTweenEvent<FTweenFloat>
	{
		private AudioSource _source;

		protected override void OnInit()
		{
			_source = Owner.GetComponent<AudioSource>();
		}

		protected override void ApplyProperty( float t )
		{
			_source.volume = _tween.GetValue( t );
		}
	}
}
