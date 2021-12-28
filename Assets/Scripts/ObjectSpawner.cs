using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ObjectPool;

public class ObjectSpawner : MonoBehaviour, IPoolGetting<GameObject>, IPoolReturn<GameObject>
{
    public GameObject Pool;

    IPool<GameObject> _Pool
    {
        get
        {
            return Pool.GetComponent<IPool<GameObject>>();
        }
    }

    private void Awake()
    {
        _Pool.PoolGetting = this;
        _Pool.PoolReturn = this;
        _Pool.InitializePool(5);
    }

    public void Get()
    {
        _Pool.Get();
    }

    public void Return()
    {
        _Pool.Return();
    }

    void IPoolGetting<GameObject>.OnGot(GameObject gameObject)
    {
        StartCoroutine(YieldOnGet(gameObject));
    }

    IEnumerator YieldOnGet(GameObject gameObject)
    {
        //取得物件時，可在此設定 gameObject 的 property 例如:        
        gameObject.transform.position = Random.insideUnitSphere;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

    void IPoolReturn<GameObject>.OnReturned(GameObject gameObject)
    {
        StartCoroutine(YieldOnReturn(gameObject));
    }

    IEnumerator YieldOnReturn(GameObject gameObject)
    {
        //返還物件時，可在此重置 gameObject 的屬性，或不做任何事情               
        gameObject.SetActive(false);
        yield return null;
    }
}
