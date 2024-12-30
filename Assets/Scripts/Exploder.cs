using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField, Range(5, 50)] private float _explosionRadius;
    [SerializeField, Range(100, 1000)] private float _explosionForce;
    [SerializeField, Range(1, 30)] private float _reducedForceMultiplier;

    public void Exploid(Vector3 exploidPosition)
    {
        Collider[] hits = Physics.OverlapSphere(exploidPosition, _explosionRadius);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            Vector3 offset = exploidPosition - rigidbody.transform.position;
            float reducedForce = offset.magnitude * _reducedForceMultiplier;
            rigidbody.AddExplosionForce(_explosionForce - reducedForce, exploidPosition, _explosionRadius);
        }
    }
}