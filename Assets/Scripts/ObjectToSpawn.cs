using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class ObjectToSpawn : MonoBehaviour
{
    private protected MeshRenderer Renderer;
    private protected Rigidbody Rigidbody;
    private protected MaterialHolder MaterialHolder;
    private protected bool _isInitialized;

    public abstract event Action<ObjectToSpawn> FinishLifeTime;

    public bool IsInitialized => _isInitialized;

    private void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();
        _isInitialized = false;
    }

    public abstract void InitLifeTime(float lifeTime);

    public void SetMaterialHolder(MaterialHolder materialHolder)
    {
        MaterialHolder = materialHolder;
    }

    public void SetStartPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void SetInitialize()
    {
        _isInitialized = true;
    }

    private protected abstract IEnumerator BeginLifeTime();
}