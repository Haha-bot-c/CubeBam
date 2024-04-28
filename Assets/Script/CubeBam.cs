using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class CubeBam : MonoBehaviour
{
    private const float ScaleFactor = 0.5f;
    private const int MinNumCubes = 2;
    private const int MaxNumCubes = 6;
    private const float InitialSplitChance = 1f;
    private const float MinSplitChance = 0.1f;

    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private AnimationCurve _splitChanceCurve;
    [SerializeField] private CubeBam _cubePrefab; 

    private List<GameObject> _cubes = new List<GameObject>();

    private void OnMouseDown()
    {
        SplitCube(gameObject);
        ApplyExplosionForce(transform.position);
    }

    private void SplitCube(GameObject cube)
    {
        _cubes.Remove(cube);

        if (ShouldSplitCube())
        {
            Destroy(cube);

            int numCubes = Random.Range(MinNumCubes, MaxNumCubes + 1);
            for (int i = 0; i < numCubes; i++)
            {
                GameObject newCube = Instantiate(_cubePrefab.gameObject, cube.transform.position, Quaternion.identity);
                newCube.transform.localScale = cube.transform.localScale * ScaleFactor;
                SetRandomColor(newCube);
                _cubes.Add(newCube);
            }
        }
        else
        {
            Destroy(cube);
        }
    }

    private bool ShouldSplitCube()
    {
        float normalizedTime = 1f - Mathf.Clamp01(InitialSplitChance);
        float curveValue = _splitChanceCurve.Evaluate(normalizedTime);
        return Random.value < curveValue && Random.value > MinSplitChance;
    }

    private void ApplyExplosionForce(Vector3 center)
    {
        foreach (GameObject cube in _cubes)
        {
            Rigidbody rb = cube.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(_explosionForce, center, _explosionRadius);
            }
        }
    }

    private void SetRandomColor(GameObject cube)
    {
        Renderer renderer = cube.GetComponent<Renderer>();

        if (renderer != null)
        {
            Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            renderer.material.color = randomColor;
        }
    }
}
