using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    private const float ScaleFactor = 0.5f;
    private const int MinNumCubes = 2;
    private const int MaxNumCubes = 7;
    private const float SplitChanceReductionRate = 0.3f;

    [SerializeField] private float _minSplitChance = 0.1f;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private CustomExplosion _customExplosion;

    private List<Cube> _cubes = new List<Cube>();
    private float _splitChance = 1;

    private void OnMouseDown()
    {
        if (TryGetComponent(out Cube clickedCube))
        {
            SplitCube(clickedCube);
            clickedCube.ApplyExplosionForce(transform.position, _cubes);
        }
    }

    private void SplitCube(Cube cube)
    {
        _cubes.Remove(cube);

        if (ShouldSplitCube())
        {
            _splitChance -= SplitChanceReductionRate;
            Destroy(cube.gameObject);
            _customExplosion.Explode(cube.transform.position);
            int numCubes = Random.Range(MinNumCubes, MaxNumCubes);

            for (int i = 0; i < numCubes; i++)
            {
                Cube newCube = Instantiate(_cubePrefab, cube.transform.position, Quaternion.identity);
                newCube.TryGetComponent(out CubeSpawner cubeSpawner);
                cubeSpawner._splitChance = _splitChance;
                newCube.transform.localScale = cube.transform.localScale * ScaleFactor;
                newCube.SetRandomColor();
                _cubes.Add(newCube);
            }
        }
        else
        {
            Destroy(cube.gameObject);
            _customExplosion.Explode(cube.transform.position);
        }
    }

    private bool ShouldSplitCube()
    {
        return Random.value < _splitChance && _splitChance > _minSplitChance;
    } 
}