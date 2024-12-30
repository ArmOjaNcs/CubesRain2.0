public class CubesText : UIText<CubesSpawner>
{
    private void OnEnable()
    {
        Spawner.ValueOfActiveElementsChanged += OnValueUpdate;
    }

    private void OnDisable()
    {
        Spawner.ValueOfActiveElementsChanged -= OnValueUpdate;
    }

    private protected override void Start()
    {
        TypeName = Spawner.GetTypeName();
        base.Start();
    }
}