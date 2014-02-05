using UnityEngine;
using System.Collections;

public class Page : MonoBehaviour
{
	[SerializeField]
	private float hideDepth = -100;
	[SerializeField]
	private float hideSpeed = 120;
	[SerializeField]
	private float revealSpeed = 80;

	private bool isHidden;
	private Transform cachedTransform;
	private bool childrenDeactivated;
	private float revealProgress;

	void Awake()
	{
		cachedTransform = transform;
		cachedTransform.localPosition = Vector3.up * hideDepth;
		isHidden = true;
		revealProgress = 0;
		Deactivate();
	}

	void Start ()
	{
		transform.localPosition = Vector3.up * hideDepth;
	}

	void Activate()
	{
		childrenDeactivated = false;
		for (int i = 0; i < cachedTransform.childCount; i++)
		{
			cachedTransform.GetChild(i).gameObject.SetActive(true);
		}
	}

	void Deactivate()
	{
		childrenDeactivated = true;
		for (int i = 0; i < cachedTransform.childCount; i++)
		{
			cachedTransform.GetChild(i).gameObject.SetActive(false);
		}
	}

	void Update()
	{
		if (isHidden)
		{
			revealProgress = Mathf.MoveTowards(revealProgress, 0, hideSpeed * Time.deltaTime);
			if (!childrenDeactivated)
			{
				if (cachedTransform.localPosition.y > hideDepth)
				{
					revealProgress = Mathf.MoveTowards(revealProgress, 0, hideSpeed * Time.deltaTime);
					
					float height = EasingHelper.EaseInCirc(1 - revealProgress, 0, 1) * hideDepth;
					
					cachedTransform.localPosition = Vector3.up * height;
				}
				else
				{
					Deactivate();
				}
			}
		}
		else
		{
			revealProgress = Mathf.MoveTowards(revealProgress, 1, revealSpeed * Time.deltaTime);

			float height = EasingHelper.EaseInCirc(1 - revealProgress, 0, 1) * hideDepth;

			cachedTransform.localPosition = Vector3.up * height;
		}
	}

	public void Reveal()
	{
		isHidden = false;

		if (childrenDeactivated) Activate();
	}

	public void Hide()
	{
		isHidden = true;
	}
}
