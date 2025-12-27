using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class CookingPan : MonoBehaviour
{
    public Transform panRoot;

    private bool justCooked;
    private float cooldown = 0.5f;

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

            if (!justCooked)
            {
                CookItem(ingredient);
            }

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

    private void CookItem(InteractableIngredientObject itemObject)
    {
        Debug.Log("Started cooking " + itemObject.itemData.itemName);

        StartCoroutine(CookTimer(3.0f, itemObject));
    }

    IEnumerator CookTimer(float duration, InteractableIngredientObject itemObject)
    {
        yield return new WaitForSeconds(duration);

        StartCooldown();

        if (itemObject.itemData.burnsInto != null)
        {
            itemObject.ChangeData(itemObject.itemData.burnsInto);
        }
        else if (itemObject.itemData.cooksInto != null)
        {
            itemObject.ChangeData(itemObject.itemData.cooksInto);
        }


    }

    private void StartCooldown()
    {
        cooldown = 0.5f;
        justCooked = true;

        Debug.Log("Cooldown started");
    }
}
