using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguradorDrac : MonoBehaviour
{
    [SerializeField] private DracData _selectedDracData;
    
    [SerializeField] private CustomSlider _rangeSlider;
    [SerializeField] private CustomSlider _damageSlider;
    [SerializeField] private CustomSlider _attackSpeedSlider;

    [SerializeField] private TargetingModeDropdown _targetingModeDropdown;

    [SerializeField] private Image _titleImage;


    private void Start()
    {
        _rangeSlider.OnValueChanged += SaveRange;
        _damageSlider.OnValueChanged += SaveDamage;
        _attackSpeedSlider.OnValueChanged += SaveAttackSpeed;
        _targetingModeDropdown.OnValueChanged += SaveTargetingMode;

        _titleImage.color = _selectedDracData._color;
        
        ChangeSelectedDrac(_selectedDracData);
    }

    
    private void SaveTargetingMode(TargetingMode obj)
    {
        _selectedDracData._targetingMode = obj;
    }
    private void SaveDamage(float obj)
    {
        _selectedDracData._damage = obj;
    }

    private void SaveAttackSpeed(float obj)
    {
        _selectedDracData._attackSpeed = obj;
    }

    private void SaveRange(float obj)
    {
        _selectedDracData._radius = obj;
    }

    public void ChangeSelectedDrac(DracData selectorSlot)
    {
        _selectedDracData = selectorSlot;
        
        _rangeSlider.SetValue(_selectedDracData._radius);
        _damageSlider.SetValue(_selectedDracData._damage);
        _attackSpeedSlider.SetValue(_selectedDracData._attackSpeed);
        
        _targetingModeDropdown.SetTargetingMode(_selectedDracData._targetingMode);
        
        _titleImage.color = _selectedDracData._color;
    }
    
}
