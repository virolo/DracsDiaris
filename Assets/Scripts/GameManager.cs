using System.Collections.Generic;
using DragonBook;
using DragonBook.Factory;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<DragonBookScriptableObject> _drakes;

    [SerializeField]
    private DragonBookFactoryView _dragonBookFactoryView;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform _movableDrake;

    [SerializeField]
    private bool _enabled;
    
    private void Awake()
    {
        IDragonBookUseCase dragonBookUseCase = new DragonBookUseCase();

        IDragonBookViewModel dragonBookViewModel = new DragonBookViewModel();
        ServiceLocator.ServiceLocator.Instance.Register(dragonBookViewModel);
        
        IDragonBookFactoryViewModel dragonBookFactoryViewModel = new DragonBookFactoryViewModel(dragonBookUseCase, dragonBookViewModel);

        _dragonBookFactoryView.Configure(dragonBookFactoryViewModel);
        
        foreach (DragonBookScriptableObject dragonBookScriptableObject in _drakes)
        {
            dragonBookUseCase.AddCard(dragonBookScriptableObject.Map());
        }
    }

    private void Update()
    {
        if (!_enabled || !Mouse.current.leftButton.isPressed)
        {
            return;
        }
        
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        int boardLayerMask = 1 << LayerMask.NameToLayer("Board");

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, boardLayerMask))
        {
            GameObject hitObject = hit.collider.gameObject;
            _movableDrake.position = hitObject.transform.position;
        }
    }
}