using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    public List<GameObject> pooledObjects;
    public GameObject[] objectToPool;

    private List<GameObject> objectsInPool = new List<GameObject>();
    void Awake()
    {
        SharedInstance = this;

        pooledObjects = new List<GameObject>();
        foreach (GameObject section in objectToPool)
        {
            GameObject obj = (GameObject)Instantiate(section);
            obj.SetActive(false);
            
            pooledObjects.Add(obj);
        }
    }

    private void Start()
    {
        
    }

    public GameObject GetPooledObject()
    {
        
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
           
        return null;
    }

    public GameObject GetPooledObjectByIndex(int index)
    {
        if (index <= pooledObjects.Count)
        {
            if (!pooledObjects[index].activeInHierarchy)
            {
                return pooledObjects[index];
            }
        }
        return null;
    }
    public GameObject GetRandomPooledObject()
    {

        int choose;
        objectsInPool.Clear();
        
        foreach(GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                objectsInPool.Add(obj);
            }
        }
        choose = Random.Range(0, objectsInPool.Count);

        return objectsInPool[choose];
    }

}
