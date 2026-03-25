using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace PlanA_Assets.Scripts.Core
{
    public class ObjectResolver
    {
        private readonly Dictionary<Type, Object> _instances = new Dictionary<Type, Object>();

        public void RegisterInstance<T>(T instance)
        {
            _instances[typeof(T)] = instance;
        }

        public void UnregisterType<T>()
        {
            _instances.Remove(typeof(T));
        }

        public T Resolve<T>()
        {
            Type instanceType = typeof(T);

            if (_instances.TryGetValue(instanceType, out var instance))
            {
                Debug.Log($"[ObjectResolver] Found instance of type {instanceType}");
                return (T)instance;
            }

            Debug.Log($"[ObjectResolver] Couldn't resolve type {instanceType}");
            
            return default;
        }
    }
}