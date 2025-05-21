using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour, IActivatable
{
    [Header("Cinemachine Settings")]
    [SerializeField] private List<CinemachineCamera> _cameraList;
    [SerializeField] private CinemachineCamera _playerCamera;
    private int _currentIndex = 0;

    [Header("UI Settings")]
    [SerializeField] private GameObject _cameraSwitchUI;

    [Header("Interaction")]
    [SerializeField] private bool _interactWithEnigma;

    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;
        if(!_interactWithEnigma)
        {
            _interactWithEnigma = true;
        }
        else _interactWithEnigma = false;
        ChangePositionCinemachine.Instance.SwitchCam(_cameraList[_currentIndex], _interactWithEnigma);
      
        GameManager.Instance.playerUI.SetActive(!_interactWithEnigma);
        GameManager.Instance.pillarUI.SetActive(_interactWithEnigma);
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
        _currentIndex--;
        if (_currentIndex < 0)
            _currentIndex = _cameraList.Count - 1;

        SwitchToCamera(_cameraList[_currentIndex]);
    }

    private void SwitchToCamera(CinemachineCamera targetCam)
    {
        foreach (var cam in _cameraList)
        {
            cam.gameObject.SetActive(false);
        }

        _playerCamera.gameObject.SetActive(false);
        targetCam.gameObject.SetActive(true);
    }
}