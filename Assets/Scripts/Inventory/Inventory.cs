using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }


    private void Awake()
    {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
    }


    // 用于存放物品的容器（比如背包）
    private List<ItemData> items = new List<ItemData>();

    // 添加物品（支持叠加）
    public void AddItem(ItemData item)
    {
        if (item == null) return;

        // 查找是否已有同 ID 的物品
        ItemData existing = items.Find(i => i.ItemID == item.ItemID);
        if (existing != null)
        {
            existing.Quantity += item.Quantity;
            Debug.Log($"叠加物品：{existing.ItemName} x{existing.Quantity}");
        }
        else
        {
            items.Add(new ItemData
            {
                ItemID = item.ItemID,
                ItemName = item.ItemName,
                Quantity = item.Quantity,
                ItemIcon = item.ItemIcon
            });
            Debug.Log($"添加新物品：{item.ItemName} x{item.Quantity}");
        }
    }

    // 移除物品
    public void RemoveItem(ItemData item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"移除物品：{item.ItemName}");
        }
    }

    // 获取当前所有物品
    public List<ItemData> GetAllItems()
    {
        return new List<ItemData>(items); // 返回一个拷贝，避免外部直接修改
    }

    // 预留方法：上传仓库数据到数据库
    public void UploadToDatabase()
    {
        // TODO: 这里将来写上传逻辑，比如发送到后端
        Debug.Log("上传仓库数据到数据库（未实现逻辑）");
    }
}
