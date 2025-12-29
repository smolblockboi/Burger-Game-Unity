using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    private SpringJoint grabJoint;

    private bool _isGrabbed; // internal
    public bool IsGrabbed { get { return _isGrabbed; } } // Read-only

    public virtual void Grabbed(Transform holdPoint)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            grabJoint = gameObject.AddComponent<SpringJoint>();
            grabJoint.connectedBody = holdPoint.gameObject.GetComponent<Rigidbody>();
            grabJoint.spring = 20f;
            //grabJoint.damper = 1.5f;

            rb.linearDamping = 5f;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = false;
            rb.useGravity = false;

            _isGrabbed = true;
        }

        Debug.Log("Grabbed " + name);
    }

    public virtual void Dropped()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            Destroy(grabJoint);

            rb.linearDamping = 0f;
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;

            _isGrabbed = false;
        }

        Debug.Log("Dropped " + name);
    }

    public virtual void Punched()
	{
	    Debug.Log("Punched " + gameObject.name);
	}

	public virtual void Chopped()
	{
	    Debug.Log("Chopped " + gameObject.name);
	}

}