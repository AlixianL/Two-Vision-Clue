using Rewired;
using Unity.Cinemachine;
using UnityEngine;

public class MirrorRotation : MonoBehaviour, IActivatable
{
    [Header("R�f�rences")]
    public Transform pivotHorizontal;              // Pivot de rotation gauche/droite
    public Transform mirrorTilt;                   // Partie du miroir qui s'incline
    [SerializeField] private Transform _playerTransform;

    [Header("Param�tres")]
    public float rotationSpeed = 50f;
    public float verticalMin = -45f;               // Limite minimum d'inclinaison
    public float verticalMax = 45f;                // Limite maximum d'inclinaison

    private bool _interactWithEnigma = false;
    [SerializeField] private bool _enigmaisend = false;

    private float verticalAngle = 0f;              // Stocke l'angle d'inclinaison


    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;


    public void Activate()
    {
        
        if (!_interactWithEnigma)
        {
            _interactWithEnigma = true;
        }
        else _interactWithEnigma = false;
        GameManager.Instance.ToggleTotalFreezePlayer();

        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWithEnigma);
        
        Vector3 direction = new Vector3(gameObject.transform.position.x, PlayerBrain.Instance.playerGameObject.transform.position.y, gameObject.transform.position.z);
        PlayerBrain.Instance.playerGameObject.transform.position = new Vector3(_playerTransform.position.x, PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.y, _playerTransform.position.z);
        PlayerBrain.Instance.playerGameObject.transform.rotation = Quaternion.Euler(0, _enigmaCinemachineCamera.transform.eulerAngles.y, 0);
        PlayerBrain.Instance.cinemachineTargetGameObject.transform.LookAt(direction);
    }

    void Update()
    {
        if (_interactWithEnigma && !_enigmaisend)
        {
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                pivotHorizontal.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                pivotHorizontal.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
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
    }
}
