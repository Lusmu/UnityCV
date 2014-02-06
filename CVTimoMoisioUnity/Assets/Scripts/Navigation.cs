using UnityEngine;
using System.Collections;
using System.Linq;

namespace TimoMoisio.CV
{
	public class Navigation : MonoBehaviour 
	{
		private NavigationItem current;

		public NavigationItem[] items;

		void Start()
		{
			current = items[0];

			current.NavigateTo();
		}

		void OnEnable()
		{
			for (int i = 0; i < items.Length; i++)
			{
				items[i].button.OnButtonPress += OnButtonPress;
			}
		}

		void OnDisable()
		{
			for (int i = 0; i < items.Length; i++)
			{
				items[i].button.OnButtonPress -= OnButtonPress;
			}
		}

		void OnButtonPress(Button button)
		{
			NaviButton naviButton = (NaviButton)button;
			NavigationItem item = items.FirstOrDefault(i => i.button == naviButton);
			if (item != null && item != current)
			{
				current.NavigateFrom();

				current = item;

				current.NavigateTo();
			}
		}

		[System.Serializable]
		public class NavigationItem
		{
			public NaviButton button;
			public Transform cameraPosition;
			public Page targetPage;

			public void NavigateTo()
			{
				if (button != null) button.IsSelected = true;
				if (cameraPosition != null) Camera.main.transform.localPosition = cameraPosition.position;
				if (targetPage != null) targetPage.Reveal();
			}

			public void NavigateFrom()
			{
				if (button != null) button.IsSelected = false;
				if (targetPage != null) targetPage.Hide();
			}
		}
	}
}