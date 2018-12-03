using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Represents an object that is to be spawned, given parameters.
/// </summary>
[System.Serializable]
public class SpawnObject : MonoBehaviour {
    //this tracks if this object has passed at LEAST an entire frame
    //this is important because the singleton managers are initialized during awake and on enable. 
	private bool firsFramePassed = false;

    public enum WHERE_TO_SPAWN {
        TARGET_TRANSFORM=1,
        TARGET_VECTOR3=2,
        RANDOM_AREA_IN_BOUNDS=3
    }



    [Tooltip("The name of the pool that the prefab is pooled in.")]
    [SerializeField]
    private string poolName = "Object Pool";
    [Tooltip("The name of the prefab that is to be spawned.")]
    [SerializeField]
    private string objectName = "Object";

    [Tooltip("Where the prefab should be spawned.")]
    [SerializeField]
    private WHERE_TO_SPAWN whereToSpawn = WHERE_TO_SPAWN.TARGET_VECTOR3;

    [Tooltip("The transform to use as the position to spawn an object at.")]
    [SerializeField]
    private Transform targetPositionTransform;
    [Tooltip("The transform to use as the rotation to spawn an object at.")]
    [SerializeField]
    private Transform targetRotationTransform;

    [Tooltip("The position to spawn the object at.")]
    [SerializeField]
    private Vector3 spawnPosition;
    [Tooltip("The rotation to spawn the object with.")]
    [SerializeField]
    private Quaternion spawnRotation;

    [Tooltip("Whether to repeatdely spawn the object every \"repeatSeconds\" number of seconds.")]
    [SerializeField]
    private bool repeatedlySpawn = false;
    [Tooltip("The time (in seconds) for the object to repeatedly spawn.")]
    [SerializeField]
    private float repeatSeconds = 0.0f;

    public enum RANDOM_BOUNDS {
        SPHERE = 1,
        BOX = 2
    }
    [Tooltip("The shape of the random bounds to spawn the object in.")]
    [SerializeField]
    private RANDOM_BOUNDS randomBounds = RANDOM_BOUNDS.SPHERE;

    [Tooltip("Whether to draw gizmos for the random spawn bounds, or not")]
    [SerializeField]
    private bool drawSpawnBounds = true;
    [Tooltip("The color of the bounds gizmo to draw.")]
    [SerializeField]
    private Color gizmoColor = Color.green;

    [Tooltip("The radius of the sphere bounds to randomly spawn the object in.")]
    [SerializeField]
    private float radius = 1.0f;

    [Tooltip("The length (x-axis) of the box to randomly spawn the object in.")]
    [SerializeField]
    private float length = 1.0f;
    [Tooltip("The width (z-axis) of the box to randomly spawn the object in.")]
    [SerializeField]
    private float width = 1.0f;
    [Tooltip("The height (y-axis) of the box to randomly spawn the object in.")]
    [SerializeField]
    private float height = 1.0f;

