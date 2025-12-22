using UnityEngine;

public class InteractableIngredientObject : MonoBehaviour, IInteractable
{
    private Transform targetTransform;

    void Update()
    {
        if (targetTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }
    }

    public void Grabbed(Transform holdPoint)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        targetTransform = holdPoint;

        Debug.Log("Grabbed " + name);
    }

    public void Dropped()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        targetTransform = null;

        Debug.Log("Dropped " + name);
    }

    public void Punched()
    {
        Debug.Log("Punched " + name);
    }

    public void Chopped()
    {
        Debug.Log("Chopped " + name);
    }
}
