using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoinObjectsPool : MonoBehaviour
{
    public static CoinObjectsPool Instance;
    [field: SerializeField] GameObject CoinPrefab;
    [field: SerializeField] int PoolSize { get; set; }
    [field: SerializeField] GameObject[] ObjectsPool;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        ObjectsPool = new GameObject[PoolSize];
        for(int i = 0; i < PoolSize; i++)
        {
            var clone = Instantiate(CoinPrefab, this.transform);
            clone.SetActive(false);
            ObjectsPool[i] = clone;
        }
    }
    
    public bool TryGetObjectPool(out GameObject gameObject)
    {
        gameObject = ObjectsPool.FirstOrDefault(_object => !_object.activeInHierarchy);

        if (gameObject != null)
            return true;
        return false;
    }
}
