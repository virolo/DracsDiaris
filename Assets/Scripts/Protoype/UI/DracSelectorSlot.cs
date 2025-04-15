using UnityEngine;
using UnityEngine.UI;

public class DracSelectorSlot : MonoBehaviour
{

    [SerializeField] private DracData _dracData;

    private Image _selectedImage;
    
    public DracData GetData => _dracData;


    private void Awake()
    {
        _selectedImage = GetComponent<Image>();
    }

    public DracData Select()
    {
        _selectedImage.color = Color.yellow;
        return _dracData;
        
    }

    public void Deselect()
    {
        _selectedImage.color = Color.white;
    }
}
