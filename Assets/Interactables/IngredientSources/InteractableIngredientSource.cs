using UnityEngine;

public class InteractableIngredientSource : MonoBehaviour, IInteractable
{
	public InteractableIngredientObject itemPrefab;
	public ItemData[] ingredientDatas;

	public Transform spawnTransform;

	public void Grabbed(Transform holdPoint)
	{
		for(int i = 0; i < ingredientDatas.Length; i++)
		{
			InteractableIngredientObject itemInstance = Instantiate(itemPrefab, spawnTransform.position, spawnTransform.rotation);
			itemInstance.itemData = ingredientDatas[i];
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