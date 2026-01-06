using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class OrderBell : InteractableObject
{
    public UnityEvent<OrderData> orderGenerated;

    public List<BurgerData> burgerOptions;

    private int orderCount = 0;

    public override void Grabbed(Transform holdPoint)
    {
        orderCount++;

        OrderData newOrder = ScriptableObject.CreateInstance<OrderData>();
        newOrder.burgerData = burgerOptions[Random.Range(0, burgerOptions.Count)];
        newOrder.orderNumber = orderCount;

        orderGenerated.Invoke(newOrder);
    }
}
