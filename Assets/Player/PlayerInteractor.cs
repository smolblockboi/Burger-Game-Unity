using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public Rigidbody owner_rb;

    public Camera cam;
    public float maxDistance = 100f;

    public Transform holdPoint;
    private Interactable heldItem;

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

                Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

                if (Physics.Raycast(ray, out hit, maxDistance))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.GetGrabbed(holdPoint);
                        heldItem = interactable;
                        Debug.Log("Picked up " + heldItem.name);
                    } else
                    {
                        Debug.Log("Nothing here");
                    }

                }
            }
        } else
        {
            if (context.canceled)
            {
                Debug.Log("Dropped " + heldItem.name);
                heldItem.GetDropped();
                heldItem = null;
            }
        }
    }


}
