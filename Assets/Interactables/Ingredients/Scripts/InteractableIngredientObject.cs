using UnityEngine;

public class InteractableIngredientObject : InteractableObject
{
    public InteractableIngredientObject itemPrefab;

    public ItemData itemData;

    public float currentCookTime = 0f;
    public float targetCookTime = 3f;

    private void Start()
    {
        ChangeData(itemData);
    }

    private void Update()
    {
        if (currentCookTime >= targetCookTime)
        {
            currentCookTime = 0f;

            if (itemData.burnsInto != null)
            {
                ChangeData(itemData.burnsInto);
            }
            else if (itemData.cooksInto != null)
            {
                ChangeData(itemData.cooksInto);
            }

        }
    }

    public void ChangeData(ItemData newItemData)
    {
        itemData = newItemData;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = itemData.itemMesh;

        MeshCollider collider = GetComponent<MeshCollider>();
        collider.sharedMesh = itemData.itemMesh;
    }

    public override void Punched()
    {
        Debug.Log("Punched " + name);
        
        if (itemData.punchesInto != null)
        {
            Debug.Log("Punched into " + itemData.chopsInto.itemName);
        }
    }

    public override void Chopped()
    {
        if (itemData.chopsInto != null)
        {
            Debug.Log("Chopped " + name);

            for (int i = 0; i < 2; i++)
            {
                InteractableIngredientObject itemInstance = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                itemInstance.itemData = itemData.chopsInto;
            }
            
            Debug.Log("Chopped into " + itemData.chopsInto.itemName);

            Destroy(gameObject);
        }
    }
}
