using UnityEngine;
using TMPro;

public class ResourceSilo : ConstructionYardModule
{
    [SerializeField] private TMP_Text _resourcesCountLabel;
    [SerializeField] private int _startingResources;

    private int _availableResources;

    public int AvailableResources => _availableResources;

    protected override void Awake()
    {
        base.Awake();

        _availableResources = _startingResources;
    }

    public void Deposit(Resource resource)
    {
        if (resource == null)
            return;

        _availableResources++;
        UpdateResourcesLabel();

        Destroy(resource.gameObject);
    }

    public void Withdraw(int count)
    {
        if (count > _availableResources)
            return;

        _availableResources -= count;
        UpdateResourcesLabel();
    }

    private void UpdateResourcesLabel()
    {
        _resourcesCountLabel.text = _availableResources.ToString();
    }
}
