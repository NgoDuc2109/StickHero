using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerMode1 : MonoBehaviour
{
    public static PoolManagerMode1 Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField]
    private ObjectPoolScript star;
    public GameObject RetrieveStarFromPool()
    {
        ObjectPoolScript tempPool;
        GameObject obj;
        tempPool = star;
        //new GameObject named obj and it calls GetPooledObejct on the tempPool. 
        obj = tempPool.GetPooledObject();
        obj.SetActive(true);
        //objectPoolScript named tempPool gets accesses the elements in the array at position R
        return obj;
    }

}
