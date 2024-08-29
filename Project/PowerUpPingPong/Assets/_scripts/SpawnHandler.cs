using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPosition = new List<Transform>();
    [SerializeField] private SpawnerHolderSO _object;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private bool _shouldSpawn;
    private void Awake()
    {
        if (_spawnPosition.Count > 0)
        {
            _object.SpawnPosition = _spawnPosition[0].position;
        }
    }

    public void SpawnObject()
    {
        StartCoroutine(Spawn(_spawnDelay));
    }

    public void SpawnObject(float delay)
    {
        StartCoroutine(Spawn(delay));
    }

    private IEnumerator Spawn(float delay)
    {
        yield return new WaitUntil(() => _shouldSpawn);

        yield return new WaitForSeconds(delay);

        if (_object && _object.SpawnObject)
        {
            var spawnedObj = GameObject.Instantiate(_object.SpawnObject, _object.SpawnPosition, Quaternion.identity);
        }

        yield return null;
    }
}
