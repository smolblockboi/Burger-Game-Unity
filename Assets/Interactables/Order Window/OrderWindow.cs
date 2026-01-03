using UnityEngine;
using UnityEngine.Events;
using System;


public class OrderWindow : MonoBehaviour
{
    public UnityEvent<bool> orderSubmitted;

    public BurgerData burgerOrder;

    private void OnTriggerEnter(Collider other)
    {
        InteractableBurgerPlate burgerPlate = other.GetComponent<InteractableBurgerPlate>();

        if (burgerPlate != null)
        {
            IngredientStacker ingredientStack = burgerPlate.GetComponent<IngredientStacker>();

            if (ingredientStack != null)
            {
                if (burgerOrder)
                {
                    orderSubmitted.Invoke(burgerOrder.CheckIngredients(ingredientStack.burgerData));
                }
            }

            burgerPlate.Dropped();
            Destroy(burgerPlate.gameObject);
        }
    }

    public void OnOrderGenerated(BurgerData burgerData)
    {
        burgerOrder = burgerData;

        Debug.Log("New order generated: " + string.Join(", ", burgerOrder.ingredients));
    }
}
