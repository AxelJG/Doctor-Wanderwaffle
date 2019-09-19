using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PooledObject
{
    public GameObject objectInstance;
    public float amount;
}

public class ObjectPool : MonoBehaviour
{
    #region VARIABLES

    public List<PooledObject> objectsToPool;
    List<GameObject> _pooledObjects;

    #endregion

    void Awake()
    {
        _pooledObjects = new List<GameObject>();

        foreach (PooledObject pooledObject in objectsToPool)
        {
            for (int i = 0; i < pooledObject.amount; i++)
            {
                GameObject instance = Instantiate(pooledObject.objectInstance, transform);
                PoolObject(instance);
            }
        }
    }

    #region POOL MANAGEMENT

    /// <summary>
    /// Get the first inactive object in the pool
    /// </summary>
    /// <returns>Inactive GameObject</returns>
    public GameObject GetPooledObjectByTag(string tag)
    {
        GameObject objectToReturn = null;

        // Loop through the object pool and return the first inactive object
        foreach (GameObject o in _pooledObjects)
        {
            // Skip iteration if the object is active
            if (o.activeInHierarchy)
            {
                continue;
            }

            if (o.tag.Equals(tag))
            {
                objectToReturn = o;
            }
        }

        if (objectToReturn != null)
        {
            _pooledObjects.Remove(objectToReturn);
        }

        // If every pooled object is active, returns null
        return objectToReturn;
    }

    /// <summary>
    /// Returns as many objects as it can find in the pool (Limited by maxAmount)
    /// </summary>
    /// <param name="tag">Tag of the objects in the pool</param>
    /// <param name="maxAmount">Limit of objects to find and return</param>
    /// <returns></returns>
    public List<GameObject> GetPooledObjectsByTag(string tag, int maxAmount)
    {
        List<GameObject> returnableObjects = new List<GameObject>();

        int objectCounter = 0;

        // Loop through the object pool and add inactive objects to the returnable list
        foreach (GameObject o in _pooledObjects)
        {
            // Early exit if maximum number of objects to return is reached
            if ((objectCounter - 1) >= maxAmount)
            {
                break;
            }

            // Skip iteration if the object is active
            if (o.activeInHierarchy)
            {
                continue;
            }

            if (o.tag.Equals(tag))
            {
                returnableObjects.Add(o);

                objectCounter++;
            }
        }

        foreach (GameObject toRemove in returnableObjects)
        {
            _pooledObjects.Remove(toRemove);
        }

        // If every pooled object is active, return null
        return returnableObjects;
    }

    /// <summary>
    /// Adds an object to the Pool
    /// </summary>
    /// <param name="objectTopool">Object to pool</param>
    public void PoolObject(GameObject objectToPool)
    {
        objectToPool.SetActive(false);
        _pooledObjects.Add(objectToPool);
    }

    #endregion
}
