using UnityEngine;
using System.Collections;

namespace TimoMoisio.CV
{
	public class Button : MonoBehaviour 
	{
		public event System.Action<Button> OnButtonPress;

		public virtual void Press()
		{
			Debug.Log(gameObject.name + " pressed");
			if (OnButtonPress != null) OnButtonPress(this);
		}
	}
}