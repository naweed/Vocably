using System;
using System.Collections.Generic;
using System.Text;

namespace XGENO.Mobile.Framework.Services
{
    public static class ContainerService
    {
        private static Dictionary<Type, object> _singletons = new();
        private static Dictionary<Type, Type> _instances = new();

        public static void RegisterSingleton<T>(object obj)
        {
            var interfaceType = typeof(T);

            if (!_singletons.ContainsKey(interfaceType))
                _singletons.Add(interfaceType, obj);
            else
                _singletons[interfaceType] = obj;
        }

        public static T GetSingleton<T>()
        {
            var interfaceType = typeof(T);

            if (_singletons.ContainsKey(interfaceType))
                return (T)_singletons[interfaceType];

            throw new Exception("Interface is not registered");
        }

        public static void Register<T, S>()
        {
            var interfaceType = typeof(T);
            var instanceType = typeof(S);

            if (!_instances.ContainsKey(interfaceType))
                _instances.Add(interfaceType, instanceType);
            else
                _instances[interfaceType] = instanceType;
        }

        public static T GetInstance<T>()
        {
            var interfaceType = typeof(T);

            if (_instances.ContainsKey(interfaceType))
            {
                var instanceType = _instances[interfaceType];
                return (T) Activator.CreateInstance(instanceType);
            }

            throw new Exception("Interface is not registered");
        }

        public static void RegisterType<T>()
        {
            var interfaceType = typeof(T);

            if (!_instances.ContainsKey(interfaceType))
                _instances.Add(interfaceType, interfaceType);
            else
                _instances[interfaceType] = interfaceType;
        }


    }
}
