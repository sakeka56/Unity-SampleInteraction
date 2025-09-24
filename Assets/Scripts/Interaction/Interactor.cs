using System;
using UnityEngine;

/// <summary>
/// 挂载在玩家身上，检测与交互物体的距离
/// </summary>
public class Interactor : MonoBehaviour
{
    public static Interactor Instance { get; private set; }

    public PlayerController PlayerController;

    public event Action<Interaction> OnInteractionEnter;
    public event Action<Interaction> OnInteractionExit;
    public event Action OnInteractioned;

    private Interaction currentInteraction;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        // 如需跨场景保留，取消下行注释
        // DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        InteractionDistance();
    }

    private void InteractionDistance()
    {
        foreach (var interaction in Interaction.AllInteractions)
        {
            float distance = Vector3.Distance(transform.position, interaction.transform.position);
            if (distance <= interaction.interactionDistance)
            {
                //Todo:阻碍检测
                if(currentInteraction == null)
                {
                    currentInteraction = interaction;
                    OnInteractionEnter?.Invoke(interaction);
                }
                else
                {
                    if (currentInteraction != interaction && distance < Vector3.Distance(transform.position, currentInteraction.gameObject.transform.position))
                    {
                        currentInteraction = interaction;
                        OnInteractionEnter?.Invoke(interaction);
                    }
                }

            }
            else if (distance >= interaction.interactionDistance)
            {
                if(currentInteraction == interaction)
                {
                    OnInteractionExit?.Invoke(interaction);
                    currentInteraction = null;
                }
            }
        }
    }

    private void OnEnable()
    {
        PlayerController.OnInteractioned += Interact;
    }
    private void OnDisable()
    {
        PlayerController.OnInteractioned -= Interact;
    }
    public void Interact()
    {
        switch (currentInteraction.InteractionType)
        {
            case InteractionType.PickUp:
                Inventory.Instance.AddItem(currentInteraction.InteractionData.ItemData);
                currentInteraction.gameObject.SetActive(false);
                break;
            case InteractionType.Teleport:
                PlayerController.Teleport(currentInteraction.InteractionData.TeleportVec);
                break;
            default:
                Debug.Log("Please Set Interaction Type");
                break;
        }

        OnInteractioned?.Invoke();
    }
}