using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour, IActivatable
{
    [Header("Cinemachine Settings")]
    [SerializeField] private List<CinemachineCamera> _cameraList;
    [SerializeField] private CinemachineCamera _playerCamera;
    private int _currentIndex = 0;
    private int _defaultPlayerPriority;

    [Header("UI Settings")]
    [SerializeField] private GameObject _cameraSwitchUI;

    [Header("Interaction")]
    [SerializeField] private bool _interactWithEnigma = false;

    private void Start()
    {
        // Sauvegarde la priorité par défaut de la caméra joueur
        _defaultPlayerPriority = _playerCamera.Priority;
    }

    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;

        // Toggle interaction
        _interactWithEnigma = !_interactWithEnigma;

        if (_interactWithEnigma)
        {
            // Mode énigme - baisse la priorité de la caméra joueur
            _playerCamera.Priority = 0;
            _currentIndex = 0;
            SwitchToCamera(_cameraList[_currentIndex]);
        }
        else
        {
            // Retour au joueur - restaure la priorité de la caméra joueur
            SwitchToPlayerCamera();
        }

        GameManager.Instance.playerUI.SetActive(!_interactWithEnigma);
        _cameraSwitchUI.SetActive(_interactWithEnigma);
    }

    void Update()
    {
        if (!_interactWithEnigma) return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            SwitchRight();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchLeft();
        }
    }

    private void SwitchRight()
    {
        _currentIndex = (_currentIndex + 1) % _cameraList.Count;
        SwitchToCamera(_cameraList[_currentIndex]);
    }

    private void SwitchLeft()
    {
        _currentIndex = (_currentIndex - 1 + _cameraList.Count) % _cameraList.Count;
        SwitchToCamera(_cameraList[_currentIndex]);
    }

    private void SwitchToCamera(CinemachineCamera targetCam)
    {
        // Réinitialise toutes les priorités
        foreach (var cam in _cameraList)
        {
            cam.Priority = (cam == targetCam) ? 100 : 0;
        }
    }

    private void SwitchToPlayerCamera()
    {
        // Remet toutes les caméras à priorité 0
        foreach (var cam in _cameraList)
        {
            cam.Priority = 0;
        }
        
        // Restaure la priorité de la caméra joueur
        _playerCamera.Priority = _defaultPlayerPriority;
    }
}