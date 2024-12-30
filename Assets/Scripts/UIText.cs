using TMPro;
using UnityEngine;

public abstract class UIText<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private protected TextMeshProUGUI Text;
    [SerializeField] private protected T Spawner;

    private readonly char Xsign = 'X';

    private protected string TypeName;

    private protected virtual void Start()
    {
        Text.text = TypeName + Xsign + 0;
    }

    private protected void OnValueUpdate(int currentValue)
    {
        Text.text = TypeName + Xsign + currentValue;
    }
}