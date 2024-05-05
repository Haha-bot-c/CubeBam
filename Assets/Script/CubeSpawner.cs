using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private const float RangeX = 5f;
    private const float RangeY = 10f;
    private const float RangeZ = 5f;

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
        Cube cube = _cubePool.GetCubeFromPool();

        if (cube != null)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(transform.position.x - RangeX, transform.position.x + RangeX),
                transform.position.y + RangeY,
                Random.Range(transform.position.z - RangeZ, transform.position.z + RangeZ)
            );

            cube.transform.position = spawnPosition;
        }
    }
}
