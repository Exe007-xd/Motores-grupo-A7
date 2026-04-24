using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    SpriteRenderer _spriteRenderer;
    private static PlayerHealth instance;
    public static PlayerHealth Instance => instance;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (instance = null)
        {
            instance = this;
        }
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        StartCoroutine(BlinkRed());
        
        if (_health <= 0)
        {
            Die();
        }
    }
    private IEnumerator BlinkRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        SceneManager.LoadScene("Derrota");
    }
}
