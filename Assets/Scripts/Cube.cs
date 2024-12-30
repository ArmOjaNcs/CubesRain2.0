using System;
using System.Collections;
using UnityEngine;

public class Cube : ObjectToSpawn
{
    private bool _isFirstCollision;
    private WaitForSeconds _lifeTime;

    public override event Action<ObjectToSpawn> FinishLifeTime;

    public Rigidbody CubeRigidbody => Rigidbody;

    private void OnEnable()
    {
        if(_isInitialized)
            Renderer.material = MaterialHolder.DropDownCubeDefaultMaterial;

        _isFirstCollision = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _isFirstCollision)
        {
            _isFirstCollision = false;
            Renderer.material = MaterialHolder.GetMaterial();
            StartCoroutine(BeginLifeTime());
        }
    }

    public override void InitLifeTime(float lifeTime)
    {
        _lifeTime = new WaitForSeconds(lifeTime);
    }

    private protected override IEnumerator BeginLifeTime()
    {
        yield return _lifeTime;

        FinishLifeTime?.Invoke(this);
    }
}