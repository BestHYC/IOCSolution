using System;
using System.Collections.Generic;
using System.Text;

namespace IOC.Framework
{
    public interface IShallowCopy<T>
    {
        T ShallowCopy();
    }
    public interface IDeepCopy<T>
    {
        T DeepCopy();
    }
}
