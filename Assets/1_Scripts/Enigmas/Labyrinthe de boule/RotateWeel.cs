using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RotateWeel : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _labyrinth;
    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;
    [SerializeField] private GameObject _labyrintheLight;//-------------------------> Light pour voir le labyrinthe
    [SerializeField] private Transform _playerTransform;
    private Light _lightComponent;


    [Header("Variables"), Space(5)]
    [SerializeField] private float _rotationSpeed;
    [Space(5)]
    [SerializeField] private bool _interactWhisEnigma;
    public bool enigmaIsValidate;

    void Start()
    {
        _lightComponent = _labyrintheLight.GetComponent<Light>();
    }
    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();
        
        if (!_interactWhisEnigma)
        {
            _interactWhisEnigma = true;
        }
        else _interactWhisEnigma = false;
        
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWhisEnigma);

        GameManager.Instance.GeneralDelay(0.5f);
        PlayerBrain.Instance.transform.Translate(_playerTransform.position.x, PlayerBrain.Instance.transform.position.y + 0.5f, _playerTransform.position.z);
        
        //Vector3 direction = new Vector3(gameObject.transform.position.x, PlayerBrain.Instance.playerGameObject.transform.position.y, gameObject.transform.position.z + 2f);
        //PlayerBrain.Instance.playerGameObject.transform.position = new Vector3(_playerTransform.position.x, PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.y, _playerTransform.position.z);
        //PlayerBrain.Instance.playerGameObject.transform.rotation = Quaternion.Euler(0, _enigmaCinemachineCamera.transform.eulerAngles.y, 0);
        //PlayerBrain.Instance.cinemachineTargetGameObject.transform.LookAt(direction);

        if (_lightComponent.enabled == false)
        {
            _lightComponent.enabled = true;
        }
        else _lightComponent.enabled = false;
    }
    
    void FixedUpdate()
    {
        if (_interactWhisEnigma)
        {
            // Rotation droite
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                transform.Rotate(-_rotationSpeed * Time.deltaTime, 0f, 0f);
            }

            // Rotation gauche
            else if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                transform.Rotate(_rotationSpeed * Time.deltaTime, 0f, 0f);
            }

            if (!enigmaIsValidate)
            {
                // Rotation droite
                if (PlayerBrain.Instance.player.GetButton("RightMovement"))
                {
                    _labyrinth.transform.Rotate(-_rotationSpeed * Time.deltaTime, 0f, 0f);
                }

                // Rotation gauche
                else if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
                {
                    _labyrinth.transform.Rotate(_rotationSpeed * Time.deltaTime, 0f, 0f);
                }
            }
        }
    }
}
