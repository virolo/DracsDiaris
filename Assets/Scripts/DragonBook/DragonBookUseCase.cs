using System;
using System.Collections.Generic;

namespace DragonBook
{
    public class DragonBookUseCase : IDragonBookUseCase
    {
        public event Action<DragonBookData> OnBookDragonAdded;

        private int _bookDragonIdGenerator;

        private readonly Dictionary<int, DragonBookData> _viewIdToDragonBookData = new ();

        public void AddCard(DragonBookData data)
        {
            OnBookDragonAdded?.Invoke(data);
        }

        public int GenerateBookDragonId()
        {
            return _bookDragonIdGenerator++;
        }

        public void RegisterBookDragon(int viewId, DragonBookData data)
        {
            _viewIdToDragonBookData.Add(viewId, data);
        }

        public void UnregisterBookDragons(int viewId)
        {
            _viewIdToDragonBookData.Remove(viewId);
        }

        public DragonBookData GetDragonBookData(int viewId)
        {
            return _viewIdToDragonBookData[viewId];
        }
    }
}