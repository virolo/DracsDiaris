using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace DragonBook
{
    public class DragonBookView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private Image _draggableImage;

        [SerializeField]
        private Transform _draggableObject;
        
        private Transform _transform;
        
        private IDragonBookViewModel _viewModel;

        private int _viewId;

        private Vector3 _snapPosition;

        public void Awake()
        {
            _viewModel = ServiceLocator.ServiceLocator.Instance.Resolve<IDragonBookViewModel>();
            _transform = transform;
        }
    
        public void Configure(int id)
        {
            _viewId = id;
            _viewModel.SnapPosition.OnValueChange += SetPosition;
            _viewModel.OnInitialize += SetData;
            _viewModel.SetInitialPosition(_transform.position);
        }

        private void OnDestroy()
        {
            _viewModel.SnapPosition.OnValueChange -= SetPosition;
            _viewModel.OnInitialize -= SetData;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _viewModel.OnBeginDrag(_viewId, eventData);
            _draggableImage.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _draggableObject.position = Mouse.current.position.ReadValue();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _viewModel.OnEndDrag(_viewId, eventData);
            _draggableObject.position = _transform.position;
            _draggableImage.raycastTarget = true;
        }

        private void SetPosition(Vector3 position)
        {
            _snapPosition = position;
            _transform.position = position;
        }

        private void SetData(int viewId, Color color)
        {
            if (_viewId != viewId)
            {
                return;
            }

            _draggableImage.color = color;
        }
    }
}
