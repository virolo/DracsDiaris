using System;

namespace DragonBook.Factory
{
    public interface IDragonBookFactoryViewModel
    {
        public event Action<int> OnInstantiate;
    }
}