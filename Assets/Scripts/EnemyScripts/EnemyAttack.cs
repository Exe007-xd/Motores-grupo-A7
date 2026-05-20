using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 3;
    [SerializeField] private Collider _collider;


    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth.Instance.GetDamage(_damage);
        }
    }

    
}




