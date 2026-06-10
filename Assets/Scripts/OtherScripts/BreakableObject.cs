using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] private GameObject brokenPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        NoiseSystem.MakeNoise(transform.position, 10f);

        if (brokenPrefab != null)
        {
            Instantiate(brokenPrefab,transform.position,transform.rotation);
        }

        Destroy(gameObject);
    }
}

