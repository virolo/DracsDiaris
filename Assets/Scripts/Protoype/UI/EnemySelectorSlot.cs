using UnityEngine;
using UnityEngine.UI;

public class EnemySelectorSlot : MonoBehaviour
{
     [SerializeField] private EnemyData _enemyData;
     [SerializeField] private Image _selectorImage;

     public EnemyData GetData => _enemyData;
     
     public EnemyData Select()
     {
          _selectorImage.color = Color.yellow;
          return _enemyData;
     }

     public void Deselect()
     {
          _selectorImage.color = Color.white;
     }
}
