using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;
using UnityEditor.Build.Content;

public class Button : MonoBehaviour
{
    public Outline outline;
    public string message;

    public UnityEvent onInteraction;


    public int ButtonIndex { get; set; }
    [SerializeField] SimonsManager simonsManager;
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
        
    }

//#--------------------------------------------------------------------------------

    void ResetButton()
    {
        GetComponent<MeshRenderer>().material.color = defaultColor;
        AudioManager.instance.PlayOneShot(SoundActivate, this.transform.position);
    }
       
    public void Interact()
    {
        onInteraction.Invoke();
        simonsManager.PlayersPick(ButtonIndex);
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
