using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    private SpringJoint grabJoint;

    private Renderer objectRenderer;
    [HideInInspector] public Material outline;
    public float outlineSize { get; private set; } = 0.05f;

    private bool _isGrabbed; // internal
    public bool IsGrabbed { get { return _isGrabbed; } } // Read-only

    private RigidbodyConstraints grabbedConstraints = RigidbodyConstraints.None;

    public virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            if (objectRenderer.materials.Length > 1)
            {
                outline = objectRenderer.materials[1];
                HideOutline();
            }
        }

    }
    public virtual void ShowOutline()
    {
        outline.SetFloat("_Outline_Thickness", outlineSize);
    }

    public virtual void HideOutline()
    {
        outline.SetFloat("_Outline_Thickness", 0f);
    }

    public virtual void Grabbed(Transform holdPoint)
    {
        if (!grabJoint)
        {
            grabJoint = gameObject.AddComponent<SpringJoint>();
            grabJoint.connectedBody = holdPoint.gameObject.GetComponent<Rigidbody>();
            grabJoint.spring = 20f;
            //grabJoint.damper = 1.5f;
        }

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearDamping = 5f;
            grabbedConstraints = rb.constraints;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = false;
            rb.useGravity = false;

            _isGrabbed = true;
        }

        Debug.Log("Grabbed " + name);
    }

    public virtual void Dropped()
    {
        if (grabJoint)
        {
            Destroy(grabJoint);
        }

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearDamping = 0f;
            rb.constraints = grabbedConstraints;
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