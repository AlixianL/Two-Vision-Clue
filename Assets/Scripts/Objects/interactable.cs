using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Outline outline;
    public string message;

    public UnityEvent onInteraction;

    private void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
        Debug.Log(outline);
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }
    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    
}
