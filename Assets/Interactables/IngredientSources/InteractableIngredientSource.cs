using UnityEngine;

public class InteractableIngredientSource : MonoBehaviour, IInteractable
{
	public Transform spawnTransform;
	public GameObject ingredient;

	public void Grabbed(Transform holdPoint)
	{
		Instantiate(ingredient, spawnTransform.position, spawnTransform.rotation);
    }

	public void Dropped()
	{
	
	}

	public void Punched()
	{
	
	}

	public void Chopped()
	{
	
	}

}