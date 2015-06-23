using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileScript))]
public class TileScriptInspector : Editor {

	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector();
		TileScript myTarget = target as TileScript;

		if (GUILayout.Button("Generate") || GUI.changed)
		{
			myTarget.ClearPlatform();
			myTarget.DrawPlatform();
		}

		if (GUILayout.Button("Clear"))
		{
			myTarget.ClearPlatform();
		}
	}

	public void Awake()
	{
		TileScript myTarget = target as TileScript;

		myTarget.ClearPlatform();
	}
}
