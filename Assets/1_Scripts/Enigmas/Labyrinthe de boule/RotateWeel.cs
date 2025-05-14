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
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;

        if (!_interactWhisEnigma)
        {
            _interactWhisEnigma = true;
        }
        else _interactWhisEnigma = false;
        
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWhisEnigma);
        
        GameManager.Instance.playerUI.SetActive(!_interactWhisEnigma);
        GameManager.Instance.labyrintheUI.SetActive(_interactWhisEnigma);

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
