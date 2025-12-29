using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public Transform holdPoint;

    public Camera cam;
    public float maxDistance = 100f;

    private Collider heldItem;
    private InteractorAction currentAction = InteractorAction.Punch;

    public void OnInteract(InputAction.CallbackContext context)
    {

        if (heldItem == null)
        {
            if (context.performed)
            {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Grabbed(holdPoint);
                        heldItem = hit.collider;
                    }
                } 
            }
        } else
        {
            if (context.canceled)
            {
                if (heldItem != null)
                {
                    IInteractable interactable = heldItem.GetComponent<IInteractable>();
                    interactable.Dropped();
                    heldItem = null;
                }
            }
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    switch (currentAction)
                    {
                        case InteractorAction.Punch:
                            interactable.Punched();
                            break;
                        case InteractorAction.Chop:
                            interactable.Chopped();
                            break;
                    }
                }
            }
        }
    }

    public void OnActionCycle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentAction = (InteractorAction)(context.ReadValue<float>());

            Debug.Log("Action selected: " + currentAction);
        }
    }

}
