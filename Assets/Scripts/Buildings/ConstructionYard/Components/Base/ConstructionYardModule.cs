using UnityEngine;

[RequireComponent(typeof(ConstructionYard))]
public abstract class ConstructionYardModule : MonoBehaviour
{
    protected ConstructionYard _constructionYard;

    protected virtual void Awake()
    {
        _constructionYard = GetComponent<ConstructionYard>();
    }
}
