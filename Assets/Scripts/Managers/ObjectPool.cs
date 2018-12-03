using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to hold a collection of object pools (that pool a single prefab). 
/// This class will be given a name (the member "poolName"), that should reflect what object pools are in this collection.
/// This class is convenient to organize and group pooled objects by their type 
/// </summary>
/// <seealso cref="ObjectPool.poolName"/> 
/// <seealso cref="ObjectPools"/> 
/// <remarks>
/// EG: This pool could be named "Projectiles" and have ObjectPools for "AK47_Bullet" and "RPG_Missile"
/// </remarks>
public class ObjectPool : MonoBehaviour {
    [SerializeField]
    private string poolName = "Pool";
    public string GetPoolName() { return poolName; }

    [SerializeField]
    private List<PooledObject> pools;

    private Dictionary<string, PooledObject> pooledObjects;

    public void Awake() {
        pooledObjects = new Dictionary<string, PooledObject>();
        foreach(PooledObject i in pools) {
            string name = i.GetPooledObjectName();

            if (pooledObjects.ContainsKey(name)) {
                Debug.LogError("A pool has already been initialized with the name \"" + name + "\"");
            } else {
                pooledObjects.Add(name, i);
            }
        }

        //now initialize all pools
        foreach (KeyValuePair<string, PooledObject> entry in pooledObjects) {
            entry.Value.InitPool(this);
        }
    }

    public PooledObject GetPooledObject(string pooledObjectName) {
        return pooledObjects[pooledObjectName];
    }
}
