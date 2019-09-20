using System;
using System.Collections.Generic;
using System.Text;

namespace IOC.Framework
{
    public interface IServiceProvider
    {
        T GetRequiredService<T>();
        Object GetRequiredService(Type type);
    }
    /// <summary>
    /// 
    /// </summary>
    public class ServiceProvider : IServiceProvider
    {
        private IDictionary<Type, IServiceCache> _cache;
        public ServiceProvider(IDictionary<Type, IServiceCache> valuePairs)
        {
            _cache = valuePairs;
        }
        public T GetRequiredService<T>()
        {
            Type t = typeof(T);
            return (T)GetRequiredService(t);
        }
        public object GetRequiredService(Type type)
        {
            IServiceCache service = null;
            if (!_cache.TryGetValue(type, out service))
            {
                throw new Exception("获取参数对象没有注入");
            }
            return service.GetCache(_cache);
        }
    }
}
