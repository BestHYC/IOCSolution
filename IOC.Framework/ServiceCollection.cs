using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace IOC.Framework
{
    /// <summary>
    /// 通过特写
    /// </summary>
    public class ServiceCollection : IServiceCollection
    {
        private ConcurrentDictionary<Type, IServiceCache> _typePairs;
        public ServiceCollection()
        {
            _typePairs = new ConcurrentDictionary<Type, IServiceCache>();
        }
        public IServiceCollection AddScoped<T1, T2>() where T2 : T1
        {
            throw new NotImplementedException();
        }
        public IServiceCollection AddScoped<T1>(T1 t2)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 构建单例
        /// </summary>
        /// <typeparam name="T1">基类型</typeparam>
        /// <typeparam name="T2">子类实例</typeparam>
        /// <returns></returns>
        public IServiceCollection AddSingleton<T1, T2>() where T2 : T1
        {
            Type t1 = typeof(T1);
            Type t2 = typeof(T2);
            ServiceTypeCache service = new ServiceTypeCache(t2, ServiceLifetime.Singleton);
            if (!_typePairs.TryAdd(t1, service))
            {
                throw new Exception("在注入对象时,有相同对象存在");
            }
            return this;
        }
        public IServiceCollection AddTransient<T1, T2>() where T2 : T1
        {
            Type t1 = typeof(T1);
            Type t2 = typeof(T2);
            ServiceTypeCache service = new ServiceTypeCache(t2, ServiceLifetime.Transient);
            if (!_typePairs.TryAdd(t1, service))
            {
                throw new Exception("在注入对象时,有相同对象存在");
            }
            return this;
        }
        /// <summary>
        /// 构建单列
        /// </summary>
        /// <typeparam name="T1">泛型类型</typeparam>
        /// <param name="t2">类型对象信息</param>
        /// <returns></returns>
        public IServiceCollection AddSingleton<T>(T t2)
        {
            Type t = typeof(T);
            ServiceTypeCache service = new ServiceTypeCache(t2, ServiceLifetime.Singleton);
            if (!_typePairs.TryAdd(t, service))
            {
                throw new Exception("在注入对象时,有相同对象存在");
            }
            return this;
        }
        /// <summary>
        /// 每次都注入新的对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        
        /// <summary>
        /// 每次都注入新的对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="t2"></param>
        /// <returns></returns>
        public IServiceCollection AddTransient<T>(T t2)
        {
            Type t = typeof(T);
            ServiceTypeCache service = new ServiceTypeCache(t2, ServiceLifetime.Transient);
            if (!_typePairs.TryAdd(t, service))
            {
                throw new Exception("在注入对象时,有相同对象存在");
            }
            return this;
        }
        /// <summary>
        /// 提供真实对象信息
        /// </summary>
        /// <returns></returns>
        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(_typePairs);
        }
    }
}
