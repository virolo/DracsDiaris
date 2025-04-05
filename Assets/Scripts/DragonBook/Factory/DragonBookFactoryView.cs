using UnityEngine;

namespace DragonBook.Factory
{
    public class DragonBookFactoryView : MonoBehaviour
    {
        [SerializeField]
        private DragonBookView _dragonBookView;

        [SerializeField]
        private Transform _parent;
        
        private IDragonBookFactoryViewModel _factoryViewModel;

        public void Configure(IDragonBookFactoryViewModel factoryViewModel)
        {
            _factoryViewModel = factoryViewModel;
            _factoryViewModel.OnInstantiate += InstantiateDragonBook;
        }

        private void OnDestroy()
        {
            _factoryViewModel.OnInstantiate -= InstantiateDragonBook;
        }

        private void InstantiateDragonBook(int viewId)
        {
            DragonBookView dragonBookView = Instantiate(_dragonBookView, _parent);
            dragonBookView.Configure(viewId);
        }
    }
}