using UnityEngine;

public class HandhledCameraShake : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Animator _targetAnimator;

    public void TriggerAnimation()
    {
        _targetAnimator.SetTrigger("Shake");
    }
}
