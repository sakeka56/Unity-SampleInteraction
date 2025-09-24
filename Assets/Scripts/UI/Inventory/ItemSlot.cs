using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;   // 显示物品图标
    [SerializeField] private TextMeshProUGUI quantityText; // 显示数量

    private ItemData currentItem;

    // 设置插槽里的物品
    public void SetItem(ItemData item)
    {
        currentItem = item;

        if (item != null)
        {
            icon.sprite = item.ItemIcon;  // 假设 ItemData 里有 Sprite Icon
            icon.enabled = true;
            quantityText.text = item.Quantity > 1 ? item.Quantity.ToString() : "";
        }
        else
        {
            ClearSlot();
        }
    }

    // 清空插槽
    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }
}
