using UnityEngine;
using System.Collections;

public class TouchToBounce : MonoBehaviour {
	[SerializeField]
	private Vector3 bounceForce = new Vector3(0, -10, 0);

	[SerializeField]
	private Color hotColor = Color.red;

	[SerializeField]
	private float colorChangeSpeed = 1;

	[SerializeField]
	private float hotnessCooldown = 1;

	[SerializeField]
	private float hotnessPerTouch = 1;

	[SerializeField]
	private TouchToBounce pair;

	[SerializeField]
	private Rigidbody targetRigidbody;

	[SerializeField]
	private Renderer targetRenderer;

	private Color originalColor;
	private float hotness;
	private float targetHotness;

	void Awake()
	{
		if (targetRigidbody == null) Debug.LogError("Missing targetRigidbody!");
		if (targetRenderer == null) Debug.LogError("Missing targetRenderer!");

		originalColor = targetRenderer.material.color;
	}

	public void Bounce()
	{
		rigidbody.AddForce(bounceForce, ForceMode.Force);
		targetHotness = Mathf.Clamp01(targetHotness + hotnessPerTouch);
	}

	public void BounceNegative()
	{
		rigidbody.AddForce(-bounceForce, ForceMode.Force);
	}

	void OnMouseUpAsButton ()
	{
		Bounce();
		if (pair != null) pair.BounceNegative();
	}
	
	void Update()
	{
		hotness = Mathf.MoveTowards(hotness, targetHotness, colorChangeSpeed * Time.deltaTime);
		targetRenderer.material.color = Color.Lerp(originalColor, hotColor, hotness);
		if (targetHotness > 0) targetHotness -= Time.deltaTime * hotnessCooldown;
	}
}
