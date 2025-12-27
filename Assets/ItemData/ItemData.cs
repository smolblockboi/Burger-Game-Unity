using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Game Data/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public ItemState itemState;
    public ItemType itemType;

    public Mesh itemMesh;

    public ItemData cooksInto;
    public ItemData burnsInto;

    public ItemData chopsInto;
    public ItemData punchesInto;
}
