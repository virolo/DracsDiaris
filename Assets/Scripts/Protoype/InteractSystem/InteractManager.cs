using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;

    
    private IInteractable _selectedInteractable;

    private void Update()
    {
        UpdateMousePosition();

        if (_inputManager.MouseDown && _selectedInteractable != null)
        {
            _selectedInteractable.OnInteract();
        }
    }

    private void UpdateMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(_inputManager.MousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit , 100.0f))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (_selectedInteractable != null)
                {
                    _selectedInteractable.OnDeselectInteractable();
                }
                
                _selectedInteractable = interactable;
                _selectedInteractable.OnSelectInteractable();
            }
            else
            {
            
                if (_selectedInteractable != null)
                {
                    _selectedInteractable.OnDeselectInteractable();
                    _selectedInteractable = null;
                }
            }
        }
        else
        {
            if (_selectedInteractable != null)
            {
                _selectedInteractable.OnDeselectInteractable();
                _selectedInteractable = null;
            }
        }
    }
}