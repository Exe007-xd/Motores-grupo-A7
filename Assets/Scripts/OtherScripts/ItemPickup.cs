using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO item;

    public void Interact()
    {
        InventoryManager.Instance.AddItem(item);

        Destroy(gameObject);
    }
}
