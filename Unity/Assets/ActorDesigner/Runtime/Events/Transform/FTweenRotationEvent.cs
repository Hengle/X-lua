﻿using UnityEngine;

namespace Flux
{
	[FEvent("模型控制/旋转动画", typeof(FTransformTrack))]
	public class FTweenRotationEvent : FTransformEvent
	{
		private Quaternion _startRotation;

		protected override void OnTrigger( float timeSinceTrigger )
		{
			_startRotation = Owner.localRotation;
			base.OnTrigger(timeSinceTrigger);
		}

		protected override void SetDefaultValues ()
		{
			_tween = new FTweenVector3( Vector3.zero, new Vector3(0, 360, 0) );
		}

		protected override void ApplyProperty( float t )
		{
//			Owner.localRotation = _tween.GetValue( t );
			Owner.localRotation = Quaternion.Euler( _tween.GetValue( t ) );
		}

		protected override void OnStop ()
		{
			Owner.localRotation = _startRotation;
		}
	}
}
