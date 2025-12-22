using UnityEngine;

public interface IInteractable
{
    void Grabbed(Transform holdPoint);
    void Dropped();
    void Punched();
    void Chopped();

}
