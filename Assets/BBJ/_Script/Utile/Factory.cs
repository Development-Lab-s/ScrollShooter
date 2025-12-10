using System.Collections.Generic;
using UnityEngine;

public class Factory<T> where T : MonoBehaviour, IRecycle
{
    private List<T> _pool = new List<T>();
    private int _defaultPoolSize;
    private Transform _parent;

    private T _prefab;

    public Factory(T prefab, int defaultPoolSize = 5, Transform parent = default)
    {
        this._prefab = prefab;
        this._defaultPoolSize = defaultPoolSize;
        this._parent = parent;

        Debug.Assert(this._prefab != null, "Prefab is null");

        CreatePool();
    }

    private void CreatePool()
    {
        if (_parent == null)
        {
            _parent = new GameObject().transform;
            _parent.name = _prefab.name + " Pool";
        }
        T obj;
        for (int i = 0; i < _defaultPoolSize; i++)
        {
            obj = GameObject.Instantiate(_prefab) as T;
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_parent);
            _pool.Add(obj);
        }
    }

    public T Get()
    {
        if (_pool.Count <= 0)
        {
            CreatePool();
        }

        int lastIndex = _pool.Count - 1;
        T obj = _pool[lastIndex];
        _pool.RemoveAt(lastIndex);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Restore(T obj)
    {
        Debug.Assert(obj != null, "Null object");

        obj.gameObject.SetActive(false);
        _pool.Add(obj);
    }
}
