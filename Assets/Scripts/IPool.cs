using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool
{
    public interface IPool<T>
    {
        IPoolInitialize PoolInitialize { get; set; }
        IPoolGetting<T> PoolGetting { get; set; }
        IPoolReturn<T> PoolReturn { get; set; }
        void Get();
        void Return();
        void InitializePool(int size);
    }
}
