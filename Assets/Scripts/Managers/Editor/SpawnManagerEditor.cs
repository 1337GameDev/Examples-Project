using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnManager))]
public class SpawnManagerEditor : Editor {

	public SerializedProperty 
	objectsToSpawn;

	//gui elements
	private static GUIContent 
	addButton = new GUIContent("+", "add"),
	deleteButton = new GUIContent("-", "delete"),
	addToListButton = new GUIContent("Add", "add element");

	//gui element options
	private static GUILayoutOption listButtonWidth = GUILayout.Width(20f);

	public void OnEnable() {
		objectsToSpawn = serializedObject.FindProperty ("objectsToSpawn");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update ();
		EditorGUILayout.HelpBox("This list references GameObjects with a SpawnObject component.", MessageType.Info);
		EditorGUILayout.LabelField("GameObject List", EditorStyles.boldLabel);
		RenderListElementsWithButtons (objectsToSpawn);

		serializedObject.ApplyModifiedProperties();
	}

	private static void AddListAddDeleteButtons (SerializedProperty list, int index) {
		if (!list.isArray) {
			EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
			return;
		}

		if (GUILayout.Button (addButton, EditorStyles.miniButtonLeft, listButtonWidth)) {
			list.InsertArrayElementAtIndex(index);
			list.DeleteArrayElementAtIndex(index+1);//remove the "default" value it has (copies the element currently at the index)
		}

		if (GUILayout.Button (deleteButton, EditorStyles.miniButtonLeft, listButtonWidth)) {
			//check if the element was removed (unity can sometimes be helpful and "clear" the element's values instead of deleting)
			int oldSize = list.arraySize;
			list.DeleteArrayElementAtIndex(index);
			if (list.arraySize == oldSize) {
				//if the element was cleared, delete it again to actually remove it
				list.DeleteArrayElementAtIndex(index);
			}
		}
	}

	private static void RenderListElementsWithButtons (SerializedProperty list) {
		if (!list.isArray) {
			EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
			return;
		}

		for (int i = 0; i < list.arraySize; i++) {
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), true);
			AddListAddDeleteButtons(list, i);
			EditorGUILayout.EndHorizontal();
		}

		if (list.arraySize == 0 && GUILayout.Button(addToListButton, EditorStyles.miniButton)) {
			list.arraySize++;
		}
	}
}
