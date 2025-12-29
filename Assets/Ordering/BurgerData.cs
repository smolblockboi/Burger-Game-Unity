using UnityEngine;

[CreateAssetMenu(fileName = "NewBurgerData", menuName = "Game Data/Burger Data")]
public class BurgerData : ScriptableObject
{
    public ItemData[] ingredients;
}
