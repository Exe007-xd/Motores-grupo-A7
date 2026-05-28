using UnityEngine;

public class HealingItem : MonoBehaviour, IInteractable
{
    private int _healingAmount = 2;

    public void Interact()
    {
        PlayerHealth playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(_healingAmount);
            Destroy(gameObject);
        }
    }
}
