//using Common.Unity;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.LogError( "[Singleton] Something went really wrong " +
                                   " - there should never be more than 1 singleton!" +
                                   " Reopening the scene might fix it.");
                    return _instance;
                }

                if (_instance == null)
                {
                    GameObject singletonPrefab = null;
                    GameObject singleton = null;

                    // Check if exists a singleton prefab on Resources Folder.
                    // -- Prefab must have the same name as the Singleton SubClass
                    singletonPrefab = (GameObject)Resources.Load(typeof(T).ToString(), typeof(GameObject));

                    // Create singleton as new or from prefab
                    if (singletonPrefab != null)
                    {
                        singleton = Instantiate(singletonPrefab);
                        _instance = singleton.GetComponent<T>();
                    }
                    else
                    {
                        singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                    }

                    DontDestroyOnLoad(singleton);
                    singleton.name = "[IS: " + typeof(T) + "]";

                    _instance.Init();


                    // Debug.Log("[Singleton] An instance of " + typeof(T) + 
                    // 	" is needed in the scene, so '" + singleton +
                    // 	"' was created with DontDestroyOnLoad.");
                }
                else
                {
                    Debug.Log("[Singleton] Using instance already created: " +
                              _instance.gameObject.name);
                }
            }

            return _instance;
        }
    }
    protected virtual void Init() { }
}