using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAfterTime : MonoBehaviour
{
    public float lifeTime;
    ObjectPool pool;

    void Awake()
    {
        pool = GameObject.FindGameObjectWithTag("DustParticlePool").GetComponent<ObjectPool>();
    }

    void OnEnable()
    {
        StartCoroutine(DelayedPool());
    }

    IEnumerator DelayedPool()
    {
        yield return new WaitForSeconds(lifeTime);

        gameObject.SetActive(false);
        pool.PoolObject(gameObject);
    }
}
