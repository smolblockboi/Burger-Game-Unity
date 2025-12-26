using System;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableIngredientObject : MonoBehaviour, IInteractable
{
    private SpringJoint holdJoint;

    public GameObject chopsInto;

    private void FixedUpdate()
    {
        if (holdJoint)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void Grabbed(Transform holdPoint)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            holdJoint = gameObject.AddComponent<SpringJoint>();
            holdJoint.connectedBody = holdPoint.gameObject.GetComponent<Rigidbody>();
            holdJoint.spring = 20f;
            //holdJoint.damper = 1.5f;

            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.linearDamping = 5f;

            rb.useGravity = false;
        }

        Debug.Log("Grabbed " + name);
    }

    public void Dropped()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            Destroy(holdJoint);

            rb.constraints = RigidbodyConstraints.None;
            rb.linearDamping = 0f;

            rb.useGravity = true;
        }

        Debug.Log("Dropped " + name);
    }

    public void Punched()
    {
        Debug.Log("Punched " + name);
    }

    public void Chopped()
    {
        if (chopsInto)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(chopsInto, transform.position, Quaternion.identity);
            }
            Debug.Log("Chopped " + name);
            Destroy(gameObject);
        }
    }
}
