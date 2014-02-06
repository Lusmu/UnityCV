using UnityEngine;
using System.Collections;

public class PagesManager : MonoBehaviour
{
	[SerializeField]
	private Page[] pages;

	public Page Current { get; private set; }
	
	public void OpenPage(Page page)
	{
		if (Current != null)
		{
			Current.Hide();
		}

		Current = page;

		Current.Reveal();
	}
}
