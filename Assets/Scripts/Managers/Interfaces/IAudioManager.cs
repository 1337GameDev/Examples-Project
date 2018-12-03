using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioManager {
    void PlayClip(string clipName, float volume);
    void PlayClip(string clipName, Vector3 pos, float volume);
    void StopAllClips();
    void PlayMusic(string musicClipName);
    void StopMusic();
    void PlayDialog(string dialogClipName);
    void StopDialog();
    AudioClip GetAudioClip(string clipName);
}
