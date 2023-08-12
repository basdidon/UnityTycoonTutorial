using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnCoin : MonoBehaviour
{
    [field: SerializeField] float MaxNextSpawnTime { get; set; }
    [field: SerializeField] float MinNextSpawnTime { get; set; }
    [field: SerializeField] float NextSpawnTime { get; set; }
    float timeElapsed = 0f;

    private void Start()
    {
        NextSpawnTime = Random.Range(MinNextSpawnTime, MaxNextSpawnTime);
    }

    private void Update()
    {
        if(timeElapsed >= NextSpawnTime)
        {
            timeElapsed = 0f;
            if(CoinObjectsPool.Instance.TryGetObjectPool(out GameObject gameObject))
            {
                var randomScreenPos = new Vector3(Random.Range(0, Screen.width),Random.Range(0,Screen.height));
                gameObject.SetActive(true);
                var spawnAt = Camera.main.ScreenToWorldPoint(randomScreenPos);
                spawnAt.z = 0f;
                gameObject.transform.position = spawnAt;
            }
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }

}
