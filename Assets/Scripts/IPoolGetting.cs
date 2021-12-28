using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPool
{
    public interface IPoolGetting<T>
    {
        void OnGot(T gameObject);
    }
}
