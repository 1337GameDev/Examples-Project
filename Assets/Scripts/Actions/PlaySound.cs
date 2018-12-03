using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    [SerializeField]
    private string soundClipName;

    public void OnEnable() {
        SingletonManager m = SingletonManager.GetInstance();
        if (m != null) {
            m.GetAudioManager().PlayClip(soundClipName, 1.0f);
        }
        
    }
}
