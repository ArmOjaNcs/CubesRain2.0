using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;

    public ObjectPool(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container;
        CreatePull(count);
    }

    public T Prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform Container { get; }

    public bool HasFreeElements(out T element)
    {
        foreach(var objectToSpawn in _pool)
        {
            if (objectToSpawn.gameObject.activeInHierarchy == false)
            {
                element = objectToSpawn;
                objectToSpawn.gameObject.SetActive(false);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElements(out var element))
            return element;

        if(AutoExpand)
            return CreateObject();

        throw new Exception($"There is no free elements in pool type {typeof(T)}");
    }

    public List<T> GetAllElements()
    {
        return _pool;
    }

    private void CreatePull(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private T CreateObject()
    {
        var createdObject = MonoBehaviour.Instantiate(Prefab);
        createdObject.gameObject.SetActive(false);
        _pool.Add(createdObject);
        return createdObject;
    }
}