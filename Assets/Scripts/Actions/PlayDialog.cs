using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDialog : MonoBehaviour {
    [SerializeField]
    private string dialogClipName;

	//this is to track if this object has passed the first frame
	//the managers are initialized and reistered the first frame
	private bool firsFramePassed = false;

    public void OnEnable() {
	if (firsFramePassed) {
		PlayClip();
	}
    }

	public void OnStart() {
		if (!firsFramePassed && gameObject.activeSelf) {
			PlayClip();
		}
		firsFramePassed = true;
	}

	private void PlayClip() {
		SingletonManager m = SingletonManager.GetInstance();
		if (m != null) {
			m.GetAudioManager().PlayDialog(dialogClipName);
		}
	}
}
