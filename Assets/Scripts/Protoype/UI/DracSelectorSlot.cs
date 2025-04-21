using UnityEngine;
using UnityEngine.UI;

public class DracSelectorSlot : MonoBehaviour
{

    [SerializeField] private DracData _dracData;
    [SerializeField] private Scrollbar _timeBar;

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

    public void UpdateTimerBar(float percent)
    {
        _timeBar.size = percent;
    }

    public void Deselect()
    {
        _selectedImage.color = Color.white;
    }

    public void Activate(float time)
    {
        gameObject.SetActive(true);
        _timeBar.size = time / _dracData._time;
    }
}
