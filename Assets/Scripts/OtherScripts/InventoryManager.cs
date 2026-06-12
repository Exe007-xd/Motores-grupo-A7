using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    // Array fijo de 3 espacios
    public ItemSO[] items = new ItemSO[3];

    public bool AddItem(ItemSO newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                Debug.Log(newItem.itemName + " añadido.");
                // Aquí actualizarías la UI
                return true; // Ítem añadido
            }
        }
        Debug.Log("Inventario lleno");
        return false; // Inventario lleno
    }
}
