using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TurnPillar : MonoBehaviour, IActivatable
{

    [SerializeField] private List<GameObject> turnRock = new List<GameObject>();

    private GameObject _currentRock;
    private int _currentIndex = 0;
    private bool _isRotating = false;
    private bool _enigmeisend = false;

    [SerializeField] private bool _interactWithEnigma;
    [SerializeField] private bool _enigmaisend;

    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;
    [SerializeField] private GameObject _validationLight;
    [SerializeField] private Transform _playerTransform;


    void Start()
    {
        _validationLight.SetActive(false);
        _currentRock = turnRock[_currentIndex];
    }

    public void Activate()
    {
        if (!_interactWithEnigma)_interactWithEnigma = true;
        else _interactWithEnigma = false;
        
        PlayerBrain.Instance.transform.position = new Vector3(_playerTransform.position.x, PlayerBrain.Instance.transform.position.y, _playerTransform.position.z);
        GameManager.Instance.ToggleTotalFreezePlayer();
        
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWithEnigma);
        
        Vector3 direction = new Vector3(gameObject.transform.position.x, PlayerBrain.Instance.playerGameObject.transform.position.y, gameObject.transform.position.z);
        PlayerBrain.Instance.playerGameObject.transform.position = new Vector3(_playerTransform.position.x, PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.y, _playerTransform.position.z);
        PlayerBrain.Instance.playerGameObject.transform.rotation = Quaternion.Euler(0, _enigmaCinemachineCamera.transform.eulerAngles.y, 0);
        PlayerBrain.Instance.cinemachineTargetGameObject.transform.LookAt(direction);
    }
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
                _currentIndex = (_currentIndex + 1+ turnRock.Count) % turnRock.Count;
                _currentRock = turnRock[_currentIndex];
            }
            if (PlayerBrain.Instance.player.GetButtonDown("BackwardMovement"))
            {
                _currentIndex = (_currentIndex - 1 + turnRock.Count) % turnRock.Count;
                _currentRock = turnRock[_currentIndex];
            }
        }
    }

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

    public void EndEnigme()
    {
        _validationLight.SetActive(true);
        _enigmeisend = true;
    }
}
