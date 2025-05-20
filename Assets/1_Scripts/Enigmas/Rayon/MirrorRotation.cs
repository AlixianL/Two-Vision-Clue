using Rewired;
using Unity.Cinemachine;
using UnityEngine;

public class MirrorRotation : MonoBehaviour, IActivatable
{
    [Header("R�f�rences")]
    public Transform pivotHorizontal;              // Pivot de rotation gauche/droite
    public Transform mirrorTilt;                   // Partie du miroir qui s'incline


    [Header("Param�tres")]
    public float rotationSpeed = 50f;
    public float verticalMin = -45f;               // Limite minimum d'inclinaison
    public float verticalMax = 45f;                // Limite maximum d'inclinaison

    private bool _interactWithEnigma = false;
    [SerializeField] private bool _enigmaisend = false;

    private float verticalAngle = 0f;              // Stocke l'angle d'inclinaison


    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;

    //Sound-Design
    //---------------------------------
    public TriggerSound triggerSound;


    public void Activate()
    {
        
        if (!_interactWithEnigma)
        {
            _interactWithEnigma = true;
        }
        else _interactWithEnigma = false;

        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;
        
        if (_enigmaCinemachineCamera != null)
        {
            ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWithEnigma);
        }

        GameManager.Instance.playerUI.SetActive(!_interactWithEnigma);
        GameManager.Instance.mirrorUI.SetActive(_interactWithEnigma);
    }

    void Update()
    {
        if (_interactWithEnigma && !_enigmaisend)
        {
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                pivotHorizontal.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);

                //Sound-Design
                //---------------------------------
                triggerSound.PlaySound();
            }
            if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                pivotHorizontal.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

                //Sound-Design
                //---------------------------------
                triggerSound.PlaySound();
            }

            if (PlayerBrain.Instance.player.GetButton("ForwardMovement"))
            {
                verticalAngle -= rotationSpeed * Time.deltaTime;
            }
            if (PlayerBrain.Instance.player.GetButton("BackwardMovement"))
            {
                verticalAngle += rotationSpeed * Time.deltaTime;
            }

            verticalAngle = Mathf.Clamp(verticalAngle, verticalMin, verticalMax);

            mirrorTilt.localEulerAngles = new Vector3(0f, 0f, verticalAngle);
        }
    }

    public void FreezMirror()
    {
        _enigmaisend = true;
        Debug.Log("Enigme Miroir fini");
    }
}
