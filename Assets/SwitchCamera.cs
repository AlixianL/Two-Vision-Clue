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
    [SerializeField] private List<CanvasGroup> _uiGroups; // Nouvel ajout

    [Header("Interaction")]
    [SerializeField] private bool _interactWithEnigma = false;

    private void Start()
    {
        _defaultPlayerPriority = _playerCamera.Priority;

        // S'assurer que tous les CanvasGroup sont cachés au début
        foreach (CanvasGroup group in _uiGroups)
        {
            SetCanvasGroupVisible(group, false);
        }
    }

    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;

        _interactWithEnigma = !_interactWithEnigma;

        if (_interactWithEnigma)
        {
            _playerCamera.Priority = 0;
            _currentIndex = 0;
            SwitchToCamera(_cameraList[_currentIndex]);
        }
        else
        {
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
        for (int i = 0; i < _cameraList.Count; i++)
        {
            _cameraList[i].Priority = (_cameraList[i] == targetCam) ? 100 : 0;

            if (i < _uiGroups.Count)
            {
                SetCanvasGroupVisible(_uiGroups[i], _cameraList[i] == targetCam);
            }
        }
    }

    private void SwitchToPlayerCamera()
    {
        foreach (var cam in _cameraList)
        {
            cam.Priority = 0;
        }

        _playerCamera.Priority = _defaultPlayerPriority;

        // Cacher tous les UI liés aux caméras
        foreach (CanvasGroup group in _uiGroups)
        {
            SetCanvasGroupVisible(group, false);
        }
    }

    private void SetCanvasGroupVisible(CanvasGroup group, bool visible)
    {
        group.alpha = visible ? 1 : 0;
        group.interactable = visible;
        group.blocksRaycasts = visible;
    }
}
