using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour, IAudioManager {
    private static AudioListener listener;

    [SerializeField]
    [Range(1, 10)]
    private int numberOfAudioSources = 1;
    private AudioSource[] audioSources;

    [SerializeField]
    [Range(0,100)]
    private float fadeInMusicTime = 1.0f;
    [SerializeField]
    [Range(0, 100)]
    private float fadeOutMusicTime = 1.0f;
    private AudioSource musicSource;
    private bool fadingOutMusic = false;
    private bool fadingInMusic = false;

    [SerializeField]
    [Range(0, 100)]
    private float dialogVolume = 100.0f;
    private AudioSource dialogSource;

    [SerializeField]
    private List<AudioClip> audioClips;
    //Speed up runtime lookups, and cache the array as a Dictionary
    private Dictionary<string, AudioClip> audioClipDictionary;

    //Singleton enforcement
    private static AudioManager instance;
    public static AudioManager GetInstance() {
        return instance;
    }

    public void Awake() {
        if (instance == null) {
            instance = this;
            listener = gameObject.GetComponent<AudioListener>();
            //set up secondary audio sources
            musicSource = AddNewAudioSource("MusicAudioSource");
            dialogSource = AddNewAudioSource("Dialog1AudioSource");

	    UpdateDictionary ();
        } else {
            Debug.Log("A singleton of type \"AudioManager\" already exists, so this object was destroyed.");
            Destroy(gameObject);
        }
    }

	public void UpdateDictionary() {
		if (audioClipDictionary == null) {
			audioClipDictionary = new Dictionary<string, AudioClip> ();
		}
		audioClipDictionary.Clear ();

		foreach (AudioClip c in audioClips) {
			audioClipDictionary.Add(c.name,c);
		}
	}

    public void PlayClip(string clipName, float volume = 1.0f) {
        PlayClip(clipName, transform.position, volume);
    }
    public void PlayClip(string clipName, Vector3 pos, float volume = 1.0f) {
        AudioClip clip = GetAudioClip(clipName);
        if(clip != null) {
            foreach(AudioSource s in audioSources) {
                if(!s.isPlaying) {
                    s.clip = clip;
                    s.volume = volume;
                    s.transform.position = pos;
                    s.Play();
                }
            }//do we care if no available source is found and this sound is skipped? Or do we use AudioSource.PlayOneShot()?
        }
    }
    public void StopAllClips() {
        //if audio clip is playing
        foreach (AudioSource s in audioSources) {
            if (s.isPlaying) {
                s.Stop();
            }
        }
    }

    public void PlayMusic(string musicClipName) {
        StartCoroutine(PlayMusicCoRoutine(musicClipName));
    }

    private IEnumerator PlayMusicCoRoutine(string musicClipName) {
        AudioClip music = GetAudioClip(musicClipName);
        if(music != null) {
            //if music is playing (and we arent already fading it out)
            //use coroutine to fade it out
            if(musicSource.isPlaying && fadingOutMusic) {
                fadingOutMusic = true;
                //this will execute over multiple frames, and not continue execution until the coroutine being called has finished
                yield return StartCoroutine(FadeAudioOut(musicSource, fadeOutMusicTime));
                fadingOutMusic = false;
            }

            musicSource.loop = true;
            musicSource.clip = music;
            //use coroutine to fade in music (if we aren't already fading in)
            if (!fadingOutMusic) {
                fadingOutMusic = true;
                yield return StartCoroutine(FadeAudioIn(musicSource, fadeInMusicTime));
                fadingOutMusic = false;
            }
        }
    }

    public void StopMusic() {
        //if music is playing
        //use coroutine to fade it out
        if (musicSource.isPlaying) {
            StartCoroutine(FadeAudioOut(musicSource, fadeOutMusicTime));
        }
    }

    private static IEnumerator FadeAudioOut(AudioSource source, float fadeOutTime) {
        float startVolume = source.volume;

        while (source.volume > 0) {
            source.volume = Mathf.Max(0, source.volume - (startVolume * Time.deltaTime / fadeOutTime));
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }

    private static IEnumerator FadeAudioIn(AudioSource source, float fadeInTime) {
        //float startVolume = source.volume;
        source.volume = 0;
        source.Play();
        while (source.volume < 1) {
            source.volume = Mathf.Min(1, source.volume + (Time.deltaTime / fadeInTime));
            yield return null;
        } 
    }

    public void PlayDialog(string dialogClipName) {
        AudioClip dialog = GetAudioClip(dialogClipName);
        if (dialog != null) {
            dialogSource.mute = false;
            dialogSource.PlayOneShot(dialog, dialogVolume/100.0f);
        }
    }
    public void StopDialog() {
        //if dialog is playing
        //use coroutine to fade it out
        if (dialogSource.isPlaying) {
            dialogSource.mute = true;
        }
    }

    private AudioSource AddNewAudioSource(string label) {
        GameObject srcObj = new GameObject(label, typeof(AudioSource));
        srcObj.transform.position = transform.position;
        srcObj.transform.parent = transform;
        return srcObj.GetComponent<AudioSource>();
    }

    public AudioClip GetAudioClip(string clipName) {
        AudioClip clip = audioClipDictionary[clipName];
        if(clip == null) {
            Debug.Log("The dialog clip \"" + clipName + "\" was not found in the list of available audio clips.");
        }

        return clip;
    }
}
