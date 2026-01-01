using UnityEngine;

public class InteractableFridgeDoor : InteractableObject
{
    private bool isOpened = false;
    private Vector3 openVector = new Vector3(0f, -75f, 0f);
    public Transform hinge;

    public override void Grabbed(Transform holdPoint)
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

}
