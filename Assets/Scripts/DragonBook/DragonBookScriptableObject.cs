using UnityEngine;

namespace DragonBook
{
    [CreateAssetMenu(menuName = "DracsDiaris/Create DragonBookScriptableObject", fileName = "DragonBookScriptableObject", order = 0)]
    public class DragonBookScriptableObject : ScriptableObject
    {
        [SerializeField]
        private Color _color;

        public DragonBookData Map()
        {
            return new DragonBookData(_color);
        }
    }
}