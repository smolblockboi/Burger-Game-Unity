using UnityEngine;

public class IngredientStacker : MonoBehaviour
{
    public CapsuleCollider triggerCollider;
    public float triggerGrowth = 0.1f;

    public Transform ingredientStackRoot;
    private int baseTriggerSize = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<InteractableIngredientObject>(out InteractableIngredientObject ingredient))
        {
            ingredient.Dropped();

            Collider coll = ingredient.GetComponent<Collider>();
            coll.enabled = false;

            Rigidbody rb = ingredient.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            int ingredientCount = ingredientStackRoot.childCount;

            ingredient.transform.SetParent(ingredientStackRoot, false);
            
            if (ingredientCount > 0)
            {
                Transform lastIngredient = ingredientStackRoot.GetChild(ingredientCount - 1);
                MeshFilter lastMesh = lastIngredient.GetComponent<MeshFilter>();
                
                if (lastMesh != null)
                {
                    Bounds lastBounds = lastMesh.mesh.bounds;
                    float hieghtOffset = lastIngredient.localPosition.y + lastBounds.center.y + lastBounds.extents.y;

                    ingredient.transform.SetLocalPositionAndRotation(new Vector3(0, hieghtOffset, 0), Quaternion.identity);
                    AddIngredient();
                }
                else
                {
                    ingredient.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                    AddIngredient();
                }
            }
            else
            {
                ingredient.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                AddIngredient();
            }
        }
    }

    private void AddIngredient()
    {
        int stackCount = ingredientStackRoot.childCount;

        if (stackCount > baseTriggerSize)
        {
            triggerCollider.height += triggerGrowth;
            triggerCollider.center += new Vector3(0, triggerGrowth, 0f);
        }
    }

    public void RemoveIngredient()
    {
        int ingredientCount = ingredientStackRoot.childCount;

        if (ingredientCount <= 0)
        {
            return;
        }

        Transform lastIngredient = ingredientStackRoot.GetChild(ingredientCount - 1);

        if (ingredientCount > baseTriggerSize)
        {
            triggerCollider.height -= triggerGrowth;
            triggerCollider.center -= new Vector3(0, triggerGrowth, 0f);
        }

        Destroy(lastIngredient.gameObject);

    }

}
