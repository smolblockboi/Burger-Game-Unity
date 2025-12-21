using UnityEngine;

public class Interactable : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent != null)
        {
            //transform.localPosition = Vector3.Lerp(transform.position, Vector3.zero, Time.fixedDeltaTime);
            //transform.rotation = Quaternion.identity;
        }
    }

    public void GetGrabbed(Transform holdPoint)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        transform.parent = holdPoint;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void GetDropped()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        transform.parent = null;
    }

    public void GetPunched()
    {

    }

    public void GetChopped()
    {

    }
}
