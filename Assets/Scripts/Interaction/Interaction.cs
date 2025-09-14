using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



public enum InteractionType
{
    None,
    Pickup,
}

/// <summary>
/// 挂载在场景中的可交互物体上
/// </summary>
public class Interaction : MonoBehaviour
{
    public static List<Interaction> AllInteractions = new List<Interaction>();

    //默认交互类型为拾取
    public InteractionType Type = InteractionType.Pickup;
    public int count = 1;
    public bool Showed = false;
    public float interactionDistance = 2;
    public ItemData ItemData;

    private void OnEnable()
    {
        AllInteractions.Add(this);
    }

    private void OnDisable()
    {
        AllInteractions.Remove(this);
    }

}
