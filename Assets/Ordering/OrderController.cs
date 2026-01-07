using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderController : MonoBehaviour
{
    public UnityEvent<OrderData> orderGenerated;
    public UnityEvent<int> orderCompleted;

    public List<BurgerData> burgerOptions;

    private List<BurgerData> activeOrders = new();

    private int orderCount = 0;

    public void GenerateOrder()
    {
        orderCount++;

        OrderData newOrder = ScriptableObject.CreateInstance<OrderData>();
        newOrder.burgerData = burgerOptions[Random.Range(0, burgerOptions.Count)];
        newOrder.orderNumber = orderCount;

        activeOrders.Add(newOrder.burgerData);

        orderGenerated.Invoke(newOrder);

    }

    public void OnOrderRequested()
    {
        GenerateOrder();
    }

    public void OnOrderSubmitted(BurgerData submittedBurgerData)
    {
        int search = -1;

        for (int i = 0; i < activeOrders.Count; i++)
        {
            search = i;
            
            BurgerData orderBurgerData = activeOrders[i];

            bool orderMatch = orderBurgerData.CheckIngredients(submittedBurgerData);

            if (orderMatch)
            {
                break;
            }
        }

        if (search >= 0)
        {
            activeOrders.RemoveAt(search);

            orderCompleted.Invoke(search);
        }

        Debug.Log("order controller got data");
    }
}
