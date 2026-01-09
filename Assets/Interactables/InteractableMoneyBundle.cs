using UnityEngine;
using UnityEngine.Events;

public class InteractableMoneyBundle : InteractableObject
{
    public int moneyValue = 1;

    public override void Grabbed(Transform holdPoint)
    {
        Debug.Log($"Got {moneyValue} money!");

        GameController.Instance.ChangeMoney(moneyValue);

        Destroy(gameObject);
    }
}
