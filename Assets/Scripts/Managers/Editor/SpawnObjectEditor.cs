using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnObject))]
public class SpawnObjectEditor : Editor {
    public SerializedProperty poolName,
        objectName,
        whereToSpawn,
        targetPositionTransform,
        targetRotationTransform,
        spawnPosition,
        spawnRotation,
	repeatedlySpawn,
        repeatSeconds,
        randomBounds,
        drawSpawnBounds,
        gizmoColor,
        radius,
        length,
        width,
        height;

    public void OnEnable() {
        poolName = serializedObject.FindProperty("poolName");
        objectName = serializedObject.FindProperty("objectName");
        whereToSpawn = serializedObject.FindProperty("whereToSpawn");
        targetPositionTransform = serializedObject.FindProperty("targetPositionTransform");
        targetRotationTransform = serializedObject.FindProperty("targetRotationTransform");
        spawnPosition = serializedObject.FindProperty("spawnPosition");
        spawnRotation = serializedObject.FindProperty("spawnRotation");
	repeatedlySpawn = serializedObject.FindProperty("repeatedlySpawn");
        repeatSeconds = serializedObject.FindProperty("repeatSeconds");
        randomBounds = serializedObject.FindProperty("randomBounds");
        drawSpawnBounds = serializedObject.FindProperty("drawSpawnBounds");
        gizmoColor = serializedObject.FindProperty("gizmoColor");
        radius = serializedObject.FindProperty("radius");
        length = serializedObject.FindProperty("length");
        width = serializedObject.FindProperty("width");
        height = serializedObject.FindProperty("height");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(poolName);
        EditorGUILayout.PropertyField(objectName);
	EditorGUILayout.PropertyField(repeatedlySpawn);

	bool repeatedlySpawnValue = repeatedlySpawn.boolValue;
	EditorGUI.BeginDisabledGroup (!repeatedlySpawnValue);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(repeatSeconds);
		EditorGUI.indentLevel--;
	EditorGUI.EndDisabledGroup ();

        EditorGUILayout.PropertyField(whereToSpawn);

        //get an int, instead of enum index to get the TRUE enum value (the index is just it's order in the enum, not it's value)
        SpawnObject.WHERE_TO_SPAWN whereToSpawnValue = (SpawnObject.WHERE_TO_SPAWN)whereToSpawn.intValue;
        using (EditorGUILayout.FadeGroupScope vectorGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(whereToSpawnValue == SpawnObject.WHERE_TO_SPAWN.TARGET_VECTOR3)) ) {
            if (vectorGroup.visible) {
		EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(spawnPosition);
                EditorGUILayout.PropertyField(spawnRotation);
		EditorGUI.indentLevel--;
            }
        }

        using (EditorGUILayout.FadeGroupScope transformGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(whereToSpawnValue == SpawnObject.WHERE_TO_SPAWN.TARGET_TRANSFORM))) {
            if (transformGroup.visible) {
		EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(targetPositionTransform);
                EditorGUILayout.PropertyField(targetRotationTransform);
		EditorGUI.indentLevel--;
            }
        }

        using (EditorGUILayout.FadeGroupScope boundsGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(whereToSpawnValue == SpawnObject.WHERE_TO_SPAWN.RANDOM_AREA_IN_BOUNDS))) {
            if (boundsGroup.visible) {
                EditorGUILayout.PropertyField(randomBounds);
                EditorGUILayout.PropertyField(drawSpawnBounds);
                EditorGUILayout.PropertyField(gizmoColor);

                //get an int, instead of enum index to get the TRUE enum value (the index is just it's order in the enum, not it's value)
                SpawnObject.RANDOM_BOUNDS randomBoundsValue = (SpawnObject.RANDOM_BOUNDS)randomBounds.intValue;
                using (EditorGUILayout.FadeGroupScope sphereBoundsGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(randomBoundsValue == SpawnObject.RANDOM_BOUNDS.SPHERE))) {
                    if (sphereBoundsGroup.visible) {
			EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(radius);
			EditorGUI.indentLevel--;
                    }
                }

                using (EditorGUILayout.FadeGroupScope boxBoundsGroup = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(randomBoundsValue == SpawnObject.RANDOM_BOUNDS.BOX))) {
                    if (boxBoundsGroup.visible) {
			EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(length);
                        EditorGUILayout.PropertyField(width);
                        EditorGUILayout.PropertyField(height);
			EditorGUI.indentLevel--;
                    }
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
