using System;
using UnityEngine;


[RequireComponent(typeof(Outline))]
public class DracSlot : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline _outline;

    
    public event Action<DracSlot> OnSelectedSlot;
   
    private void Start()
    {
        OnDeselectInteractable();
        _outline.OutlineColor = Color.yellow;
    }

    public void OnSelectInteractable()
    {
        _outline.OutlineWidth = 2.5f;
    }

    public void OnDeselectInteractable()
    {
        
        
        _outline.OutlineWidth = 0.0f;
    }
    
    public bool CanInteract()
    {
        return true;
    }

    public void OnInteract()
    {
        OnSelectedSlot?.Invoke(this);
    }
}