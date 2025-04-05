using System;

namespace DragonBook.Factory
{
    public class DragonBookFactoryViewModel : IDragonBookFactoryViewModel
    {
        public event Action<int> OnInstantiate;
        
        private readonly IDragonBookUseCase _dragonBookUseCase;
        
        private readonly IDragonBookViewModel _dragonBookViewModel;

        public DragonBookFactoryViewModel(IDragonBookUseCase dragonBookUseCase, IDragonBookViewModel dragonBookViewModel)
        {
            _dragonBookUseCase = dragonBookUseCase;
            _dragonBookViewModel = dragonBookViewModel;
            _dragonBookUseCase.OnBookDragonAdded += InstantiateBook;
        }

        private void InstantiateBook(DragonBookData dragonBookData)
        {
            int viewId = _dragonBookUseCase.GenerateBookDragonId();
            OnInstantiate?.Invoke(viewId);
            _dragonBookUseCase.RegisterBookDragon(viewId, dragonBookData);
            _dragonBookViewModel.Initialize(viewId, dragonBookData);
        }
    }
}