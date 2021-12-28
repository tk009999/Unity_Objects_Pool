using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectsPool : MonoBehaviour, IPool<GameObject>
    {
        public IPoolInitialize PoolInitialize { get; set; }
        public IPoolGetting<GameObject> PoolGetting { get; set; }
        public IPoolReturn<GameObject> PoolReturn { get; set; }

        [SerializeField] GameObject prefab;

        Stack<GameObject> FreePool = null;

        Stack<GameObject> InUsePool = null;

        private Dictionary<string, Stack<GameObject>> Pools = null; 

        public void InitializePool(int size)
        {
            if (PoolGetting == null || PoolReturn == null) { Debug.LogError("Initialize failed with out implements PoolGeting or PoolReturn"); return; }
            if (FreePool == null) { FreePool = new Stack<GameObject>(size); };
            if (InUsePool == null) { InUsePool = new Stack<GameObject>(size); };

            Pools = new Dictionary<string, Stack<GameObject>>();
            
            for (int i = 0; i < size; i++)
            {
                GameObject @object = Instantiate(prefab);
                @object.SetActive(false);
                FreePool.Push(@object);
            }
        }

        public void Get()
        {
            if (FreePool.Count > 0)
            {
                var @object = FreePool.Pop();
                PoolGetting.OnGot(@object);
                FreePool.Push(@object);
            }
        }

        public void Return()
        {
            if (InUsePool.Count > 0)
            {
                var @object = InUsePool.Pop();
                PoolReturn.OnReturned(@object);
                FreePool.Push(@object);
            }
        }
    }
}
