using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody), typeof(CubeSpawner))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float _explosionRadius = 5f;

    private Rigidbody rigidBody;

    public void SetRandomColor()
    {
        if (TryGetComponent(out Renderer renderer))
        {
            Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            renderer.material.color = randomColor;
        }
    }

    public void ApplyExplosionForce(Vector3 center, List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            if (cube.TryGetComponent(out rigidBody))
            {
                rigidBody.AddExplosionForce(_explosionForce, center, _explosionRadius);
            }
        }
    }
}