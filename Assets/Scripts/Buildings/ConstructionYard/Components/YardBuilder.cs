using UnityEngine;

public class YardBuilder : ConstructionYardModule
{
    [SerializeField] private int _buildCost;
    [SerializeField] private ConstructionYardObject _constructionYardObject;
    [SerializeField] private Transform _constructionFlag;

    private Bot _builder;

    public int BuildCost => _buildCost;
    public bool IsBuilding => _builder != null;

    public void Build()
    {
        if (_constructionYard.AvailableResources < _buildCost || IsBuilding)
            return;

        Bot builder = _constructionYard.RequestBot();

        if (builder == null)
            return;

        _constructionYard.RequestResources(_buildCost);

        builder.Build(_constructionYardObject.Prefab, _constructionFlag);
    }

    public void FinishBuilding()
    {
        _builder = null;
    }
}
