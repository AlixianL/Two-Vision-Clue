using UnityEngine;
using FMODUnity;
using System.Collections;


public class SimonsElement : MonoBehaviour, IActivatable
{
    [Header("References")]

    [SerializeField] private Light _simonsLight;//--------------> Light au pied du sphyxe
    [SerializeField] private SimonsManager _simonsManager;//----> reference au manager de l'enigme
    [SerializeField] private Color _defaultColor;//-------------> couleur de base de la lumière
    [SerializeField] private Color _pressColor;//---------------> couleur pour indiquer l'interaction


    [Header("Gameplay")]
    public int ButtonIndex { get; set; }//----------------------> index du bouton donné par le manager

    [SerializeField] private float resetDelay = .25f;//---------> delay de la lumière en mode "interaction"
    [SerializeField] private bool _enigmaIsEnd = false;//-------> booléen pour "freeze" l'enigme

    public TriggerSound triggerSound;
    

    

    public void Start()
    {
        _simonsLight.color = _defaultColor;
    }
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Activation avec l'interaction du joueur  -----------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Activate()
    {
        if (!_enigmaIsEnd)
        {
            FeedbackSimons();
            _simonsManager._simonsElementActivate(ButtonIndex);
            triggerSound.JouerOneShot();
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Feedback sur chaque pression de bouton -------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void FeedbackSimons()
    {
        StartCoroutine(FlashRoutine());
        

    }

    private IEnumerator FlashRoutine()
    {
        _simonsLight.color = _pressColor;
        yield return new WaitForSeconds(resetDelay);
        _simonsLight.color = _defaultColor;
        triggerSound.JouerOneShot();

    }


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Freeze les bouton une fois l'enigme fini  ----------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void FreezSimons()
    {
        _enigmaIsEnd = true;
    }
}
