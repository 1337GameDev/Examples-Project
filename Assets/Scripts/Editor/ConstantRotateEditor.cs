using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConstantRotate))]
public class ConstantRotateEditor : Editor {

	public SerializedProperty 
	drawGizmo,
	rotateInXaxis,
	xDegreeRotationPerSecond,
	rotateInYaxis,
	yDegreeRotationPerSecond,
	rotateInZaxis,
	zDegreeRotationPerSecond;

	public void OnEnable() {
		drawGizmo = serializedObject.FindProperty ("drawGizmo");
		rotateInXaxis = serializedObject.FindProperty ("rotateInXaxis");
		xDegreeRotationPerSecond = serializedObject.FindProperty ("xDegreeRotationPerSecond");
		rotateInYaxis = serializedObject.FindProperty ("rotateInYaxis");
		yDegreeRotationPerSecond = serializedObject.FindProperty ("yDegreeRotationPerSecond");
		rotateInZaxis = serializedObject.FindProperty ("rotateInZaxis");
		zDegreeRotationPerSecond = serializedObject.FindProperty ("zDegreeRotationPerSecond");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update ();

		EditorGUILayout.PropertyField (drawGizmo);
		bool drawGizmoValue = drawGizmo.boolValue;


		EditorGUILayout.PropertyField (rotateInXaxis);
		bool rotateInXaxisValue = rotateInXaxis.boolValue;
		EditorGUI.BeginDisabledGroup (!rotateInXaxisValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(xDegreeRotationPerSecond);
		EditorGUI.indentLevel--;
		EditorGUI.EndDisabledGroup ();

		EditorGUILayout.PropertyField (rotateInYaxis);
		bool rotateInYaxisValue = rotateInYaxis.boolValue;
		EditorGUI.BeginDisabledGroup (!rotateInYaxisValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(yDegreeRotationPerSecond);
		EditorGUI.indentLevel--;
		EditorGUI.EndDisabledGroup ();

		EditorGUILayout.PropertyField (rotateInZaxis);
		bool rotateInZaxisValue = rotateInZaxis.boolValue;
		EditorGUI.BeginDisabledGroup (!rotateInZaxisValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(zDegreeRotationPerSecond);
		EditorGUI.indentLevel--;
		EditorGUI.EndDisabledGroup ();

		serializedObject.ApplyModifiedProperties();
	}
}
