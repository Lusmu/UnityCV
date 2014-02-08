using UnityEngine;
using System.Collections;

namespace TimoMoisio.CV
{
	public class BounceOnButtonPress : MonoBehaviour
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
		private Vector3 bounceAmount;

		private float direction = 1;

		private float position = 0;

		private float inDelta;
		private float outDelta;
		private float stayTimer;
		private Transform cachedTransform;
		private Vector3 originalPosition;
		
		void Awake()
		{
			cachedTransform = transform;
			inDelta = 1 / inTime;
			outDelta = 1 / outTime;
			direction = 0;
			position = 0;
			originalPosition = cachedTransform.localPosition;
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
				position = Mathf.MoveTowards(position, 1, inDelta * Time.deltaTime);
				if (Mathf.Approximately(position, 1)) 
				{
					stayTimer += Time.deltaTime;
					if (stayTimer >= stayTime)
					{
						direction = -1;
					}
				}
				SetPosition();
			}
			else if (direction < 0)
			{
				position = Mathf.MoveTowards(position, 0, inDelta * Time.deltaTime);
				if (Mathf.Approximately(position, 1)) direction = 0;
				stayTimer = 0;
				SetPosition();
			}
		}
		
		void SetPosition()
		{
			cachedTransform.localPosition = 
				Vector3.Lerp(
					originalPosition,
					originalPosition + bounceAmount,
					position);
		}
	}
}