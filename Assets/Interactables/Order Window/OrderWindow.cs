using UnityEngine;
using UnityEngine.Events;
using System;


public class OrderWindow : MonoBehaviour
{
    [SerializeField] InteractableMoneyBundle moneyBundlePrefab;
    [SerializeField] Transform moneySpawnTransform;

    public UnityEvent<BurgerData> orderSubmitted;

    private void OnTriggerEnter(Collider other)
    {
        InteractableBurgerPlate burgerPlate = other.GetComponent<InteractableBurgerPlate>();

        if (burgerPlate != null)
        {
            IngredientStacker ingredientStack = burgerPlate.GetComponent<IngredientStacker>();

            if (ingredientStack != null)
            {
                orderSubmitted.Invoke(ingredientStack.burgerData);
            }

            SpawnMoney(ingredientStack.burgerData.ingredients.Count);

            burgerPlate.Dropped();
            Destroy(burgerPlate.gameObject);


        }
    }

    private void SpawnMoney(int amount)
    {
        InteractableMoneyBundle moneyInstance = Instantiate(moneyBundlePrefab, moneySpawnTransform.position, moneySpawnTransform.rotation);
        moneyInstance.moneyValue = amount;
    }

}
