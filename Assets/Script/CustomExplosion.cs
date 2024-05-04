using UnityEngine;

public class CustomExplosion : MonoBehaviour
{
    [SerializeField] private float _maxForce = 300f;
    [SerializeField] private float _baseRadius = 5f;

    public void Explode(Vector3 center)
    {
        Collider[] colliders = Physics.OverlapSphere(center, _baseRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidBody))
            {
                float scaleFactor = Mathf.Max(rigidBody.transform.localScale.x, 
                    rigidBody.transform.localScale.y, rigidBody.transform.localScale.z);

                float radiusMultiplier = 1 / scaleFactor;

                float radius = _baseRadius * radiusMultiplier;
                float force = _maxForce * radiusMultiplier;

                rigidBody.AddExplosionForce(force, center, radius);
            }
        }
    }
}
