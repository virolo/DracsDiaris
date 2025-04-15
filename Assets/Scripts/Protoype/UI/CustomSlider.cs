using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _amount;

    public event Action<float> OnValueChanged;

    public void UpdateValue(float amount)
    {
        _amount.text = amount.ToString("F2");
        OnValueChanged?.Invoke(amount);
    }

    public void SetValue(float amount)
    {
        _slider.value = amount;
    }

    public void SetValue(string amount)
    {
        _slider.value = float.Parse(amount);
    }
}