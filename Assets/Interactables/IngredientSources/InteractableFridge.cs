using UnityEngine;

public class InteractableFridge : MonoBehaviour, IInteractable
{
    public Transform topDoor;
    public Transform bottomDoor;

    public void Grabbed(CharacterController characterController, Transform holdPoint = null)
    {
        topDoor.Rotate(new Vector3(5f, 0f, 0f));
        bottomDoor.Rotate(new Vector3(-5f, 0f, 0f));
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
