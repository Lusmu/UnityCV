using UnityEngine;
using System.Collections;

namespace TimoMoisio.CV
{
	public class ButtonPressHandler : MonoBehaviour
	{
		public Camera guiCamera;

		private GameObject hitObject;

		void Update ()
		{
			Ray ray = guiCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, guiCamera.cullingMask))
			{
				GameObject currentHitObject;
				if (hit.collider.attachedRigidbody != null)
				{
					currentHitObject = hit.collider.attachedRigidbody.gameObject;
				}
				else
				{
					currentHitObject = hit.collider.gameObject;
				}

				if (Input.GetMouseButtonDown(0))
				{
					hitObject = currentHitObject;
					Debug.Log("down " + hitObject.name);
				}
				else if (Input.GetMouseButtonUp(0))
				{
					if (hitObject != null && currentHitObject == hitObject)
					{
						Debug.Log("up " + hitObject.name);
						Button button = hitObject.GetComponent<Button>();

						if (button != null) button.Press();

						hitObject = null;
					}
				}
			}
		}
	}
}