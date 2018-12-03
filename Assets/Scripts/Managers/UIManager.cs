using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager {
    //Singleton enforcement
    private static UIManager instance;
    public static UIManager GetInstance() {
        return instance;
    }

    public void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.Log("A singleton of type \"UIManager\" already exists, so this object was destroyed.");
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
