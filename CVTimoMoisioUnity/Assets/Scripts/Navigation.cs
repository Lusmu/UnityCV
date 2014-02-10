using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TimoMoisio.CV
{
	// TODO make navi items monobehaviours
	public class Navigation : MonoBehaviour 
	{
		public enum NaviId
		{
			None = 0,
			Main = 1,
			Samples = 2,
		}

		private NavigationItem current;

		public NavigationItem[] mainItems;
		public NavigationItem[] samplesItems;

		private NaviId currentNavi;
		private FeatheredRotation mainButtonsAnimation;
		private FeatheredRotation samplesButtonsAnimation;
		List<NavigationItem> all;

		void Start()
		{
			current = mainItems[0];

			current.NavigateTo();

			Transform[] transforms = mainItems.Select(t => t.button.transform).ToArray();
			mainButtonsAnimation = new FeatheredRotation(transforms);

			Transform[] transforms2 = samplesItems.Select(t => t.button.transform).ToArray();
			samplesButtonsAnimation = new FeatheredRotation(transforms2);

			currentNavi = NaviId.Main;

			HideButtons(NaviId.Main);
			HideButtons(NaviId.Samples);

			all = new List<NavigationItem>(mainItems);
			all.AddRange(samplesItems);

			StartCoroutine(ShowButtons(currentNavi, 1));
		}

		IEnumerator ShowButtons(NaviId navi, float delay)
		{
			yield return new WaitForSeconds(delay);
			GetNaviAnimation(navi).RotateTo(Vector3.zero, 100, 0.25f);
		}

		void HideButtons(NaviId navi)
		{
			GetNaviAnimation(navi).SetTo(Vector3.up * 90);
		}

		void SwitchNavi(NaviId newNavi)
		{
			if (newNavi != currentNavi)
			{
				HideButtons(currentNavi);

				currentNavi = newNavi;

				StartCoroutine(ShowButtons(currentNavi, 0.5f));
			}
		}

		FeatheredRotation GetNaviAnimation(NaviId navi)
		{
			switch (navi)
			{
			case NaviId.Samples:
				return samplesButtonsAnimation;
			case NaviId.Main:
				return mainButtonsAnimation;
			}

			return null;
		}

		void OnEnable()
		{
			for (int i = 0; i < mainItems.Length; i++)
			{
				mainItems[i].button.OnButtonPress += OnButtonPress;
			}
			for (int i = 0; i < samplesItems.Length; i++)
			{
				samplesItems[i].button.OnButtonPress += OnButtonPress;
			}
		}

		void OnDisable()
		{
			for (int i = 0; i < mainItems.Length; i++)
			{
				mainItems[i].button.OnButtonPress -= OnButtonPress;
			}
			for (int i = 0; i < samplesItems.Length; i++)
			{
				samplesItems[i].button.OnButtonPress -= OnButtonPress;
			}
		}

		void Update()
		{
			GetNaviAnimation(currentNavi).UpdatePositions(Time.deltaTime);
		}

		void OnButtonPress(Button button)
		{
			NaviButton naviButton = (NaviButton)button;
			NavigationItem item = all.FirstOrDefault(i => i.button == naviButton);

			if (item != null && item != current)
			{
				current.NavigateFrom();

				current = item;

				current.NavigateTo();

				if (current.changeToNavi != NaviId.None)
				{
					SwitchNavi(current.changeToNavi);
				}
			}
		}

		[System.Serializable]
		public class NavigationItem
		{
			public NaviId changeToNavi;
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