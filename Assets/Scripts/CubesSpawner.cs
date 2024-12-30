using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubesSpawner : Spawner<Cube>
{
    [SerializeField] private float _delay;
    [SerializeField] private Transform _ground;
    [SerializeField] private float _height;
    [SerializeField, Range(1, 5)] private float _offset;

    private WaitForSeconds _wait;

    public event Action<Vector3> CubeFinished;
    public override event Action<int> ValueOfActiveElementsChanged;

    private float MinXPosition => _ground.position.x - _ground.localScale.x * 5 + _offset;
    private float MaxXPosition => MinXPosition + _ground.localScale.x * 10 - _offset * 2;
    private float YPosition => _ground.position.y + _height;
    private float MinZPosition => _ground.position.z - _ground.localScale.z * 5 + _offset;
    private float MaxZPosition => MinZPosition + _ground.localScale.z * 10 - _offset * 2;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        StartCoroutine(BeginnigCubesRain());
    }

    private protected override void Initialize(Cube cube)
    {
        cube.InitLifeTime(GetLifeTime());
        cube.SetMaterialHolder(MaterialHolder);
        cube.SetInitialize();
        cube.FinishLifeTime += OnFinishedLifeTime;
    }

    private void OnFinishedLifeTime(ObjectToSpawn cube)
    {
        ValueOfActiveElements--;
        ValueOfActiveElementsChanged?.Invoke(ValueOfActiveElements);
        CubeFinished?.Invoke(cube.transform.position);
        cube.Deactivate();
    }

    private Vector3 GetSpawnPosition()
    {
        float xPosition = Random.Range(MinXPosition, MaxXPosition);
        float zPosition = Random.Range(MinZPosition, MaxZPosition);

        return new Vector3(xPosition, YPosition, zPosition);
    }

    private IEnumerator BeginnigCubesRain()
    {
        while (enabled)
        {
            yield return _wait;

            if (Pool.HasFreeElements(out Cube cube))
            {
                cube.SetStartPosition(GetSpawnPosition());
                cube.Activate();
                ValueOfActiveElements++;
                ValueOfActiveElementsChanged?.Invoke(ValueOfActiveElements);
            }
            else
            {
                Cube newCube = Pool.GetFreeElement();
                Initialize(newCube);
                newCube.SetStartPosition(GetSpawnPosition());
                newCube.Activate();
                ValueOfActiveElements++;
                ValueOfActiveElementsChanged?.Invoke(ValueOfActiveElements);
            }
        }
    }
}