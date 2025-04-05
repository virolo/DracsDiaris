using System.Collections.Generic;
using DragonBook;
using DragonBook.Factory;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<DragonBookScriptableObject> _drakes;

    [SerializeField]
    private DragonBookFactoryView _dragonBookFactoryView;
   
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
}