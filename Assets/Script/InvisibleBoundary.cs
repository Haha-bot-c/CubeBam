using UnityEngine;

public class InvisibleBoundary : MonoBehaviour
{
    private const float BoundaryDivisionFactor = 2f;

    [SerializeField] private Vector3 _boundarySize = new Vector3(10f, 10f, 10f);

    private GameObject[] _cubes;

    private void Update()
    {
        _cubes = GameObject.FindGameObjectsWithTag("Cube");

        foreach (GameObject cube in _cubes)
        {
            if (cube != null)
            {
                ClampCubePosition(cube);
            }
        }
    }

    private void ClampCubePosition(GameObject cube)
    {
        Vector3 clampedPosition = cube.transform.position;

        for (int i = 0; i < 3; i++)
        {
            float boundaryHalfSize = _boundarySize[i] / BoundaryDivisionFactor;
            float boundaryMin = transform.position[i] - boundaryHalfSize;
            float boundaryMax = transform.position[i] + boundaryHalfSize;
            clampedPosition[i] = Mathf.Clamp(clampedPosition[i], boundaryMin, boundaryMax);
        }

        cube.transform.position = clampedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, _boundarySize);
    }
}
