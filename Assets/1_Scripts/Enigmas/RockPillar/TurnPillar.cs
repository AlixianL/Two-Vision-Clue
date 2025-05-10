using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TurnPillar : MonoBehaviour, IActivatable
{
    [Header("References")]
    [SerializeField] private List<GameObject> turnRock = new List<GameObject>();//----> liste des 3 caillou a tourner
    [SerializeField] private List<Transform> arrowposition = new List<Transform>();//-> liste des position de la flèche


    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;//------------> reference a la cinemachine camera pour voir le pilier
    [SerializeField] private GameObject _validationLight;//---------------------------> reference a la light de validation sur le pilier centrale
    [SerializeField] private Transform _arrow;//--------------------------------------> reference a la position de la flèche
    private Transform targetArrowPosition;//------------------------------------------> prochaine position de la fleche


    private GameObject _currentRock;//------------------------------------------------> caillou actuellement séléctionné
    private int _currentIndex = 0;//--------------------------------------------------> index du caillou actuellement séléctionné
    private bool _isRotating = false;//-----------------------------------------------> booléen qui verifie si un cube tourne
    private bool _enigmeisend = false;//----------------------------------------------> booléen qui verifie si l'enigme est fini

    [SerializeField] private bool _interactWithEnigma;//------------------------------> booléen qui verifie si on est entrein d'interagir avec le pillier

    [SerializeField] private float arrowMoveSpeed = 5f;//-----------------------------> vitesse de la fleche pour changer de position


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- initialisation de l'enigme -------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Start()
    {
        _validationLight.SetActive(false);
        _currentRock = turnRock[_currentIndex];
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Activation avec l'interaction du joueur  -----------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Activate()
    {
        _currentIndex = 0;
        if (!_interactWithEnigma)
        {
            _interactWithEnigma = true;
        }
        else _interactWithEnigma = false;

        GameManager.Instance.ToggleTotalFreezePlayer();
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWithEnigma);
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- logique des touche du pillier  ---------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Update()
    {
        if (_interactWithEnigma && !_isRotating && !_enigmeisend)
        {
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                StartCoroutine(RotateRockSmooth(90f, 0.5f));
            }
            if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                StartCoroutine(RotateRockSmooth(-90f, 0.5f));
            }

            if (PlayerBrain.Instance.player.GetButtonDown("ForwardMovement"))
            {
                _currentIndex = (_currentIndex + 1 + turnRock.Count) % turnRock.Count;
                _currentRock = turnRock[_currentIndex];
            }
            if (PlayerBrain.Instance.player.GetButtonDown("BackwardMovement"))
            {
                _currentIndex = (_currentIndex - 1 + turnRock.Count) % turnRock.Count;
                _currentRock = turnRock[_currentIndex];
            }
        }
        //-----> ICI la position de la fleche quand on interagit avec l'enigme
        if (_interactWithEnigma)
        {
            targetArrowPosition = arrowposition[_currentIndex+1];
        }
        else
        {
            targetArrowPosition = arrowposition[0]; // Position "idle"
        }
        //-----> ICI la position de la fleche quand on passe d'un cube a l'autre
        if (_arrow != null && targetArrowPosition != null)
        {
            _arrow.position = Vector3.Lerp(_arrow.position, targetArrowPosition.position, Time.deltaTime * arrowMoveSpeed);
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- logique de rotation de l'enigme --------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    IEnumerator RotateRockSmooth(float angle, float duration)
    {
        _isRotating = true;

        Quaternion startRotation = _currentRock.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);

        float animationTime = 0f;

        while (animationTime < duration)
        {
            _currentRock.transform.rotation = Quaternion.Slerp(startRotation, endRotation, animationTime / duration);
            animationTime += Time.deltaTime;
            yield return null;
        }

        _currentRock.transform.rotation = endRotation;
        _isRotating = false;
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- fin de l'enigme  -----------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void EndEnigme()
    {
        _validationLight.SetActive(true);
        _enigmeisend = true;
    }
}