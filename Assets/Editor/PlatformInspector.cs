using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Platform))]
public class PlatformInspector : Editor {

	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector();
		Platform myTarget = target as Platform;

		if (GUILayout.Button("Generate") || GUI.changed)
		{
			myTarget.ClearPlatform();
			myTarget.CreatePlatform();
		}

		if (GUILayout.Button("Clear"))
		{
			myTarget.ClearPlatform();
		}
    }

	/*public void Awake()
	{
		Platform myTarget = target as Platform;

		myTarget.ClearPlatform();
	}*/
}
