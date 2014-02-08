using UnityEngine;
using System.Collections;

namespace TimoMoisio.CV
{
	[RequireComponent(typeof(Renderer))]
	public class FlashOnButtonPress : MonoBehaviour
	{
		[SerializeField]
		private Button button;

		[SerializeField]
		private float inTime = 0.5f;

		[SerializeField]
		private float stayTime = 2;

		[SerializeField]
		private float outTime = 2;

		[SerializeField]
		private string flashShaderProperty = "_Threshold";

		private float direction = 1;
		private float threshold = 0;
		private Renderer cachedRenderer;
		private float inDelta;
		private float outDelta;
		private float stayTimer;

		void Awake()
		{
			cachedRenderer = renderer;
			inDelta = 1 / inTime;
			outDelta = 1 / outTime;
			threshold = 0;
			direction = 0;
			SetThreshold();
		}

		void OnEnable()
		{
			button.OnButtonPress += OnButtonPress;
		}

		void OnDisable()
		{
			button.OnButtonPress -= OnButtonPress;
		}

		void OnButtonPress(Button button)
		{
			direction = 1;
			stayTimer = 0;
		}

		void Update()
		{
			if (direction > 0) 
			{
				threshold = Mathf.MoveTowards(threshold, 1, inDelta * Time.deltaTime);
				if (Mathf.Approximately(threshold, 1)) 
				{
					stayTimer += Time.deltaTime;
					if (stayTimer >= stayTime)
					{
						direction = -1;
					}
				}
				SetThreshold();
			}
			else if (direction < 0)
			{
				threshold = Mathf.MoveTowards(threshold, 0, inDelta * Time.deltaTime);
				if (Mathf.Approximately(threshold, 1)) direction = 0;
				stayTimer = 0;
				SetThreshold();
			}
		}

		void SetThreshold()
		{
			cachedRenderer.material.SetFloat(flashShaderProperty, threshold);
		}
	}
}
