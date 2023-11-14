using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;

    private List<SpawnPoint> _spawnPoints;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());

        UpdateSpawnDelay();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());

        foreach (SpawnPoint point in _spawnPoints)
        {
            point.Spawned += OnResourceSpawned;
            point.Emptied += OnResourceEmptied;
        }
    }

    private void OnDisable()
    {
        foreach (SpawnPoint point in _spawnPoints)
        {
            point.Spawned -= OnResourceSpawned;
            point.Emptied -= OnResourceEmptied;
        }
    }

    private void OnValidate()
    {
        UpdateSpawnDelay();
    }

    private void OnResourceSpawned(SpawnPoint spawnPoint)
    {
        if (spawnPoint == null)
            return;

        _spawnPoints.Remove(spawnPoint);
    }

    private void OnResourceEmptied(SpawnPoint spawnPoint)
    {
        if (spawnPoint == null)
            return;

        _spawnPoints.Add(spawnPoint);
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            SpawnPoint spawnPoint = GetRandomSpawnPoint();

            if (spawnPoint != null)
                spawnPoint.Spawn();

            yield return _delay;
        }
    }

    private void UpdateSpawnDelay()
    {
        _delay = new WaitForSeconds(_spawnDelay);
    }

    private SpawnPoint GetRandomSpawnPoint()
    {
        if (_spawnPoints.Count == 0)
            return null;

        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }
}
