using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public string ItemID;
    [TextArea]
    public string Description;
    public Sprite ItemIcon;
    public int Quantity;
}