using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(OnColliderEnabler))]
public class OnColliderEnablerEditor : Editor {

	public SerializedProperty 
	triggerOnEnter,
	triggerOnExit,
	triggerOnStay,
	toggleTarget,
	targetGameObject,
	requiredTriggerStayTime;

	public void OnEnable() {
		triggerOnEnter = serializedObject.FindProperty ("triggerOnEnter");
		triggerOnExit = serializedObject.FindProperty ("triggerOnExit");
		triggerOnStay = serializedObject.FindProperty ("triggerOnStay");
		toggleTarget = serializedObject.FindProperty ("toggleTarget");
		targetGameObject = serializedObject.FindProperty ("targetGameObject");
		requiredTriggerStayTime = serializedObject.FindProperty ("requiredTriggerStayTime");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update ();

		EditorGUILayout.PropertyField (triggerOnEnter);
		EditorGUILayout.PropertyField (triggerOnExit);
		EditorGUILayout.PropertyField (triggerOnStay);
		EditorGUILayout.PropertyField (toggleTarget);

		//get an int, instead of enum index to get the TRUE enum value (the index is just it's order in the enum, not it's value)
		OnColliderEnabler.ToggleTarget toggleTargetValue = (OnColliderEnabler.ToggleTarget)toggleTarget.intValue;
		using (EditorGUILayout.FadeGroupScope toggleTargetGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(toggleTargetValue == OnColliderEnabler.ToggleTarget.TargetObject)) ) {
			if (toggleTargetGroup.visible) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(targetGameObject);
				EditorGUI.indentLevel--;
			}
		}
		EditorGUILayout.PropertyField(requiredTriggerStayTime);

		serializedObject.ApplyModifiedProperties();
	}
}
