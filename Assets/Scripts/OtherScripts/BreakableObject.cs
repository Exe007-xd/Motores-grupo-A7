using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject brokenPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        NoiseSystem.MakeNoise(transform.position);

        if (brokenPrefab != null)
        {
            Instantiate(
                brokenPrefab,
                transform.position,
                transform.rotation
            );
        }

        Destroy(gameObject);
    }
}

