using UnityEngine;

public class InteractableBurgerPlate : InteractableObject
{
    
    public override void Punched()
    {
        IngredientStacker ingredientStack = GetComponent<IngredientStacker>();

        if (ingredientStack != null)
        {
            ingredientStack.RemoveIngredient();
        }
        
    }
}
