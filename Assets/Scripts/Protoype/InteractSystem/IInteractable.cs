using UnityEngine;

public interface IInteractable
{
    public void OnSelectInteractable();
    public void OnDeselectInteractable();
    public bool CanInteract();
    public void OnInteract();
}