    private void OnDrawGizmos() {
        if(drawSpawnBounds) {
            if (whereToSpawn == WHERE_TO_SPAWN.RANDOM_AREA_IN_BOUNDS) {
                Gizmos.color = gizmoColor;

                switch(randomBounds) {
                    case RANDOM_BOUNDS.SPHERE:
                        Gizmos.DrawWireSphere(transform.position, radius);
                        break;
                    case RANDOM_BOUNDS.BOX:
                        float halfLength = length / 2.0f;//x-axis
                        float halfWidth = width / 2.0f;//z-axis
                        float halfHeight = height / 2.0f;//y-axis

                        //draw top rectangle, then lower one, then connect them
                        //top rectangle
                        Vector3 ul1 = new Vector3(halfLength, halfHeight, halfWidth) + transform.position;//upper-left
                        Vector3 ll1 = new Vector3(-halfLength, halfHeight, halfWidth) + transform.position;//lower-left
                        Vector3 ur1 = new Vector3(halfLength, halfHeight, -halfWidth) + transform.position;//upper-right
                        Vector3 lr1 = new Vector3(-halfLength, halfHeight, -halfWidth) + transform.position;//lower-right
                        Gizmos.DrawLine(ul1,ur1);
                        Gizmos.DrawLine(ll1,lr1);
                        Gizmos.DrawLine(ul1,ll1);
                        Gizmos.DrawLine(ur1,lr1);

                        //bottom rectangle
                        Vector3 ul2 = new Vector3(halfLength, -halfHeight, halfWidth) + transform.position;//upper-left
                        Vector3 ll2 = new Vector3(-halfLength, -halfHeight, halfWidth) + transform.position;//lower-left
                        Vector3 ur2 = new Vector3(halfLength, -halfHeight, -halfWidth) + transform.position;//upper-right
                        Vector3 lr2 = new Vector3(-halfLength, -halfHeight, -halfWidth) + transform.position;//lower-right
                        Gizmos.DrawLine(ul2, ur2);
                        Gizmos.DrawLine(ll2, lr2);
                        Gizmos.DrawLine(ul2, ll2);
                        Gizmos.DrawLine(ur2, lr2);

                        //connecting lines
                        Gizmos.DrawLine(ul1, ul2);
                        Gizmos.DrawLine(ll1, ll2);
                        Gizmos.DrawLine(ur1, ur2);
                        Gizmos.DrawLine(lr1, lr2);

                        break;
                }

            }
        }
    }

    public void OnEnable() {
	if (firsFramePassed) {
            BeginSpawning();
        }
    }
    public void Start() {
	if (!firsFramePassed && gameObject.activeSelf) {
    		BeginSpawning();
	}
	firsFramePassed = true;
    }
    public void OnDisable() {
        CancelInvoke();
    }
    public void OnDestroy() {
        CancelInvoke();
    }

    private void BeginSpawning() {
	if (repeatedlySpawn) {
            SpawnTheObject();
        } else {
            InvokeRepeating("SpawnTheObject", 0, repeatSeconds);
        }
    }

    public void SpawnTheObject() {
        Quaternion rot = transform.rotation;
        Vector3 pos = transform.position;
        float halfLength = length / 2.0f;//x-axis
        float halfWidth = width / 2.0f;//z-axis
        float halfHeight = height / 2.0f;//y-axis

        switch (whereToSpawn) {
            case WHERE_TO_SPAWN.TARGET_VECTOR3:
                rot = spawnRotation;
                pos = spawnPosition;
                break;
            case WHERE_TO_SPAWN.TARGET_TRANSFORM:
                rot = targetRotationTransform.rotation;
                pos = targetPositionTransform.position;
                break;
            case WHERE_TO_SPAWN.RANDOM_AREA_IN_BOUNDS:
                if(randomBounds == RANDOM_BOUNDS.SPHERE) {
                    pos = Random.insideUnitSphere * radius;
                } else {
                    float rndX = Random.Range(-halfLength, halfLength);
                    float rndY = Random.Range(-halfHeight, halfHeight);
                    float rndZ = Random.Range(-halfWidth, halfWidth);
                    pos = new Vector3(rndX, rndY, rndZ);
                }
                break;
        }

        //now we know pos and rot to spawn

        SingletonManager m = SingletonManager.GetInstance();
        if(m != null) {
            IPoolManager p = m.GetPoolManager();
            if(p != null) {
                ObjectPool pool = p.GetObjectPool(poolName);
                if(pool != null) {
                    PooledObject pooled = pool.GetPooledObject(objectName);
                    if(pooled != null) {
                        GameObject objSpawned = pooled.Spawn(pos,rot);
                        if(objSpawned == null) {
                            Debug.LogWarning("The pool \""+poolName+"\" had no more allocated \""+objectName+"\" objects able to be spawned.");
                        }
                    } else {
                        Debug.LogError("The SpawnObject tried to spawn the object \""+objectName+"\" from the pool \""+poolName+"\" but it didn't exist.");
                    }
                } else {
                    Debug.LogError("The SpawnObject tried to access the pool \""+poolName+"\" and it doesn't exist.");
                }
            }
        }
    }

    


}
