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

    [SerializeField] private bool _interactWhisEnigma;
    [SerializeField] private bool _enigmaisend;

    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;

    void Start()
    {
        _currentRock = turnRock[_currentIndex];
    }

    public void Activate()
    {
        if (!_interactWhisEnigma)
        {
            _interactWhisEnigma = true;
        }
        else _interactWhisEnigma = false;
        GameManager.Instance.ToggleTotalFreezePlayer();

        

        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWhisEnigma);
    }
    void Update()
    {
        if (_interactWhisEnigma && !_isRotating && !_enigmeisend)
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
        _enigmeisend = true;
    }
}
