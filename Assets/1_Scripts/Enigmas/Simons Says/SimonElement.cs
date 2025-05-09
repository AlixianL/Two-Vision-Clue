using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;
using UnityEditor.Build.Content;

public class SimonsElement : MonoBehaviour, IActivatable
{
    [Header("Gameplay")]
    public int ButtonIndex { get; set; }
    [SerializeField] private SimonsManager _simonsManager;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private float resetDelay = .25f;
    [SerializeField] private bool _enigmaIsEnd = false;


    [Header("--------------------------------------")]

    public FMODUnity.EventReference SoundActivate;


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Activation avec l'interaction du joueur  -------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Activate()
    {
        if (!_enigmaIsEnd)
        {
            FeedbackSimons();
            _simonsManager.PlayersPick(ButtonIndex);
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Feedback pour l'ordre et le choix ud joueur  -------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void FeedbackSimons()
    {
        GetComponent<MeshRenderer>().material.color = _highlightColor;
        Invoke("ResetButton", resetDelay);

    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Reset le bouton  -----------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void ResetButton()
    {
        GetComponent<MeshRenderer>().material.color = _defaultColor;
        AudioManager.instance.PlayOneShot(SoundActivate, this.transform.position);
    }

    public void FreezSimons()
    {
        _enigmaIsEnd = true;
    }
}
