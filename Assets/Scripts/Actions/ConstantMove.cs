using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConstantMove : MonoBehaviour {
	private Transform trans;

	[Tooltip("Whether to draw gizmos for the movement vector, or not")]
	[SerializeField]
	private bool drawGizmo = true;
	[Tooltip("The color of the direction gizmo to draw.")]
	[SerializeField]
	private Color gizmoColor = Color.green;

	[Tooltip("Whether to move in the X-Axis.")]
	[SerializeField]
	private bool moveInXaxis = false;
	[Tooltip("The rate (per second) to move in the X-Axis direction.")]
	[SerializeField]
	private float xMovementRate = 0.0f;

	[Tooltip("Whether to move in the Y-Axis.")]
	[SerializeField]
	private bool moveInYaxis = false;
	[Tooltip("The rate (per second) to move in the Y-Axis direction.")]
	[SerializeField]
	private float yMovementRate = 0.0f;

	[Tooltip("Whether to move in the Z-Axis.")]
	[SerializeField]
	private bool moveInZaxis = false;
	[Tooltip("The rate (per second) to move in the Z-Axis direction.")]
	[SerializeField]
	private float zMovementRate = 0.0f;

	// Use this for initialization
	void Start () {
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = new Vector3 (Convert.ToInt32(moveInXaxis), Convert.ToInt32(moveInYaxis), Convert.ToInt32(moveInZaxis) );//which directions (normalized) to move
		Vector3 rate = new Vector3 (xMovementRate, yMovementRate, zMovementRate);//how fast to move in the direction

		trans.Translate(Vector3.Scale(dir, rate) * Time.deltaTime);
	}

	private void OnDrawGizmos() {
		if (drawGizmo) {
			Vector3 dir = new Vector3 (Convert.ToInt32 (moveInXaxis), Convert.ToInt32 (moveInYaxis), Convert.ToInt32 (moveInZaxis));//which directions (normalized) to move
			Vector3 rates = new Vector3 (xMovementRate, yMovementRate, zMovementRate);
			Vector3 resultPos = transform.position + Vector3.Scale (dir, rates);

			//now show this as a gizmo
			Debug.DrawLine (transform.position, resultPos, gizmoColor);
		}
	}
}
