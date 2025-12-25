using UnityEngine;

public class InteractableFridgeDoor : MonoBehaviour, IInteractable
{
    private bool isOpened = false;
    private Vector3 openVector = new Vector3(0f, -75f, 0f);
    public Transform hinge;

    public void Grabbed(CharacterController characterController, Transform holdPoint = null)
    {
        if (isOpened)
        {
            hinge.localRotation = Quaternion.Euler(Vector3.zero);
            isOpened = false;
        }
        else
        {
            hinge.localRotation = Quaternion.Euler(openVector);
            isOpened = true;
        }
    }

    public void Dropped()
    {

    }

    public void Punched()
    {

    }

    public void Chopped()
    {

    }
}
