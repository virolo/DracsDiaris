using UnityEngine;

namespace DragonBook
{
    public readonly struct DragonBookData
    {
        public Color Color { get; }

        public DragonBookData(Color color)
        {
            Color = color;
        }
    }
}