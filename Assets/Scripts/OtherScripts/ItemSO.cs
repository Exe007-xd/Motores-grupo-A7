using UnityEngine;

[CreateAssetMenu(fileName = "NuevoItem", menuName = "Inventario/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
}
