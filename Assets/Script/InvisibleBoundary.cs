using UnityEngine;

public class InvisibleBoundary : MonoBehaviour
{
    public Vector3 boundarySize = new Vector3(10f, 10f, 10f);

    private void Update()
    {
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag("Cube"))
        {
            if (cube.transform.position.x < transform.position.x - boundarySize.x / 2)
            {
                cube.transform.position = new Vector3(transform.position.x - boundarySize.x / 2, cube.transform.position.y, cube.transform.position.z);
            }
            else if (cube.transform.position.x > transform.position.x + boundarySize.x / 2)
            {
                cube.transform.position = new Vector3(transform.position.x + boundarySize.x / 2, cube.transform.position.y, cube.transform.position.z);
            }

            if (cube.transform.position.y < transform.position.y - boundarySize.y / 2)
            {
                cube.transform.position = new Vector3(cube.transform.position.x, transform.position.y - boundarySize.y / 2, cube.transform.position.z);
            }
            else if (cube.transform.position.y > transform.position.y + boundarySize.y / 2)
            {
                cube.transform.position = new Vector3(cube.transform.position.x, transform.position.y + boundarySize.y / 2, cube.transform.position.z);
            }

            if (cube.transform.position.z < transform.position.z - boundarySize.z / 2)
            {
                cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, transform.position.z - boundarySize.z / 2);
            }
            else if (cube.transform.position.z > transform.position.z + boundarySize.z / 2)
            {
                cube.transform.position = new Vector3(cube.transform.position.x, cube.transform.position.y, transform.position.z + boundarySize.z / 2);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, boundarySize);
    }
}
