using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public abstract class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    static Singleton<T> _instance;

    protected static object _lock = new object();

    protected virtual void Awake()
    {
        bool destroyMe = true;

        if(_instance == null)
        {
            lock(_lock)
            {
                if (_instance == null)
                {
                    destroyMe = false;
                    _instance = this;

                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        if(destroyMe)
        {
            Destroy(gameObject);
            return;
        }
    }

    public static T Instance
    {
        get
        {
            return _instance as T;
        }
    }    
}
