using UnityEngine;

public interface IInteractable
{
    void Grabbed(CharacterController characterController, Transform holdPoint);
    void Dropped();
    void Punched();
    void Chopped();

}
