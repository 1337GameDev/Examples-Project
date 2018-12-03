using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class ConstantRotate : MonoBehaviour {
	private Transform trans;

	[Tooltip("Whether to draw gizmos for the rotation axis, or not")]
	[SerializeField]
	private bool drawGizmo = true;

	[Tooltip("Whether to rotate in the X-Axis.")]
	[SerializeField]
	private bool rotateInXaxis = false;
	[Tooltip("The rate (degrees per second) to rotate in the X-Axis.")]
	[SerializeField]
	private float xDegreeRotationPerSecond = 0.0f;

	[Tooltip("Whether to rotate in the Y-Axis.")]
	[SerializeField]
	private bool rotateInYaxis = false;
	[Tooltip("The rate (degrees per second) to rotate in the Y-Axis.")]
	[SerializeField]
	private float yDegreeRotationPerSecond = 0.0f;

	[Tooltip("Whether to rotate in the Z-Axis.")]
	[SerializeField]
	private bool rotateInZaxis = false;
	[Tooltip("The rate (degrees per second) to rotate in the Z-Axis.")]
	[SerializeField]
	private float zDegreeRotationPerSecond = 0.0f;

	// Use this for initialization
	void Start () {
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion orig = trans.rotation;

		//x-axis
		Quaternion xRot = Quaternion.AngleAxis(xDegreeRotationPerSecond * Time.deltaTime, transform.right);//right = Vector3(1,0,0)
		//y-axis
		Quaternion yRot = Quaternion.AngleAxis(yDegreeRotationPerSecond * Time.deltaTime, transform.up);//up = Vector3(0,1,0)
		//z-axis
		Quaternion zRot = Quaternion.AngleAxis(zDegreeRotationPerSecond * Time.deltaTime, transform.forward);//forward = Vector3(0,0,1)

		Quaternion result = orig;
		if (rotateInXaxis) {
			result = xRot * result;//must be in this order 
		}

		if (rotateInYaxis) {
			result = yRot * result;//must be in this order 
		}

		if (rotateInZaxis) {
			result = zRot * result;//must be in this order 
		}

		trans.rotation = result;
	}

	private void OnDrawGizmos() {
		if (drawGizmo) {
			//x-axis
			if (rotateInXaxis && xDegreeRotationPerSecond != 0.0f) {
				Handles.color = Color.red;
				Handles.DrawWireDisc (transform.position, transform.right, 1.0f);
			}

			//y-axis
			if (rotateInYaxis && yDegreeRotationPerSecond != 0.0f) {
				Handles.color = Color.green;
				Handles.DrawWireDisc (transform.position, transform.up, 1.0f);
			}

			//z-axis
			if (rotateInZaxis && zDegreeRotationPerSecond != 0.0f) {
				Handles.color = Color.blue;
				Handles.DrawWireDisc (transform.position, transform.forward, 1.0f);
			}
		}
	}
}
