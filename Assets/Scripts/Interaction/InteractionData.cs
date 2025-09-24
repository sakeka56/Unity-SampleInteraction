using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractionData", menuName = "Interaction/Interaction Data")]
public class InteractionData : ScriptableObject
{
    public ItemData ItemData;
    public Vector3 TeleportVec;
}
