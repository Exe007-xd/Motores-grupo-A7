using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private Collider _collider;

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth.Instance.GetDamage(_damage);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
