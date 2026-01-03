using UnityEngine;

public class InteractableFridge : MonoBehaviour
{
    public InteractableIngredientObject itemPrefab;

    public LayerMask overlapLayerMask;

    public Transform topShelfSpawnpoint;
    public int topShelfItemLimit;
    [SerializeField] ItemData topShelfIngredient;
    [SerializeField] BoxCollider topShelfArea;

    public Transform[] bottomShelfSpawnpoint;
    public int bottomShelfItemLimit;
    [SerializeField] ItemData[] bottomShelfIngredients;
    [SerializeField] BoxCollider[] bottomShelfAreas;



    public void OnDoorClosed(int index)
    {
        if (index == 0) // bottom door
        {
            Debug.Log("bottom door closed");

            for (int i = 0; i < bottomShelfAreas.Length; i++)
            {
                BoxCollider shelfArea = bottomShelfAreas[i];

                Collider[] hitColliders = Physics.OverlapBox(shelfArea.transform.position, shelfArea.size * 0.5f, Quaternion.identity, overlapLayerMask);

                Debug.Log(hitColliders.Length);

                if (hitColliders.Length < bottomShelfItemLimit)
                {
                    InteractableIngredientObject itemInstance = Instantiate(itemPrefab, shelfArea.transform.position, shelfArea.transform.rotation);
                    itemInstance.itemData = bottomShelfIngredients[i];
                }
            }
        }
        else if (index == 1) // top door
        {
            Debug.Log("top door closed");

            Collider[] hitColliders = Physics.OverlapBox(topShelfArea.transform.position, topShelfArea.size * 0.5f, Quaternion.identity, overlapLayerMask);

            if (hitColliders.Length < topShelfItemLimit)
            {
                InteractableIngredientObject itemInstance = Instantiate(itemPrefab, topShelfArea.transform.position, topShelfArea.transform.rotation);
                itemInstance.itemData = topShelfIngredient;
            }

        }
    }
}
