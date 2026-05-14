using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health = 3;
   
    private static PlayerHealth instance;
    public static PlayerHealth Instance => instance;

    private void Awake()
    {
       
        if (instance = null)
        {
            instance = this;
        }
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        Debug.Log("Daño =" + damage);
        
        if (_health <= 0)
        {
            Die();
        }
    }
   

    private void Die()
    {
        SceneManager.LoadScene("Derrota");
    }
}
