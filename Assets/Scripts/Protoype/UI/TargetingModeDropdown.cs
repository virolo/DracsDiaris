using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TargetingModeDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;


    public event Action<TargetingMode> OnValueChanged;
    private void Start()
    {
        _dropdown.ClearOptions();
        List<string> enumNames = System.Enum.GetNames(typeof(TargetingMode)).ToList();
        _dropdown.AddOptions(enumNames);
    }

    public void DropdownValueChanged(int index)
    {
        OnValueChanged?.Invoke((TargetingMode)index);
    }
    
    public void SetTargetingMode(TargetingMode targetingMode)
    {
        
        int index = System.Array.IndexOf(System.Enum.GetValues(typeof(TargetingMode)), targetingMode);
    
        if (index >= 0)
        {
            _dropdown.value = index;
            _dropdown.RefreshShownValue(); 
        }   
    }
    
}
