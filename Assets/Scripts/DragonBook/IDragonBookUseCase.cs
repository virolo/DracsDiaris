using System;

namespace DragonBook
{
    public interface IDragonBookUseCase
    {
        public event Action<DragonBookData> OnBookDragonAdded;

        public void AddCard(DragonBookData dragonBookData);
        
        public int GenerateBookDragonId();
        
        public void RegisterBookDragon(int viewId, DragonBookData dragonBookData);

        public void UnregisterBookDragons(int viewId);

        public DragonBookData GetDragonBookData(int viewId);
    }
}