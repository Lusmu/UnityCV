using UnityEngine;
using System.Collections;

namespace TimoMoisio.CV
{
	[RequireComponent(typeof(ParticleSystem))]
	public class ButtonParticles : MonoBehaviour 
	{
		[SerializeField]
		private Button launcherButton;

		[SerializeField]
		private int emitCount = 3;

		private ParticleSystem cachedParticleSystem;
		private Transform cachedTransform;

		void Awake()
		{
			cachedParticleSystem = particleSystem;
			cachedTransform = transform;
		}

		void OnEnable()
		{
			launcherButton.OnButtonPress += OnButtonPress;
			cachedParticleSystem.Emit(1);
		}

		void OnDisable()
		{
			launcherButton.OnButtonPress -= OnButtonPress;
		}

		void OnButtonPress(Button button)
		{
			cachedParticleSystem.Emit(emitCount);
		}

		/*
		void Update ()
		{
			if (cachedTransform.position.y < HeightFogSetter.Instance.fogStartHeight
			    && cachedParticleSystem.isPlaying)
			{
				cachedParticleSystem.Stop();
			}
		}*/
	}
}