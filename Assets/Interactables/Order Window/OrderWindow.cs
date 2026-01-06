using UnityEngine;
using UnityEngine.Events;
using System;


public class OrderWindow : MonoBehaviour
{
    public UnityEvent<bool> orderSubmitted;

    public BurgerData burgerData;

    private void OnTriggerEnter(Collider other)
    {
        InteractableBurgerPlate burgerPlate = other.GetComponent<InteractableBurgerPlate>();

        if (burgerPlate != null)
        {
            IngredientStacker ingredientStack = burgerPlate.GetComponent<IngredientStacker>();

            if (ingredientStack != null)
            {
                if (burgerData)
                {
                    orderSubmitted.Invoke(burgerData.CheckIngredients(ingredientStack.burgerData));
                }
            }

            burgerPlate.Dropped();
            Destroy(burgerPlate.gameObject);
        }
    }

    public void OnOrderGenerated(OrderData orderData)
    {
        burgerData = orderData.burgerData;

        Debug.Log("New order generated: " + string.Join(", ", burgerData.ingredients));
    }
}
