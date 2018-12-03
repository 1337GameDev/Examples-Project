using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody) )]
[RequireComponent(typeof(Collider) )]
public class OnColliderEnabler : MonoBehaviour {
	public enum ToggleTarget {
		CollidingObject, 
		TargetObject
	};

	[Tooltip("If the object should be toggled when entering the collider.")]
	[SerializeField]
	private bool triggerOnEnter = true;
	[Tooltip("If the object should be toggled when exiting the collider.")]
	[SerializeField]
	private bool triggerOnExit = true;
	[Tooltip("If the object should be toggled when staying inside the collider.")]
	[SerializeField]
	private bool triggerOnStay = true;

	[Tooltip("What object should be toggled?")]
	[SerializeField]
	private ToggleTarget toggleTarget = ToggleTarget.CollidingObject;

	[Tooltip("The gameobject to toggle.")]
	[SerializeField]
	private GameObject targetGameObject;

	[Tooltip("The time an object needs to remain in the collider to be toggled.")]
	[SerializeField]
	private float requiredTriggerStayTime = 0.0f;

	private Collider attachedCollider;
	private Rigidbody rigidBody;
	private float colliderEnterTime = 0.0f;

	void Awake() {
		attachedCollider = transform.GetComponent<Collider> ();
		attachedCollider.isTrigger = true;

		rigidBody = transform.GetComponent<Rigidbody> ();
		rigidBody.isKinematic = true;
		rigidBody.useGravity = false;
	}

	void OnTriggerEnter(Collider collider) {
		if (triggerOnEnter) {
			ToggleTargetGameobject (collider);
		}

		colliderEnterTime = Time.time;
	}

	void OnTriggerExit(Collider collider){
		if (triggerOnExit) {
			ToggleTargetGameobject (collider);
		}
	}

	void OnTriggerStay(Collider collider){
		if (triggerOnStay) {
			if (requiredTriggerStayTime > 0) {
				if ((Time.time - colliderEnterTime) >= requiredTriggerStayTime) {
					ToggleTargetGameobject (collider);
				}
			} else {
				ToggleTargetGameobject (collider);
			}
		}
	}

	private void ToggleTargetGameobject(Collider collider) {
		if (toggleTarget == ToggleTarget.CollidingObject) {
			collider.gameObject.SetActive (!collider.gameObject.activeSelf);
		} else {
			targetGameObject.SetActive (!targetGameObject.activeSelf);
		}
	}

}
