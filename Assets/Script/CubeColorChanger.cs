using UnityEngine;
using System.Collections;

public class CubeColorChanger : MonoBehaviour
{
    private const int MinDelay = 2;
    private const int MaxDelay = 5;

    [SerializeField] private CubePool _cubePool;
    private bool _hasCollided = true;
    private Color _originalColor;
    private Renderer _cubeRenderer;

    private void Start()
    {
        _cubeRenderer = GetComponent<Renderer>();
        _originalColor = _cubeRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out CubeSpawner cubeSpawner) && _hasCollided)
        {
            _hasCollided = false;
            ChangeColor();
            TryGetComponent(out Cube cube);

            float delay = Random.Range(MinDelay, MaxDelay);
            StartCoroutine(ReturnCubeWithDelay(cube, delay)); 
        }
    }

    private IEnumerator ReturnCubeWithDelay(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);
        _cubePool.ReturnCubeToPool(cube);
        _hasCollided = true;
        _cubeRenderer.material.color = _originalColor;
    }

    private void ChangeColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
        _cubeRenderer.material.color = randomColor;
    }
}
