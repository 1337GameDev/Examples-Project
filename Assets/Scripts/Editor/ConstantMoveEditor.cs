using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConstantMove))]
public class ConstantMoveEditor : Editor {

	public SerializedProperty 
	drawGizmo,
	gizmoColor,
	moveInXaxis,
	xMovementRate,
	moveInYaxis,
	yMovementRate,
	moveInZaxis,
	zMovementRate;

	public void OnEnable() {
		drawGizmo = serializedObject.FindProperty ("drawGizmo");
		gizmoColor = serializedObject.FindProperty ("gizmoColor");
		moveInXaxis = serializedObject.FindProperty ("moveInXaxis");
		xMovementRate = serializedObject.FindProperty ("xMovementRate");
		moveInYaxis = serializedObject.FindProperty ("moveInYaxis");
		yMovementRate = serializedObject.FindProperty ("yMovementRate");
		moveInZaxis = serializedObject.FindProperty ("moveInZaxis");
		zMovementRate = serializedObject.FindProperty ("zMovementRate");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update ();

		EditorGUILayout.PropertyField (drawGizmo);
		bool drawGizmoValue = drawGizmo.boolValue;
		EditorGUI.BeginDisabledGroup (!drawGizmoValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField (gizmoColor);
		EditorGUI.indentLevel--;
		EditorGUI.EndDisabledGroup ();


		EditorGUILayout.PropertyField (moveInXaxis);
		bool moveInXaxisValue = moveInXaxis.boolValue;
		EditorGUI.BeginDisabledGroup (!moveInXaxisValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(xMovementRate);
		EditorGUI.indentLevel--;
		EditorGUI.EndDisabledGroup ();

		EditorGUILayout.PropertyField (moveInYaxis);
		bool moveInYaxisValue = moveInYaxis.boolValue;
		EditorGUI.BeginDisabledGroup (!moveInYaxisValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(yMovementRate);
		EditorGUI.indentLevel--;
		EditorGUI.EndDisabledGroup ();

		EditorGUILayout.PropertyField (moveInZaxis);
		bool moveInZaxisValue = moveInZaxis.boolValue;
		EditorGUI.BeginDisabledGroup (!moveInZaxisValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(zMovementRate);
		EditorGUI.indentLevel--;
		EditorGUI.EndDisabledGroup ();

		serializedObject.ApplyModifiedProperties();
	}
}
