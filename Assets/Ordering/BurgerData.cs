using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "NewBurgerData", menuName = "Game Data/Burger Data")]
public class BurgerData : ScriptableObject
{
    public List<ItemData> ingredients = new();

    public void CheckIngredients(BurgerData burgerData)
    {
        Debug.Log("Checked: " + string.Join(", ", burgerData.ingredients));
        Debug.Log("Burger data: " + string.Join(", ", ingredients));

        bool result = burgerData.ingredients.SequenceEqual(ingredients);

        Debug.Log("Burgers matched: " + result);
    }

    public void AddIngredient(ItemData ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void RemoveMostRecentIngredient()
    {
        if (ingredients.Count - 1 > 0)
        {
            ingredients.RemoveAt(ingredients.Count - 1);
        }
    }
}
