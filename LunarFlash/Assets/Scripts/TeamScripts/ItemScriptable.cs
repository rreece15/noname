using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemDataScriptableObject", order=0)]

public class ItemScriptable : ScriptableObject
{
    [Header("ItemValues")]
    public string itemName;
    public Texture itemImage;
}
public enum ItemType
{
    wearable,
    consumable

}