using System;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableIngredientObject : MonoBehaviour, IInteractable
{
    private Transform targetTransform;
    private Rigidbody rb;

    private FixedJoint grabJoint;

    public GameObject chopsInto;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 distanceFromTarget = targetTransform.position - transform.position;

            Debug.Log(distanceFromTarget.sqrMagnitude);
            if(distanceFromTarget.sqrMagnitude > 0.5f)
            {
                rb.AddForce((distanceFromTarget.normalized * distanceFromTarget.sqrMagnitude), ForceMode.Acceleration);
            }
            //transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }
    }

    public void Grabbed(CharacterController characterController, Transform holdPoint)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            //rb.isKinematic = true;
        }

        grabJoint = characterController.gameObject.AddComponent<FixedJoint>();
        grabJoint.connectedBody = rb;

        //targetTransform = holdPoint;

        Debug.Log("Grabbed " + name);
    }

    public void Dropped()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = true;
            //rb.isKinematic = false;
        }

        targetTransform = null;

        Destroy(grabJoint);

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
