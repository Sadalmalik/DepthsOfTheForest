using System;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    CreateInstance();
                return _instance;
            }
        }

        public void Awake()
        {
            if (_instance != null && _instance != this)
                throw new Exception(
                    $"Singleton behaviour duplication detected! Please check followed objects:\n{_instance}\n{this}");

            if (_instance == this)
                return;
            _instance = this as T;

            _instance.name = $"[singleton] {this.name}";

            Init();
        }

        public abstract void Init();

        private static void CreateInstance()
        {
            GameObject o = new GameObject($"[singleton] {typeof(T).Name}");
            _instance = o.AddComponent<T>();
        }
    }
}