using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {
    [SerializeField]
    private string musicClipName;

    public void OnEnable() {
        SingletonManager m = SingletonManager.GetInstance();
        if (m != null) {
            m.GetAudioManager().PlayMusic(musicClipName);
        }
    }
}
