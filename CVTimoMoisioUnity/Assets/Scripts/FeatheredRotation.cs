using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TimoMoisio.CV
{
	public class FeatheredRotation 
	{
		private RotationTarget[] _targets;

		public FeatheredRotation(Transform[] targets)
		{
			this._targets = new RotationTarget[targets.Length];
			for (int i = 0; i < _targets.Length; i++)
			{
				_targets[i] = new RotationTarget(){
					transform = targets[i],
					endRotation = targets[i].localRotation
				};
			}
		}

		public void SetTo(Vector3 targetRotation)
		{
			for (int i = 0; i < _targets.Length; i++)
			{
				_targets[i].endRotation = Quaternion.Euler(targetRotation);
				_targets[i].rotationSpeed = 0;
				_targets[i].pauseTime = 0;
				_targets[i].transform.localRotation = _targets[i].endRotation;
			}
		}

		public void RotateTo(Vector3 targetRotation, float speed, float interval)
		{
			for (int i = 0; i < _targets.Length; i++)
			{
				_targets[i].endRotation = Quaternion.Euler(targetRotation);
				_targets[i].rotationSpeed = speed;
				_targets[i].pauseTime = interval * i;
			}
		}

		public void UpdatePositions(float delta)
		{
			for (int i = 0; i < _targets.Length; i++)
			{
				_targets[i].Rotate(Time.deltaTime);
			}
		}

		private class RotationTarget
		{
			public Transform transform;
			public Quaternion endRotation;
			public float rotationSpeed;
			public float pauseTime;

			public void Rotate(float delta)
			{
				if (pauseTime > 0)
				{
					pauseTime -= delta;
				}
				else
				{
					transform.localRotation
						= Quaternion.RotateTowards(
							transform.localRotation,
							endRotation,
							rotationSpeed * delta); 
				}
			}
		}
	}
}