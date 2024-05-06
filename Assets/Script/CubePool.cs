using UnityEngine;
using System.Collections.Generic;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    private Queue<Cube> _pool = new Queue<Cube>();

    public Cube GiveCubeFromPool()
    {
        Cube cube;

        if (_pool.Count > 0)
        {
            cube = _pool.Dequeue();
            cube.gameObject.SetActive(true);

            return cube;
        }
        else 
        {
            cube = Instantiate(_prefab);
            cube.AssignPool(this);

            return cube;
        }
    }

    public void ReturnCubeToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }
}
