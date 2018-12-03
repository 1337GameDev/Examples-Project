using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PooledObject))]
public class PooledObjectEditor : Editor {

	public SerializedProperty 
	objectPoolName,
	prefab,
	maximumLoaded,
	minimumLoaded;

	//gui elements
	private static GUIContent 
	addButton = new GUIContent("+", "add"),
	deleteButton = new GUIContent("-", "delete"),
	addToListButton = new GUIContent("Add", "add element");

	//gui element options
	private static GUILayoutOption listButtonWidth = GUILayout.Width(20f);

	public void OnEnable() {
		objectPoolName = serializedObject.FindProperty ("objectPoolName");
		prefab = serializedObject.FindProperty ("prefab");
		maximumLoaded = serializedObject.FindProperty ("maximumLoaded");
		minimumLoaded = serializedObject.FindProperty ("minimumLoaded");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update ();

		EditorGUILayout.PropertyField (objectPoolName);
		EditorGUILayout.PropertyField (prefab);
		EditorGUILayout.PropertyField (maximumLoaded);
		EditorGUILayout.PropertyField (minimumLoaded);

		serializedObject.ApplyModifiedProperties();
	}


}
