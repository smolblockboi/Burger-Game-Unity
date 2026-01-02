using UnityEngine;
using UnityEngine.Events;
using System;


public class OrderWindow : MonoBehaviour
{
    public BurgerData burgerOrder;

    private void OnTriggerEnter(Collider other)
    {
        InteractableBurgerPlate burgerPlate = other.GetComponent<InteractableBurgerPlate>();

        if (burgerPlate != null)
        {
            IngredientStacker ingredientStack = burgerPlate.GetComponent<IngredientStacker>();

            if (ingredientStack != null)
            {
                burgerOrder.CheckIngredients(ingredientStack.burgerData);
            }

            burgerPlate.Dropped();
            Destroy(burgerPlate.gameObject);
        }
    }
}
