using System;
using UnityEngine;

/// <summary>
/// 挂载在玩家身上，检测与交互物体的距离
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance { get; private set; }

    public event Action<Interaction> OnInteractionEnter;
    public event Action<Interaction> OnInteractionExit;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // 保证只存在一个实例
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
            if (distance <= interaction.interactionDistance && interaction.Showed == false)
            {
                interaction.Showed = true;
                OnInteractionEnter?.Invoke(interaction);
            }
            else if (distance >= interaction.interactionDistance && interaction.Showed)
            {
                interaction.Showed = false;
                OnInteractionExit?.Invoke(interaction);
            }
        }
    }
}