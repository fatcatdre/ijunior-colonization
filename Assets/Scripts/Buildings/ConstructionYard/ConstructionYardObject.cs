using UnityEngine;

[CreateAssetMenu]
public class ConstructionYardObject : ScriptableObject
{
    [SerializeField] private ConstructionYard _prefab;

    public ConstructionYard Prefab => _prefab;
}
