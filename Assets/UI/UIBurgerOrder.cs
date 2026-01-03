using TMPro;
using UnityEngine;


public class UIBurgerOrder : MonoBehaviour
{
    public TextMeshProUGUI orderText;

    public void OnOrderOnOrderGenerated(BurgerData burgerData)
    {
        orderText.text = string.Join(", ", burgerData.ingredients);
    }

    public void OnOrderSubmitted(bool orderMatches)
    {
        if (orderMatches)
        {
            orderText.text = "Ring bell for next order!";
        }
    }
}
