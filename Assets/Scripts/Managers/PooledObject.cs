using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Represents a prefab to be pooled, as well as the actual pool of the prefabs. 
/// </summary>
[System.Serializable]
public class PooledObject {
    [Tooltip("The name of the prefab that is to be pooled. If blank, it'll use the prefab's object name.")]
    [SerializeField]
    private string objectPoolName = "Object Pool";
    public string GetPooledObjectName() { return objectPoolName; }
    [Tooltip("The prefab gameobject to pool.")]
    [SerializeField]
    private GameObject prefab;
    [Tooltip("The maximum count of the prefab to be pooled.")]
    [SerializeField]
    private int maximumLoaded = 10;
    [Tooltip("The minimum amount of the prefab to be pooled.")]
    [SerializeField]
    private int minimumLoaded = 5;

    /// <summary> 
    /// Tracks the current number of objects INITIALIZED by this pool. 
    /// <para> The "objectCache" field only stores AVAILABLE objects, and 
    /// doesn't include objects that could possinly be returned to the pool (and are still in use). </para>
    /// </summary>
    private int currentLoaded = 0;
    private ObjectPool owningPool;
    

    public LinkedList<GameObject> objectCache;

    public void InitPool(ObjectPool owner) {
        owningPool = owner;
        objectCache = new LinkedList<GameObject>();
        for (int i = 0; i < minimumLoaded; i++) {
            PoolObject();
        }
    }

    private void PoolObject() {
        GameObject g = MonoBehaviour.Instantiate(prefab, owningPool.transform.position, owningPool.transform.rotation, owningPool.transform);
        OriginPoolTracker o = g.AddComponent<OriginPoolTracker>();
        o.originPool = this;
        g.SetActive(false);
        g.transform.name = g.transform.name.Replace("(Clone)", "").Trim();
        objectCache.AddLast(g);
        currentLoaded++;
    }

    public GameObject Spawn(Vector3 pos, Quaternion rot, Transform parent = null) {
        if(objectCache.Count < 1) {
            if ((currentLoaded < maximumLoaded)) {
                PoolObject();
            } else {
                return null;
            }
        }
        GameObject g = objectCache.First.Value;
        objectCache.RemoveFirst();

        g.transform.position = pos;
        g.transform.rotation = rot;
        g.transform.SetParent(parent);
        g.SetActive(true);
        return g;
    }

    public void Despawn(GameObject obj) {
        if (objectCache.Count == maximumLoaded) {
            Debug.LogError("The object \"" + obj.name + "\" was instructed to return to the pool \"" + objectPoolName + "\" but the pool was full.");
        } else {
            objectCache.AddLast(obj);
            obj.SetActive(false);
            obj.transform.position = owningPool.transform.position;
            obj.transform.rotation = owningPool.transform.rotation;
            obj.transform.SetParent(owningPool.transform);
        }
    }

    public static void ReturnObject(GameObject obj) {
        OriginPoolTracker tracker = obj.GetComponent<OriginPoolTracker>();
        if(tracker != null) {
            tracker.originPool.Despawn(obj);
        } else {
            Debug.LogWarning("The object \""+obj.name+"\" did not have a pool tracker, but was instructed to be returned to a pool.");
        }
    }

    
}
