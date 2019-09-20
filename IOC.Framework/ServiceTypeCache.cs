using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace IOC.Framework
{
    public enum ServiceLifetime
    {
        Singleton = 0,
        Transient = 1,
        Scoped = 2
    }
    public interface IServiceCache
    {
        Object GetCache(IDictionary<Type, IServiceCache> typePairs);
    }
    /// <summary>
    /// 此处只当做流程执行下去,具体请看AutoFac源码,此处只是对此源码做抽象改编
    /// 
    /// </summary>
    public class ServiceTypeCache : IServiceCache
    {
        /// <summary>
        /// 保存当前类型
        /// </summary>
        private Type _type;
        /// <summary>
        /// 保存当前类型种类
        /// </summary>
        private ServiceLifetime _typeEnum;
        /// <summary>
        /// 缓存当前对象
        /// </summary>
        private Object _obj;
        public ServiceTypeCache(Type type, ServiceLifetime typeEnum)
        {
            _type = type;
            _typeEnum = typeEnum;
        }
        public ServiceTypeCache(Object t, ServiceLifetime typeEnum)
        {
            _obj = t;
            _typeEnum = typeEnum;
        }
        /// <summary>
        /// 针对构造参数最多的构造器进行创建对象
        /// 如果有相同数量的构造器,选择最后一个构造器
        /// </summary>
        /// <returns></returns>
        public Object GetCache(IDictionary<Type, IServiceCache> typePairs)
        {
            if (_obj == null)
            {
                List<Type> types = GetConstructor();
                Object[] paramters = types.ConvertAll(item => typePairs[item].GetCache(typePairs)).ToArray();
                _obj = Activator.CreateInstance(_type, paramters);
            }
            switch (_typeEnum)
            {
                case ServiceLifetime.Transient:
                    List<Type> types = GetConstructor();
                    Object[] paramters = types.ConvertAll(item => typePairs[item].GetCache(typePairs)).ToArray();
                    return Activator.CreateInstance(_type, paramters);
                case ServiceLifetime.Singleton:
                    return _obj;
                case ServiceLifetime.Scoped:
                    throw new Exception("目前不支持scoped");
                default:
                    throw new Exception("请传递正确生命周期");
            }
        }
        /// <summary>
        /// 如果有相同数量的构造器,选择最后一个构造器
        /// </summary>
        /// <returns></returns>
        private List<Type> GetConstructor()
        {
            ConstructorInfo[] a = _type.GetConstructors();
            ConstructorInfo b = null;
            Int32 length = 0;
            foreach (ConstructorInfo info in a)
            {
                if (info.GetParameters().Length >= length)
                {
                    b = info;
                }
            }
            ParameterInfo[] pa = b.GetParameters();
            List<Type> list = new List<Type>();
            foreach (var p in pa)
            {
                list.Add(p.ParameterType);
            }
            return list;
        }
    }
}
