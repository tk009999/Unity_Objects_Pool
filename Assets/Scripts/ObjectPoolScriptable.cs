using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ObjectPool
{
    [CreateAssetMenu(menuName = "My Assets/Object Pool Scriptable")]
    public class ObjectPoolScriptable : ScriptableObject, IPool<GameObject>
    {
        public IPoolInitialize PoolInitialize { get; set; }
        public IPoolGetting<GameObject> PoolGetting { get; set; }
        public IPoolReturn<GameObject> PoolReturn { get; set; }

        [SerializeField] GameObject prefab;

        Stack<GameObject> FreePool = null;

        Stack<GameObject> InUsePool = null;

        public void InitializePool(int size)
        {
            if (PoolGetting == null || PoolReturn == null) { Debug.LogError("Initialize failed with out implements PoolGeting or PoolReturn"); return; }
            if (FreePool == null) { FreePool = new Stack<GameObject>(size); };
            if (InUsePool == null) { InUsePool = new Stack<GameObject>(size); };

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
//            if (InUsePool.Count > 0)
//            {
//                var @object = InUsePool.Pop();
//                PoolReturn.OnReturned(@object);
//                FreePool.Push(@object);
//            }
        }
    }
}
