using TMPro;
using UnityEngine;


public class UIBurgerOrder : MonoBehaviour
{
    [SerializeField] private OrderData slipOrderData;
    public TextMeshProUGUI orderText;

    public void OnOrderGenerated(OrderData orderData)
    {
        slipOrderData = orderData;
        orderText.text = string.Join(", ", orderData.burgerData.ingredients);
    }

    public void OnOrderSubmitted(bool orderMatches)
    {
        if (orderMatches)
        {
            orderText.text = "Ring bell for next order!";
        }
    }
}
