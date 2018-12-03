using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour, ISpawnManager {

    [Tooltip("The list of objects to spawn.")]
    [SerializeField]
    private List<SpawnObject> objectsToSpawn;


    //Singleton enforcement
    private static SpawnManager instance;
    public static SpawnManager GetInstance() {
        return instance;
    }

    public void Awake() {
        if (instance == null) {
            instance = this;

            //objectsToSpawn = new List<SpawnObject>(FindObjectsOfType<SpawnObject>());
	    objectsToSpawn = new List<SpawnObject>(gameObject.GetComponentsInChildren<SpawnObject>() );
            
        } else {
            Debug.Log("A singleton of type \"SpawnManager\" already exists, so this object was destroyed.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
