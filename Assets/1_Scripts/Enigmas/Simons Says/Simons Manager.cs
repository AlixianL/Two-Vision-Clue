 using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SimonsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SimonsElement[] SimonsElement;//-----------------------> Liste de toute les chevre qui constitue l'enigme
    [SerializeField] private List<int> SimonsOrder = new List<int>();//-------------> liste dans laquel se trouve las equence a faire
    [SerializeField] private List<Light> LightForFeedback = new List<Light>();//----> liste des 4 light pour les feedback globaux

    [SerializeField] private Color _originalColor;//--------------------------------> Couleur d'origine des light
    [SerializeField] private Color _wrongColor;//-----------------------------------> Couleur quand le joueur fait une erreur
    [SerializeField] private Color _validateColor;//--------------------------------> Couleur quand le joueur valide al sequence


    [Header("Gameplay")]

    [SerializeField] private int _numberForEnd;//-----------------------------------> nombre de sequence a faire
    [SerializeField] private int _sequenceNumber;//---------------------------------> index de la sequence actuelle
    private int _placeTocheckSequence = 0;//----------------------------------------> place qui doit être verifier dans la liste

    private bool _enigmaIsEnd = false;//--------------------------------------------> booléen pour verifier si l'enigme est finit
    private bool _enigmaIsOn = false;//---------------------------------------------> booléen pour verifier si l'enigme est lancé

    [SerializeField] private float _simonsDelay = .4f;//----------------------------> Delay entre chaque chevre lors du montrage de la sequence
    [SerializeField] private float _feedbackDuration = 1.5f;//----------------------> delay des light pour le feedback global


    [Header("End Feedback")]
    [SerializeField] private GameObject _validationLight; //------------------------> light de validation sur le pilier centrale

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Initialisation du jeux  ----------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void Start()
    {
        SetIndex();
        _sequenceNumber = 0;
        _enigmaIsOn = false;
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction pour faire on/off a l'enigme --------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void On()
    {
        if (_enigmaIsOn)
        {
            _enigmaIsOn = false;
        }
        else if (!_enigmaIsOn)
        {
            StartCoroutine("SequenceToPLay");
            _enigmaIsOn = true;
        }
 
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Initialisation des index pour chaque bouton --------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void SetIndex()
    {
        for (int i = 0; i < SimonsElement.Length; i++)
        {
            SimonsElement[i].ButtonIndex = i;
        }

        CreateNextSequence();
    }


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Random pour decider de l'ordre de la sequence ------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void CreateNextSequence()
    {
        _sequenceNumber++;
        int random = Random.Range(0, SimonsElement.Length);
        SimonsOrder.Add(random);
        if (_enigmaIsOn)
        {
            StartCoroutine("SequenceToPLay");
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Coroutine qui montre la sequence a faire -----------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    IEnumerator SequenceToPLay()
    {
        Debug.LogError("je lance la sequence" + _sequenceNumber);
        _placeTocheckSequence = 0;
        yield return new WaitForSeconds(_simonsDelay);

        foreach (int Element in SimonsOrder)
        {
            SimonsElement[Element].FeedbackSimons();
            yield return new WaitForSeconds(_simonsDelay);
        }
    }


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Validation des bouton choisit par le joueur --------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void _simonsElementActivate(int simonsElementIndex)
    {
        if (!_enigmaIsEnd && _enigmaIsOn)
        {
            Debug.LogError("le bouton a marché");

            if (simonsElementIndex == SimonsOrder[_placeTocheckSequence])
            {
                _placeTocheckSequence++;
                if (_placeTocheckSequence == SimonsOrder.Count)
                {
                    if (_sequenceNumber == _numberForEnd)
                    {
                        EndEnigme();
                    }
                    else
                    {
                        StartCoroutine(ValidateSequence());
                    }                  
                }
            }
            else
            {
                StartCoroutine(WrongButton());
            }
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Condition de reussite d'une sequence ---------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private IEnumerator ValidateSequence()
    {
        Debug.LogError("la sequence est bonne");

        yield return StartCoroutine(CurrentSequenceFinish());
        CreateNextSequence();
    }

    private IEnumerator CurrentSequenceFinish()
    {

        foreach (Light lightObj in LightForFeedback)
        {
            lightObj.color = _validateColor;
        }

        yield return new WaitForSeconds(_feedbackDuration);

        foreach (Light lightObj in LightForFeedback)
        {
            lightObj.color = _originalColor;
        }
    }
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Condition d'erreur de sequence ---------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private IEnumerator WrongButton()
    {
        Debug.LogError("Erreur");

        yield return StartCoroutine(WrongFeedbackLight());
        StartCoroutine("SequenceToPLay");
    }

    private IEnumerator WrongFeedbackLight()
    {
        
        foreach (Light lightObj in LightForFeedback)
        {
            lightObj.color = _wrongColor;
        }

        yield return new WaitForSeconds(_feedbackDuration);

        foreach (Light lightObj in LightForFeedback)
        {
            lightObj.color = _originalColor;
        }
    }
    

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Condition de fin du mini jeux  ---------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void EndEnigme()
    {
        if (_validationLight != null)
        {
            _validationLight.SetActive(true);
        }
       
        _enigmaIsEnd = true;

        foreach (SimonsElement SimonsElementObject in SimonsElement)
        {
            if (SimonsElementObject != null)
            {
                SimonsElementObject.FreezSimons(); 
            }
            else
            {
                Debug.LogWarning("Un objet de la liste _mirror n'a pas de script SimonsElement !");
            }
        }

        Debug.Log("l'enigme est finito");

    }

}