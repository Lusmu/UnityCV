using UnityEngine;
using System.Collections;

public static class EasingHelper
{
	public static float EaseOutCirc (float progress, float startValue, float targetValue)
	{
		return targetValue * Mathf.Sqrt(1 - progress * progress) + startValue;
	}

	public static float EaseInCirc (float progress, float startValue, float targetValue)
	{
		return -targetValue * (Mathf.Sqrt(1 - progress * progress) - 1) + startValue;
	}
}
