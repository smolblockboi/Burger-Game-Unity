using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class OrderBell : InteractableObject
{
    public UnityEvent orderRequested;

    public override void Grabbed(Transform holdPoint)
    {
        orderRequested.Invoke();
    }
}
