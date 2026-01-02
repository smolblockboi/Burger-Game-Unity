using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class OrderBell : InteractableObject
{
    public UnityEvent<BurgerData> orderGenerated;

    public List<BurgerData> burgerOptions;

    public override void Grabbed(Transform holdPoint)
    {
        BurgerData newOrder = burgerOptions[Random.Range(0, burgerOptions.Count)];

        orderGenerated.Invoke(newOrder);
    }

    public void OnOrderGenerated(BurgerData burgerData)
    {
        Debug.Log("New order generated: " + string.Join(", ", burgerData.ingredients));
    }
}
