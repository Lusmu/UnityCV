using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HeightFogSetter : MonoBehaviour
{
	public float fogStartHeight = 1.5f;
	public float fogEndHeight = 1.88f;

	void Update ()
	{
		Shader.SetGlobalFloat("_YMin", fogStartHeight);
		Shader.SetGlobalFloat("_YMax", fogEndHeight);
	}
}
