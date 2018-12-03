using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager {
    //Singleton enforcement
    private static InputManager instance;
    public static InputManager GetInstance() {
        return instance;
    }

    public void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.Log("A singleton of type \"InputManager\" already exists, so this object was destroyed.");
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
