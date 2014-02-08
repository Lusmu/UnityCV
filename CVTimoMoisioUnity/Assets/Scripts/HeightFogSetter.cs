using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HeightFogSetter : MonoBehaviour
{
	public static HeightFogSetter Instance { get; private set; }

	public float fogStartHeight = 1.5f;
	public float fogEndHeight = 1.88f;

	void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != this) Destroy(this);
	}

	void Update ()
	{
		Shader.SetGlobalFloat("_YMin", fogStartHeight);
		Shader.SetGlobalFloat("_YMax", fogEndHeight);
	}
}
