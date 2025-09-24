using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;    // 插槽预制体
    [SerializeField] private Transform slotParent;     // 插槽的父物体（通常是 Grid Layout Group）

    private Inventory inventory;
    private ItemSlot[] slots;


    // 初始化 UI
    public void Init(Inventory inv, int slotCount = 20)
    {
        inventory = inv;

        // 动态生成插槽
        slots = new ItemSlot[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            slots[i] = slotObj.GetComponent<ItemSlot>();
        }

        RefreshUI();
    }

    // 刷新 UI（当物品变化时调用）
    public void RefreshUI()
    {
        var items = inventory.GetAllItems();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
                slots[i].SetItem(items[i]);
            else
                slots[i].ClearSlot();
        }
    }
}
