using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolManager {

    GameObject GetGameObject();
    void RegisterObjectPool(ObjectPool o);
    ObjectPool GetObjectPool(string poolName);
}
