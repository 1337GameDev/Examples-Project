using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor {

	public SerializedProperty 
	numberOfAudioSources,
	fadeInMusicTime,
	fadeOutMusicTime,
	dialogVolume,
	audioClips;

	//gui elements
	private static GUIContent 
	addButton = new GUIContent("+", "add"),
	deleteButton = new GUIContent("-", "delete"),
	addToListButton = new GUIContent("Add", "add element");

	//gui element options
	private static GUILayoutOption listButtonWidth = GUILayout.Width(20f);

	public void OnEnable() {
		numberOfAudioSources = serializedObject.FindProperty ("numberOfAudioSources");
		fadeInMusicTime = serializedObject.FindProperty ("fadeInMusicTime");
		fadeOutMusicTime = serializedObject.FindProperty ("fadeOutMusicTime");
		dialogVolume = serializedObject.FindProperty ("dialogVolume");
		audioClips = serializedObject.FindProperty ("audioClips");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update ();
		EditorGUILayout.PropertyField (numberOfAudioSources);
		EditorGUILayout.PropertyField (fadeInMusicTime);
		EditorGUILayout.PropertyField (fadeOutMusicTime);
		EditorGUILayout.PropertyField (dialogVolume);
		RenderListElementsWithButtons (audioClips);

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
