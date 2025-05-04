using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class RotateWeel : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _labyrinth;
    [SerializeField] private GameObject _ball;
    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private float _rotationSpeed;
    [Space(5)]
    [SerializeField] private bool _interactWhisEnigma;
    public bool enigmaIsValidate;

    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();

        if (!_interactWhisEnigma)
        {
            _interactWhisEnigma = true;
        }
        else _interactWhisEnigma = false;
        
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWhisEnigma);
    }
    
    void Update()
    {
        if (_interactWhisEnigma)
        {
            // Rotation droite
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
            }

            // Rotation gauche
            else if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                transform.Rotate(0f, 0f, -_rotationSpeed * Time.deltaTime);
            }

            if (!enigmaIsValidate)
            {
                _labyrinth.transform.localEulerAngles = new Vector3(-transform.rotation.eulerAngles.z, -207f, 90f);
            }
        }
    }
}
