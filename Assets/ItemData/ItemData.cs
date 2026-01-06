using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Game Data/Item Data")]
public class ItemData : ScriptableObject
{
    public string internalID;
    public string itemName;

    public ItemState itemState;
    public ItemType itemType;

    public Mesh itemMesh;
    public Texture2D itemIcon;

    public ItemData cooksInto;
    public ItemData burnsInto;

    public ItemData chopsInto;
    public ItemData punchesInto;
}
