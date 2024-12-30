using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private protected T Prefab;
    [SerializeField] private protected int MaxCapacity;
    [SerializeField] private protected MaterialHolder MaterialHolder;
    [SerializeField] private protected bool IsAutoExpand;

    private readonly float _minLifeTime = 2;
    private readonly float _maxLifeTime = 6;

    private protected int ValueOfActiveElements;
    private protected ObjectPool<T> Pool;

    public abstract event Action<int> ValueOfActiveElementsChanged;

    private void Awake()
    {
        Pool = new ObjectPool<T>(Prefab, MaxCapacity, transform);
        Pool.AutoExpand = IsAutoExpand;

        List<T> list = Pool.GetAllElements();

        foreach (T element in list)
            Initialize(element);
    }

    public string GetTypeName()
    {
        return typeof(T).Name + "s";
    }

    private protected float GetLifeTime()
    {
        return Random.Range(_minLifeTime, _maxLifeTime);
    }

    private protected abstract void Initialize(T element);
}