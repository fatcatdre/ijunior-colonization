using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceScanner : ConstructionYardModule
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private float _scanFrequency;
    [SerializeField] private GameObject _collectionRing;

    private WaitForSeconds _frequency;
    private List<Resource> _scannedResources = new();

    public float ScanRadius => _scanRadius;
    public List<Resource> ScannedResources => new(_scannedResources);
    public int ScannedResourceCount => _scannedResources.Count;

    protected override void Awake()
    {
        base.Awake();

        UpdateScanFrequency();
    }

    private void OnEnable()
    {
        StartCoroutine(Scan());
    }

    private void OnValidate()
    {
        UpdateScanFrequency();

        _collectionRing.transform.localScale = new Vector3(_scanRadius * 2f, _scanRadius * 2f, _collectionRing.transform.localScale.z);
    }

    public Resource GetRandomResource()
    {
        if (_scannedResources.Count == 0)
            return null;

        Resource resource = _scannedResources[Random.Range(0, _scannedResources.Count)];
        _scannedResources.Remove(resource);

        return resource;
    }

    private IEnumerator Scan()
    {
        while (enabled)
        {
            Collider[] results = Physics.OverlapSphere(transform.position, _scanRadius);

            foreach (var result in results)
            {
                if (result.TryGetComponent(out Resource resource) == false)
                    continue;

                ScanResource(resource);
            }

            yield return _frequency;
        }
    }

    private void ScanResource(Resource resource)
    {
        if (resource.HasBeenScanned || _scannedResources.Contains(resource))
            return;

        resource.HasBeenScanned = true;

        _scannedResources.Add(resource);
    }

    private void UpdateScanFrequency()
    {
        _frequency = new WaitForSeconds(_scanFrequency);
    }
}
