using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private readonly Vector3 SpawnRange = new Vector3(5f, 10f, 5f);

    [SerializeField] private CubePool _cubePool;
    [SerializeField] private float _spawnInterval = 1f;

    private float _spawnTimer = 0f;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnInterval)
        {
            SpawnCube();
            _spawnTimer = 0f;
        }
    }

    private void SpawnCube()
    {
        Cube cube = _cubePool.GiveCubeFromPool();

        if (cube != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-SpawnRange.x, SpawnRange.x),
                SpawnRange.y,
                Random.Range(-SpawnRange.z, SpawnRange.z)
            );

            cube.transform.position = spawnPosition;
        }
    }
}
