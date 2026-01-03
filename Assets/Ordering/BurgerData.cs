using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "NewBurgerData", menuName = "Game Data/Burger Data")]
public class BurgerData : ScriptableObject
{
    public List<ItemData> ingredients = new();

    public bool CheckIngredients(BurgerData burgerData)
    {
        Debug.Log("Checked: " + string.Join(", ", burgerData.ingredients));
        Debug.Log("Burger data: " + string.Join(", ", ingredients));

        bool result = burgerData.ingredients.SequenceEqual(ingredients);

        return result;
    }

    public void AddIngredient(ItemData ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void RemoveMostRecentIngredient()
    {
        if (ingredients.Count > 0)
        {
            ingredients.RemoveAt(ingredients.Count - 1);
        }
    }
}
