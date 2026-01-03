using UnityEngine;
using UnityEngine.Events;

public class InteractableFridgeDoor : InteractableObject
{
    public int doorIndex;

    private bool isOpened = false;
    private Vector3 openVector = new Vector3(0f, -75f, 0f);

    public Transform hinge;

    public UnityEvent<int> doorClosed;

    public override void Grabbed(Transform holdPoint)
    {
        if (isOpened)
        {
            hinge.localRotation = Quaternion.Euler(Vector3.zero);
            isOpened = false;
            
            doorClosed.Invoke(doorIndex);
        }
        else
        {
            hinge.localRotation = Quaternion.Euler(openVector);
            isOpened = true;
        }
    }

}
