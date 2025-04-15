
using UnityEngine;

public class AreaSpeed : MonoBehaviour
{
    [SerializeField] private StatusEffect _effectToApply;

    private void OnTriggerEnter(Collider other)
    {
        var controller = other.GetComponent<StatusEffectsController>();

        if (controller != null)
        {
            controller.ApplyEffect(_effectToApply,this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var controller = other.GetComponent<StatusEffectsController>();

        if (controller != null)
        {
            controller.RemoveEffect(_effectToApply,this);
        }
    }
}
