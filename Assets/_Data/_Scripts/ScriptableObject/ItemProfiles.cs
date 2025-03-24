using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfiles", menuName = "ScriptableObjects/ItemProfiles", order = 1)]
public class ItemProfiles : ScriptableObject
{
    public ItemType ItemType;
    public ItemName ItemName;
    public Sprite ItemIcon;
    public int MaxStack;
}
