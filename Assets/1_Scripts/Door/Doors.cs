using UnityEngine;

public class Doors : MonoBehaviour, IInteractable
{
    [Header("Interaction Settings"), Space(5)]
    [SerializeField] private Animator[] _animators;
    [SerializeField] private GameObject uiInteract;

    public bool _isOpen = false;

    public TriggerSound triggerSound;

    public void Interact()
    {
        _isOpen = !_isOpen;
        
        foreach (var animator in _animators)
        {
            animator.SetTrigger("transition");
            triggerSound.PlaySound();
        }
    }

    public void ShowUI(bool show)
    {
        uiInteract.SetActive(show);
    }
}