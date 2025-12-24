using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{

    public LayerMask rayGrabMask = LayerMask.GetMask();
    public LayerMask rayInteractMask = LayerMask.GetMask();

    public Camera cam;
    public float maxDistance = 100f;

    public Transform holdPoint;
    private Collider heldItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
                    interactable.Chopped();
                }
            }
        }
    }


}
