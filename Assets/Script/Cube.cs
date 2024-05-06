using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(CubeColorChanger))]
public class Cube : MonoBehaviour
{
    private const int MinDelay = 2;
    private const int MaxDelay = 5;

    
    [SerializeField] private CubeColorChanger _colorChanger;

    private CubePool _pool;
    private bool _hasCollided = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out CubeSpawner cubeSpawner) && _hasCollided)
        {
            _hasCollided = false;
            _colorChanger.ChangeColor();
            TryGetComponent(out Cube cube);

            float delay = Random.Range(MinDelay, MaxDelay);
            StartCoroutine(ReturnCubeWithDelay(cube, delay));
        }
    }

    private IEnumerator ReturnCubeWithDelay(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);
        _pool.ReturnCubeToPool(cube);
        _hasCollided = true;
        _colorChanger.ReturnColor();
    }

    public void AssignPool(CubePool pool)
    {
        _pool = pool;
    }
}