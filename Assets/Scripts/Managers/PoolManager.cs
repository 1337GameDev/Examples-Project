using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolManager : MonoBehaviour, IPoolManager {
    private static Dictionary<string, ObjectPool> objectPools;

    //Singleton enforcement
    private static PoolManager instance;
    public static PoolManager GetInstance() {
        return instance;
    }

    public void Awake() {
        if (instance == null) {
            instance = this;
            objectPools = new Dictionary<string, ObjectPool>();
            //fetch all pools
            List<ObjectPool> allObjectPools = new List<ObjectPool>(FindObjectsOfType<ObjectPool>() );
            foreach (ObjectPool o in allObjectPools) {
                RegisterObjectPool(o);
            }
        } else {
            Debug.Log("A singleton of type \"PoolManager\" already exists, so this object was destroyed.");
            Destroy(gameObject);
        }
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public void RegisterObjectPool(ObjectPool o) {
        objectPools.Add(o.GetPoolName(),o);
    }

    public ObjectPool GetObjectPool(string poolName) {
        return objectPools[poolName];
    }
}
