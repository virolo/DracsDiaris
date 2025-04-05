using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragonBook
{
    public interface IDragonBookViewModel : IDisposable
    {
        public event Action<int, Color> OnInitialize;

        public ReactiveProperty<Vector3> SnapPosition { get; }

        public void SetInitialPosition(Vector3 transformPosition);
    
        public void OnBeginDrag(int id, PointerEventData eventData);
    
        public void OnEndDrag(int id, PointerEventData eventData);

        public void Initialize(int viewId, DragonBookData dragonBookData);
    }
}