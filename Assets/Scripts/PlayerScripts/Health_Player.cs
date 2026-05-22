using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;
    
    private static PlayerHealth instance;
    public static PlayerHealth Instance => instance;

    private bool _isDead = false;

    private void Awake()
    {
       
        if (instance = null)
        {
            instance = this;
        }

        _currentHealth = _maxHealth;

    }

    public void GetDamage(int damage)
    {
        if (_isDead) return;
        _currentHealth -= damage;
        Debug.Log("Daño =" + damage);
        GameEvents.OnPlayerDamageHealth(_currentHealth, _maxHealth);


        if (_currentHealth <= 0)
        {
            Die();
        }
    }
   

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        SceneManager.LoadScene("Derrota");
    }
}
