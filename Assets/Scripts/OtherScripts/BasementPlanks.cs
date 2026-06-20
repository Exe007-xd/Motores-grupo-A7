using UnityEngine;

public class BasementPlanks : MonoBehaviour, IInteractable
{
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private ItemSO crowbarItem;
    public QTEManager qteManager;
    public void Interact()
    {
        if (!inventory.HasItem(crowbarItem))
        {
            Debug.Log("I need something to remove these planks.");
            return;
        }

        qteManager.StartQTE();
        
    }


}
