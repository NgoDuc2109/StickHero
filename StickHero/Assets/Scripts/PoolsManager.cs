using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public static PoolsManager Instance;
    [SerializeField]
    private ObjectPoolScript towerMode2;
    [SerializeField]
    private ObjectPoolScript melon;
    [SerializeField]
    private ObjectPoolScript melonFall;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject RetrieveTowerMode2FromPool()
    {
        ObjectPoolScript tempPool;
        GameObject obj;
        tempPool = towerMode2;
        //new GameObject named obj and it calls GetPooledObejct on the tempPool. 
        obj = tempPool.GetPooledObject();
        obj.SetActive(true);
        //objectPoolScript named tempPool gets accesses the elements in the array at position R
        return obj;
    }


    public GameObject RetrieveMelonFromPool()
    {
        ObjectPoolScript tempPool;
        GameObject obj;
        tempPool = melon;
        //new GameObject named obj and it calls GetPooledObejct on the tempPool. 
        obj = tempPool.GetPooledObject();
        obj.SetActive(true);
        //objectPoolScript named tempPool gets accesses the elements in the array at position R
        return obj;
    }

    public GameObject RetrieveMelonFallFromPool()
    {
        ObjectPoolScript tempPool;
        GameObject obj;
        tempPool = melonFall;
        //new GameObject named obj and it calls GetPooledObejct on the tempPool. 
        obj = tempPool.GetPooledObject();
        obj.SetActive(true);
        //objectPoolScript named tempPool gets accesses the elements in the array at position R
        return obj;
    }
}
