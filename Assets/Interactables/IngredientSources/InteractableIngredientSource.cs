using UnityEngine;

public class InteractableIngredientSource : InteractableObject
{
	public InteractableIngredientObject itemPrefab;
	public ItemData[] ingredientDatas;

	public Transform spawnTransform;

	public override void Grabbed(Transform holdPoint)
	{
		for(int i = 0; i < ingredientDatas.Length; i++)
		{
			InteractableIngredientObject itemInstance = Instantiate(itemPrefab, spawnTransform.position, spawnTransform.rotation);
			itemInstance.itemData = ingredientDatas[i];
		}
    }

}