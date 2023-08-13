using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sadalmalik.Forest
{
    public class UIManager : SingletonBehaviour<UIManager>
    {
        [SerializeField] private Transform _container;

        [SerializeField] private UIScreen[] _prefabs;
        [SerializeField] private UIScreen[] _screens;

        private Dictionary<Type, UIScreen> _prefabsByType;
        private Dictionary<Type, UIScreen> _screensByType;

        private UIScreen _activeScreen;
        
        public override void Init()
        {
            _prefabsByType = new Dictionary<Type, UIScreen>();
            foreach (var prefab in _prefabs)
                _prefabsByType.Add(prefab.GetType(), prefab);

            _screensByType = new Dictionary<Type, UIScreen>();
            foreach (var screen in _screens)
                _screensByType.Add(screen.GetType(), screen);
        }

        public T GetScreen<T>(bool instantiate=true) where T : UIScreen
        {
            Type type = typeof(T);

            UIScreen screen = null;
            if (_screensByType.TryGetValue(type, out screen))
                return (T) screen;

            if (!instantiate)
                return null;
            
            if (_prefabsByType.TryGetValue(type, out var prefab))
                return (T) (_screensByType[type] = Instantiate(prefab, _container));

            return null;
        }

        public void SetActiveScreen<T>(bool instant = false) where T : UIScreen
        {
            if (_activeScreen !=null)
                _activeScreen.Hide(instant);

            _activeScreen = GetScreen<T>();
            
            if (_activeScreen !=null)
                _activeScreen.Show(instant);
        }

        public static T Get<T>(bool instantiate=true) where T : UIScreen
        {
            return Instance.GetScreen<T>(instantiate);
        }

        public static void SetActive<T>(bool instant = false) where T : UIScreen
        {
            Instance.SetActiveScreen<T>(instant);
        }
    }
}