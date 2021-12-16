using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;

    public static UIManager Get()
    {
        if (_instance == null)
        {
            var go = new GameObject("[UIManager]");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<UIManager>();
        }

        return _instance;
    }

    public T ShowAsPopup<T>(Transform parent = null)
        where T : MonoBehaviour
    {
        var go = Instantiate(Resources.Load<GameObject>($"UI/{typeof(T).Name}"), parent);
        return go.GetComponent<T>();
    }
}
