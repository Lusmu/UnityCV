using UnityEngine;
using System.Collections;

namespace TimoMoisio.CV
{
	public class NaviButton : Button
	{
		[SerializeField]
		private Renderer background;

		[SerializeField]
		private Color selectedColor = new Color(1, 0.5f, 0.1f);
		
		[SerializeField]
		private float colorChangeSpeed = 2;

		private Color originalColor;

		private bool _isSelected = false;
		public bool IsSelected 
		{
			get
			{
				return _isSelected;
			}
			set
			{
				_isSelected = value;
			}
		}

		void Awake()
		{
			originalColor = background.material.color;
		}

		void Update()
		{
			if (IsSelected)
			{
				background.material.color = MoveTowardColor(background.material.color, selectedColor, Time.deltaTime * colorChangeSpeed);
			}
			else
			{
				background.material.color = MoveTowardColor(background.material.color, originalColor, Time.deltaTime * colorChangeSpeed);
			}
		}

		Color MoveTowardColor(Color startColor, Color endColor, float maxDelta)
		{
			float r = Mathf.MoveTowards(startColor.r, endColor.r, maxDelta);
			float g = Mathf.MoveTowards(startColor.g, endColor.g, maxDelta);
			float b = Mathf.MoveTowards(startColor.b, endColor.b, maxDelta);
			float a = Mathf.MoveTowards(startColor.a, endColor.a, maxDelta);
			return new Color(r, g, b, a);
		}
	}
}