using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 管理PlayerUI
/// </summary>
public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; private set; }
    //public GameObject PickUpUIPrefab;
    public Transform PickUpUI;
    public GameObject UIRoot;



    // 添加字典用于存储Interaction和对应的UI GameObject
    //private Dictionary<Interaction, GameObject> interactionUIs = new Dictionary<Interaction, GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerInteraction.Instance.OnInteractionEnter += ShowInteractionUI;
        PlayerInteraction.Instance.OnInteractionExit += DestroyInteractionUI;
    }

    private void OnDisable()
    {
        PlayerInteraction.Instance.OnInteractionEnter -= ShowInteractionUI;
        PlayerInteraction.Instance.OnInteractionExit -= DestroyInteractionUI;
    }

    private void ShowInteractionUI(Interaction interaction)
    {
        // 防止重复添加
        /*if (interactionUIs.ContainsKey(interaction))
            return;

        GameObject interactionUI = Instantiate(PickUpUIPrefab, UIRoot.transform);
        interactionUIs.Add(interaction, interactionUI);*/

        PickUpUI.gameObject.SetActive(true);
        LoadInteractionUI(interaction, PickUpUI.gameObject);

    }

    private void DestroyInteractionUI(Interaction interaction)
    {
        // 示例：销毁并移除对应UI
        /*if (interactionUIs.TryGetValue(interaction, out var ui))
        {
            Destroy(ui);
            interactionUIs.Remove(interaction);
        }*/
        PickUpUI.gameObject.SetActive(false);
    }


    private void LoadInteractionUI(Interaction interaction,GameObject UI)
    {
        ItemData itemData = interaction.ItemData;

        //确保预制体的子物体顺序正确
        UI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = itemData.itemName;
        UI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = itemData.description;
        UI.GetComponentsInChildren<Image>()[0].sprite = itemData.itemIcon;
    }
}
