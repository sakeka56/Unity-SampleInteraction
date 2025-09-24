using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 挂载在场景中的可交互物体上
/// </summary>
/// 

public enum InteractionType
{
    PickUp,
    Teleport,
    Talk,
    Open,
    Examine,
    Use
}

public class Interaction : MonoBehaviour
{
    public static List<Interaction> AllInteractions = new List<Interaction>();
    public float interactionDistance = 2;
    public InteractionType InteractionType = InteractionType.PickUp;
    public InteractionData InteractionData;

    private void OnEnable()
    {
        AllInteractions.Add(this);
    }

    private void OnDisable()
    {
        AllInteractions.Remove(this);
    }

}
