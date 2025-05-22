using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Cinemachine;
using UnityEngine;

public class TurnPillar : MonoBehaviour, IActivatable, ISaveAndPullData
{
    [Header("References")]
    [SerializeField] private Animator _pillarAnimator;
    [SerializeField] private List<GameObject> turnRock = new List<GameObject>();//----> liste des 3 caillou a tourner
    [SerializeField] private List<Transform> arrowposition = new List<Transform>();//-> liste des position de la fl�che


    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;//------------> reference a la cinemachine camera pour voir le pilier
    [SerializeField] private Transform _arrow;//--------------------------------------> reference a la position de la fl�che
    private Transform targetArrowPosition;//------------------------------------------> prochaine position de la fleche


    private GameObject _currentRock;//------------------------------------------------> caillou actuellement s�l�ctionn�
    private int _currentIndex = 0;//--------------------------------------------------> index du caillou actuellement s�l�ctionn�
    private bool _isRotating = false;//-----------------------------------------------> boolen qui verifie si un cube tourne
    private bool _enigmeisend = false;//----------------------------------------------> boolen qui verifie si l'enigme est fini
    public float rotationDuration = 0.8f;//-------------------------------------------> boolen qui determine le temps de rotation


    [SerializeField] private bool _interactWithEnigma;//------------------------------> boolen qui verifie si on est entrein d'interagir avec le pillier

    [SerializeField] private float arrowMoveSpeed = 5f;//-----------------------------> vitesse de la fleche pour changer de position


    [Header("End Feedback")]
    [SerializeField] private GameObject _validationLight;//---------------------------> reference a la light de validation sur le pilier centrale
    [SerializeField] private UnlockFInal _unlock;
    [SerializeField] private GameObject _number;

    public TriggerSoundMultiple triggerSoundMultiple;




    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- initialisation de l'enigme -------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void Start()
    {
        _validationLight.SetActive(false);
        _currentRock = turnRock[_currentIndex];
        _number.SetActive(false);

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
            triggerSoundMultiple.PlaySound(0);
        }
        else _interactWithEnigma = false;

        GameManager.Instance.ToggleTotalFreezePlayer();
        //PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWithEnigma);
        GameManager.Instance.playerUI.SetActive(!_interactWithEnigma);
        GameManager.Instance.pillarUI.SetActive(_interactWithEnigma);
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
                StartCoroutine(RotateRockSmooth(90f));
                triggerSoundMultiple.PlaySound(2);
            }
            if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                StartCoroutine(RotateRockSmooth(-90f));
                triggerSoundMultiple.PlaySound(2);
            }

            if (PlayerBrain.Instance.player.GetButtonDown("ForwardMovement"))
            {
                _currentIndex = (_currentIndex + 1 + turnRock.Count) % turnRock.Count;
                _currentRock = turnRock[_currentIndex];
                triggerSoundMultiple.PlaySound(1);
            }
            if (PlayerBrain.Instance.player.GetButtonDown("BackwardMovement"))
            {
                _currentIndex = (_currentIndex - 1 + turnRock.Count) % turnRock.Count;
                _currentRock = turnRock[_currentIndex];
                triggerSoundMultiple.PlaySound(1);
            }
        }
        //-----> ICI la position de la fleche quand on interagit avec l'enigme
        if (_interactWithEnigma)
        {
            _pillarAnimator.SetBool("IsActive", true);
            
            targetArrowPosition = arrowposition[_currentIndex+1];
        }
        else
        {
            targetArrowPosition = arrowposition[0]; // Position "idle"
            _pillarAnimator.SetBool("IsActive", false); 
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
    IEnumerator RotateRockSmooth(float angle)
    {
        _isRotating = true;

        Quaternion startRotation = _currentRock.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);

        float animationTime = 0f;

        while (animationTime < rotationDuration)
        {
            float t = animationTime / rotationDuration;
            _currentRock.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t );
            animationTime += Time.deltaTime;
            yield return null;
        }

        _currentRock.transform.rotation = endRotation;
        _isRotating = false;
        
        PushDataToSave();
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- fin de l'enigme  -----------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void EndEnigme()
    {
        _validationLight.SetActive(true);
        _enigmeisend = true;
        _unlock._pillarIsEnd = true;
        _number.SetActive(true);


        SaveData.Instance.gameData.enigmaIsComplete_pillar = true;
    }

    public void PullDataFromSave()
    {
        turnRock[0].transform.localEulerAngles = SaveData.Instance.gameData.rotationCubeYBot;
        Debug.Log("bloc bas (euler) " + turnRock[0].transform.localEulerAngles);

        turnRock[1].transform.localEulerAngles = SaveData.Instance.gameData.rotationCubeYMid;
        Debug.Log("bloc milieu (euler) " + turnRock[1].transform.localEulerAngles);

        turnRock[2].transform.localEulerAngles = SaveData.Instance.gameData.rotationCubeYTop;
        Debug.Log("bloc haut (euler) " + turnRock[2].transform.localEulerAngles);
    }


    public void PushDataToSave()
    {
        switch (_currentIndex)
        {
            case 0:
                SaveData.Instance.gameData.rotationCubeYBot = NormalizeEuler(turnRock[0].transform.localEulerAngles);
                Debug.Log("bloc bas " + turnRock[0].transform.localEulerAngles);
                break;
            case 1:
                SaveData.Instance.gameData.rotationCubeYMid = NormalizeEuler(turnRock[1].transform.localEulerAngles);
                Debug.Log("bloc mid " + turnRock[1].transform.localEulerAngles);
                break;
            case 2:
                SaveData.Instance.gameData.rotationCubeYTop = NormalizeEuler(turnRock[2].transform.localEulerAngles);
                Debug.Log("bloc haut " + turnRock[2].transform.localEulerAngles);
                break;
        }
    }
    
    Vector3 NormalizeEuler(Vector3 euler)
    {
        return new Vector3(
            Mathf.Repeat(euler.x, 360f),
            Mathf.Repeat(euler.y, 360f),
            Mathf.Repeat(euler.z, 360f)
        );
    }

}