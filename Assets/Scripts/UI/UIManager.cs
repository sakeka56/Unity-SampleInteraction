using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 管理PlayerUI
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } private set { instance = value; } }

    public PlayerController PlayerController;

    //---Interaction UI---
    public GameObject PickUpUIPrefab;
    public Transform PickUpUI;
    public GameObject UIRoot;

    //---Inventory UI---
    public InventoryUI InventoryUI;
    public int InventorySlotCount = 30;

    // 添加字典用于存储Interaction和对应的UI GameObject
    private Dictionary<Interaction, GameObject> interactionUIs = new Dictionary<Interaction, GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        InventoryUI.Init(Inventory.Instance, InventorySlotCount);
    }

    private void OnEnable()
    {
        Interactor.Instance.OnInteractionEnter += ShowInteractionUI;
        Interactor.Instance.OnInteractionExit += HideInteractionUI;
        Interactor.Instance.OnInteractioned += () => { HideInteractionUI(null);};

        PlayerController.OnInventoryToggled += DoInventoryToggle;

    }

    private void OnDisable()
    {
        Interactor.Instance.OnInteractionEnter -= ShowInteractionUI;
        Interactor.Instance.OnInteractionExit -= HideInteractionUI;
        Interactor.Instance.OnInteractioned -= () => { HideInteractionUI(null); };

        PlayerController.OnInventoryToggled -= DoInventoryToggle;
    }

    private void ShowInteractionUI(Interaction interaction)
    {
        switch (interaction.InteractionType)
        {
            case InteractionType.PickUp:
                LoadItemDataUI(interaction);
                PickUpUI.gameObject.SetActive(true);
                break;
            // 可以根据需要添加其他交互类型的UI更新逻辑
            default:
                break;
        }

    }

    private void HideInteractionUI(Interaction interaction)
    {
        PickUpUI.gameObject.SetActive(false);
    }
    private void CreatInteractionUI(Interaction interaction)
    {
       
        if (interactionUIs.ContainsKey(interaction))
            return;

        GameObject interactionUI = Instantiate(PickUpUIPrefab, UIRoot.transform);
        interactionUIs.Add(interaction, interactionUI);
    }

    private void DestroyInteractionUI(Interaction interaction)
    {
        // 示例：销毁并移除对应UI
        if (interactionUIs.TryGetValue(interaction, out var ui))
        {
            Destroy(ui);
            interactionUIs.Remove(interaction);
        }
    }


    private void LoadItemDataUI(Interaction interaction)
    {

        ItemData itemData = interaction.InteractionData.ItemData;

        //确保预制体的子物体顺序正确
        PickUpUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = itemData.ItemName;
        PickUpUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = itemData.Description;
        PickUpUI.GetComponentsInChildren<Image>()[1].sprite = itemData.ItemIcon;
    }

    
    private void DoInventoryToggle()
    {
        InventoryUI.RefreshUI();
        InventoryUI.gameObject.SetActive(!InventoryUI.gameObject.activeSelf);
    }
}
