using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;

public class Button : MonoBehaviour
{
    public Outline outline;
    public string message;

    public UnityEvent onInteraction;

    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] float resetDelay = .25f;

    [Header("--------------------------------------")]

    public FMODUnity.EventReference SoundActivate;

    private void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
        Debug.Log(outline);
    }



    public void PressButton()
    {
        GetComponent<MeshRenderer>().material.color = highlightColor;
        Invoke("ResetButton", resetDelay);
        Interact();
        AudioManager.instance.PlayOneShot(SoundActivate, this.transform.position);
    }

//#--------------------------------------------------------------------------------

    void ResetButton()
    {
        GetComponent<MeshRenderer>().material.color = defaultColor;
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
