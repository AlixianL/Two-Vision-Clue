using UnityEngine;

public class Doors : MonoBehaviour, IInteractable
{
    [Header("Interaction Settings"), Space(5)]
    [SerializeField] private Animator[] _animators;
    [SerializeField] private GameObject uiInteract;
    public PlaySoundScript playSoundScript;

    public bool _isOpen = false;

    
    public void Interact()
    {
        _isOpen = !_isOpen;

        playSoundScript.PlaySound();
        Debug.Log("Sound as been played");

        foreach (var animator in _animators)
        {
            animator.SetTrigger("transition");   
        }
    }

    public void ShowUI(bool show)
    {
        uiInteract.SetActive(show);
    }
}