using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class CookingPan : MonoBehaviour
{
    public Transform panRoot;

    private InteractableIngredientObject currentlyCooking;

    private void Update()
    {
        if (currentlyCooking != null)
        {
            currentlyCooking.currentCookTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<InteractableIngredientObject>(out InteractableIngredientObject ingredient))
        {

            ingredient.transform.position = panRoot.transform.position;
            ingredient.Dropped();

            currentlyCooking = ingredient;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<InteractableIngredientObject>(out InteractableIngredientObject ingredient))
        {
            if(currentlyCooking == ingredient)
            {
                currentlyCooking = null;
                
                Debug.Log("Stopped cooking " + ingredient.itemData.itemName);
            }


        }
    }
}
