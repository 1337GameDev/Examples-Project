using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SingletonManager : MonoBehaviour {
    private IAudioManager audioManager;
    private IIOManager ioManager;
    private IInputManager inputManager;
    private IUIManager uiManager;
    private ISpawnManager spawnManager;
    private IPoolManager poolManager;

    //Use this to track if initialization has happened for all singletons (if Start() has been executed)
    private bool singletonsInitialized = false;
    public bool GetSingletonsInitialized() {return singletonsInitialized;}
    public delegate void SingletonsReady();
    public static event SingletonsReady OnSingletonsReady;

    //Singleton enforcement
    private static SingletonManager instance;
    public static SingletonManager GetInstance() {
        return instance;
    }

    private void Awake() {
        if(instance == null) {
            instance = this;

            List<MonoBehaviour> allScripts = new List<MonoBehaviour>(FindObjectsOfType<MonoBehaviour>());

            IAudioManager[] audioManagers = (from a in allScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IAudioManager)) select (IAudioManager)a).ToArray();
            IIOManager[] ioManagers = (from a in allScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IIOManager)) select (IIOManager)a).ToArray();
            IInputManager[] inputManagers = (from a in allScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IInputManager)) select (IInputManager)a).ToArray();
            IUIManager[] uiManagers = (from a in allScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IUIManager)) select (IUIManager)a).ToArray();
            ISpawnManager[] spawnManagers = (from a in allScripts where a.GetType().GetInterfaces().Any(k => k == typeof(ISpawnManager)) select (ISpawnManager)a).ToArray();
            IPoolManager[] poolManagers = (from a in allScripts where a.GetType().GetInterfaces().Any(k => k == typeof(IPoolManager)) select (IPoolManager)a).ToArray();

            //find each manager in the scene and register them
            //IAudioManager[] audioManagers = FindObjectsOfType(typeof(IAudioManager)) as IAudioManager[];
            //IIOManager[] ioManagers = FindObjectsOfType(typeof(IIOManager)) as IIOManager[];
            //IInputManager[] inputManagers = FindObjectsOfType(typeof(IInputManager)) as IInputManager[];
            //IUIManager[] uiManagers = FindObjectsOfType(typeof(IUIManager)) as IUIManager[];
            //ISpawnManager[] spawnManagers = FindObjectsOfType(typeof(ISpawnManager)) as ISpawnManager[];
            //IPoolManager[] poolManagers = FindObjectsOfType(typeof(IPoolManager)) as IPoolManager[];

            if(audioManagers.Length > 1) {
                Debug.LogError("There are multiple AudioManagers in the scene.");
            } else if(audioManagers.Length < 1) {
            } else {
                this.RegisterAudioManager(audioManagers[0]);
            }

            if (ioManagers.Length > 1) {
                Debug.LogError("There are multiple IOManagers in the scene.");
            } else if (ioManagers.Length < 1) {
            } else {
                this.RegisterIOManager(ioManagers[0]);
            }

            if (inputManagers.Length > 1) {
                Debug.LogError("There are multiple InputManagers in the scene.");
            } else if (inputManagers.Length < 1) {
            } else {
                this.RegisterInputManager(inputManagers[0]);
            }

            if (uiManagers.Length > 1) {
                Debug.LogError("There are multiple UIManagers in the scene.");
            } else if (uiManagers.Length < 1) {
            } else {
                this.RegisterUIManager(uiManagers[0]);
            }

            if (spawnManagers.Length > 1) {
                Debug.LogError("There are multiple SpawnManagers in the scene.");
            } else if (spawnManagers.Length < 1) {
            } else {
                this.RegisterSpawnManager(spawnManagers[0]);
            }

            if (poolManagers.Length > 1) {
                Debug.LogError("There are multiple PoolManagers in the scene.");
            } else if (poolManagers.Length < 1) {
            } else {
                this.RegisterPoolManager(poolManagers[0]);
            }

        } else {
            Debug.Log("A singleton of type \"SingletonManager\" already exists, so this object was destroyed.");
            Destroy(gameObject);
        }
    }

    public void Start() {
	if (OnSingletonsReady != null) {
		ExecuteReadyEvent ();
	}
    }

	public void ExecuteReadyEvent() {
		OnSingletonsReady();
		System.Delegate[] readyEvents = OnSingletonsReady.GetInvocationList();
		for (int i = 0; i < readyEvents.Length; i++)
		{
			//Remove all event
			SingletonsReady s = readyEvents[i] as SingletonsReady;
			s ();
			OnSingletonsReady -= s;
		}
	}

	public void ExecuteWhenInitialized(SingletonsReady f) {
		if (singletonsInitialized) {
			f();
		} else {
			OnSingletonsReady += f;
		}
	}

    /* Access Managers */
    public IAudioManager GetAudioManager() {
        return audioManager;
    }
    public IIOManager GetIOManager() {
        return ioManager;
    }
    public IInputManager GetInputManager() {
        return inputManager;
    }
    public IUIManager GetUIManager() {
        return uiManager;
    }
    public ISpawnManager GetSpawnManager() {
        return spawnManager;
    }
    public IPoolManager GetPoolManager() {
        return poolManager;
    }

    /* Register Manager */
    public bool RegisterAudioManager(IAudioManager a, bool replaceExisting = false) {
        if(this.audioManager != null && !replaceExisting) {
            return false;
        }

        this.audioManager = a;
        return true;
    }
    public bool RegisterIOManager(IIOManager i, bool replaceExisting = false) {
        if (this.ioManager != null && !replaceExisting) {
            return false;
        }

        this.ioManager = i;
        return true;
    }
    public bool RegisterInputManager(IInputManager i, bool replaceExisting = false) {
        if (this.inputManager != null && !replaceExisting) {
            return false;
        }

        this.inputManager = i;
        return true;
    }
    public bool RegisterUIManager(IUIManager u, bool replaceExisting = false) {
        if (this.uiManager != null && !replaceExisting) {
            return false;
        }

        this.uiManager = u;
        return true;
    }
    public bool RegisterSpawnManager(ISpawnManager s, bool replaceExisting = false) {
        if (this.spawnManager != null && !replaceExisting) {
            return false;
        }

        this.spawnManager = s;
        return true;
    }
    public bool RegisterPoolManager(IPoolManager p, bool replaceExisting = false) {
        if (this.poolManager != null && !replaceExisting) {
            return false;
        }

        this.poolManager = p;
        return true;
    }

}
