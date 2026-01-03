using UnityEngine;

public class InteractablePlateSource : InteractableObject
{
    [SerializeField] InteractableBurgerPlate burgerPlatePrefab;
    public Transform spawnTransform;

    public override void ShowOutline()
    {
        outline.SetFloat("_Outline_Thickness", outlineSize);
    }

    public override void HideOutline()
    {
        outline.SetFloat("_Outline_Thickness", 0f);
    }

    public override void Grabbed(Transform holdPoint)
    {
        InteractableBurgerPlate plateInstance = Instantiate(burgerPlatePrefab, spawnTransform.position, spawnTransform.rotation);
    }
}
