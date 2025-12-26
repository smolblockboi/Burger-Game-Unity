using UnityEngine;

public class InteractableIngredientSource : MonoBehaviour, IInteractable
{
	public Transform spawnTransform;
	public GameObject[] ingredients;

	public void Grabbed(Transform holdPoint)
	{
		for(int i = 0; i < ingredients.Length; i++)
		{
			Instantiate(ingredients[i], spawnTransform.position, spawnTransform.rotation);
		}
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