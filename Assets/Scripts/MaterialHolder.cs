using UnityEngine;

public class MaterialHolder : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Material _dropDownCubeDefaultMaterial;
    [SerializeField] private Material _bombDefaultMaterial;

    public Material DropDownCubeDefaultMaterial => _dropDownCubeDefaultMaterial;
    public Material BombDefaultMaterial => _bombDefaultMaterial;

    public Material GetMaterial()
    {
        int numberOfMaterial = Random.Range(0, _materials.Length);
        return _materials[numberOfMaterial];
    }
}