using System;
using UnityEngine;

public class BombsSpawner : Spawner<Bomb>
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private CubesSpawner _cubeSpawner;

    public override event Action<int> ValueOfActiveElementsChanged;

    private void OnEnable()
    {
        _cubeSpawner.CubeFinished += OnCubeFinished;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeFinished -= OnCubeFinished;
    }

    private void OnCubeFinished(Vector3 position)
    {
        if (Pool.HasFreeElements(out Bomb bomb))
        {
            bomb.SetStartPosition(position);
            bomb.Activate();
            ValueOfActiveElements++;
            ValueOfActiveElementsChanged?.Invoke(ValueOfActiveElements);
        }
        else
        {
            Bomb newBomb = Pool.GetFreeElement();
            Initialize(newBomb);
            newBomb.SetStartPosition(position);
            newBomb.Activate();
            ValueOfActiveElements++;
            ValueOfActiveElementsChanged?.Invoke(ValueOfActiveElements);
        }
    }

    private protected override void Initialize(Bomb bomb)
    {
        bomb.InitLifeTime(GetLifeTime());
        bomb.SetMaterialHolder(MaterialHolder);
        bomb.SetExploder(_exploder);
        bomb.SetInitialize();
        bomb.FinishLifeTime += OnFinishLifeTime;
    }

    private void OnFinishLifeTime(ObjectToSpawn bomb)
    {
        ValueOfActiveElements--;
        ValueOfActiveElementsChanged?.Invoke(ValueOfActiveElements);
        bomb.Deactivate();
    }
}