using System;
using System.Collections;
using UnityEngine;

public class Bomb : ObjectToSpawn
{
    private float _lifeTime;
    private Exploder _exploder;

    public override event Action<ObjectToSpawn> FinishLifeTime;

    private void OnEnable()
    {
        if (_isInitialized)
        {
            Renderer.material = MaterialHolder.BombDefaultMaterial;
            StartCoroutine(BeginLifeTime());
        }
    }

    public override void InitLifeTime(float lifeTime)
    {
        _lifeTime = lifeTime;
    }

    public void SetExploder(Exploder exploder)
    {
        _exploder = exploder;
    }

    private IEnumerator ChangeColor()
    {
        float elapsedTime = 0;

        while (elapsedTime < _lifeTime)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _lifeTime;
            Renderer.material.color = Color.Lerp(Renderer.material.color, Color.clear, normalizedPosition * Time.deltaTime);

            yield return null;
        }

        Renderer.material.color = Color.clear;
    }

    private protected override IEnumerator BeginLifeTime()
    {
        yield return StartCoroutine(ChangeColor());

        _exploder.Exploid(transform.position);
        FinishLifeTime?.Invoke(this);
    }
}