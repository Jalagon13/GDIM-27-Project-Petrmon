using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static bool _applicationIsQuitting;

        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting) return null;
                
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        GameObject go = new GameObject { name = typeof(T).Name };
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            // Ensures _applicationIsQuitting is set properly after destroying an already
            // existing singleton instance.
            _applicationIsQuitting = _instance == null;
        }
    }
}
