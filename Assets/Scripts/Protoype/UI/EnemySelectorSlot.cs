using UnityEngine;
using UnityEngine.UI;

public class EnemySelectorSlot : MonoBehaviour
{
     [SerializeField] private EnemyData _enemyData;
     
     private Image _selectorImage;

     public EnemyData GetData => _enemyData;


     private void Start()
     {
          _selectorImage = GetComponent<Image>();
     }

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
