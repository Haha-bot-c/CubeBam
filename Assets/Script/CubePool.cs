using UnityEngine;
using System.Collections.Generic;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolSize = 10;

    private Queue<Cube> _cubePool = new Queue<Cube>();

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Cube cube = Instantiate(_cubePrefab, Vector3.zero, Quaternion.identity);
            cube.gameObject.SetActive(false);
            _cubePool.Enqueue(cube);
        }
    }

    public Cube GetCubeFromPool()
    {
        if (_cubePool.Count == 0)
        {
            return null;
        }

        Cube cube = _cubePool.Dequeue();
        cube.gameObject.SetActive(true);
        return cube;
    }

    public void ReturnCubeToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _cubePool.Enqueue(cube);
    }
}
