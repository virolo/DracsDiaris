using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragonBook
{
    public class DragonBookViewModel : IDragonBookViewModel
    {
        public ReactiveProperty<Vector3> SnapPosition { get; }

        public event Action<int, Color> OnInitialize;

        public DragonBookViewModel()
        {
            SnapPosition = new ReactiveProperty<Vector3>();
        }
        
        public void SetInitialPosition(Vector3 transformPosition)
        {
        
        }

        public void OnBeginDrag(int id, PointerEventData eventData)
        {
        
        }

        public void OnEndDrag(int id, PointerEventData eventData)
        {
        
        }

        public void Initialize(int viewId, DragonBookData dragonBookData)
        {
            OnInitialize?.Invoke(viewId, dragonBookData.Color);
        }

        public void Dispose()
        {
        
        }
    }
}