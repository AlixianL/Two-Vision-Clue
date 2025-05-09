using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimonsManager : MonoBehaviour
{
    [Header("All simons")]
    [SerializeField] private SimonsElement[] SimonsElement;

    [Header("Gameplay")]
    [SerializeField] private List<int> simonsOrder = new List<int>();
    [SerializeField] private float _simonsDelay = .4f;
    [SerializeField] private int _orderInSequence = 0;
    [SerializeField] private bool _enigmaisend = false;
    [SerializeField] private int _numberForEnd;



    [Header("End Feedback")]
    [SerializeField] private GameObject _validationLight;

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Initialisation du jeux  ----------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void Start()
    {
        SetButtonIndex();
        StartCoroutine("PlayGame");
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Verification de la fin du simons -------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Update()
    {

    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Initialisation des index pour chaque bouton --------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void SetButtonIndex()
    {
        for (int cnt = 0; cnt < SimonsElement.Length; cnt++)
            SimonsElement[cnt].ButtonIndex = cnt;
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Coroutine du fonctionnement du jeux ----------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    IEnumerator PlayGame()
    {
        _orderInSequence = 0;
        yield return new WaitForSeconds(_simonsDelay);

        foreach (int colorIndex in simonsOrder)
        {
            SimonsElement[colorIndex].FeedbackSimons();
            yield return new WaitForSeconds(_simonsDelay);
        }

        RandomsimonsOrder();
    }


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Random pour decider de l'ordre des cube ------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void RandomsimonsOrder()
    {
        int rnd = Random.Range(0, SimonsElement.Length);
        SimonsElement[rnd].FeedbackSimons();
        simonsOrder.Add(rnd);
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Validation des bouton choisit par le joueur --------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void PlayersPick(int pick)
    {
        if (_enigmaisend) return;

        Debug.Log(simonsOrder.Count);
        Debug.Log(_orderInSequence);

        if (pick == simonsOrder[_orderInSequence])
        {
            _orderInSequence++;

            if (_orderInSequence == simonsOrder.Count)
            {
                if (simonsOrder.Count >= _numberForEnd)
                {
                    EndEnigme();
                }
                else
                {
                    StartCoroutine(PlayGame());
                }
            }
        }
        else
        {
            Debug.Log("Mauvais bouton !");

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
       
        _enigmaisend = true;

        foreach (SimonsElement mirrorObject in SimonsElement)
        {
            if (mirrorObject != null)
            {
                mirrorObject.FreezSimons(); 
            }
            else
            {
                Debug.LogWarning("Un objet de la liste _mirror n'a pas de script SimonsElement !");
            }
        }
        Debug.Log("l'enigme est finito");

    }

}