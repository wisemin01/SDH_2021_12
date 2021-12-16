using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Awake()
    {
        if (prefab.activeSelf)
        {
            prefab.SetActive(false);
        }
    }

    public GameObject Get()
    {
        GameObject obj;

        if (_pool.Count > 0)
            obj = _pool.Dequeue();
        else
            obj = Instantiate(prefab);

        if (!obj.activeSelf)
            obj.SetActive(true);

        return obj;
    }

    public void Return(GameObject obj)
    {
        if (obj == null)
            return;

        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }

        _pool.Enqueue(obj);
    }
}
