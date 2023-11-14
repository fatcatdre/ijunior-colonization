using UnityEngine;
using System;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private Transform _spawnLocation;

    private bool _isFull;

    public event Action<SpawnPoint> Spawned;
    public event Action<SpawnPoint> Emptied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource _))
        {
            _isFull = true;
            Spawned?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Resource _))
        {
            _isFull = false;
            Emptied?.Invoke(this);
        }
    }

    public void Spawn()
    {
        if (_isFull)
            return;

        Instantiate(_prefab, _spawnLocation.position, _spawnLocation.rotation);
    }
}
