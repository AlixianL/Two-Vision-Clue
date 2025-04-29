using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPillar : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> turnRock = new List<GameObject>();

    public KeyCode previousRock = KeyCode.G;
    public KeyCode nextRock = KeyCode.T;
    public KeyCode turnLeft = KeyCode.H;
    public KeyCode turnRight = KeyCode.F;

    private GameObject _currentRock;
    private int _currentIndex = 0;
    private bool _isRotating = false;

    void Start()
    {
        _currentRock = turnRock[_currentIndex];
    }

    void Update()
    {
        if (Input.GetKeyDown(nextRock))
        {
            _currentIndex = (_currentIndex + 1) % turnRock.Count;
            _currentRock = turnRock[_currentIndex];
        }

        if (Input.GetKeyDown(previousRock))
        {
            _currentIndex = (_currentIndex - 1 + turnRock.Count) % turnRock.Count;
            _currentRock = turnRock[_currentIndex];
        }

        if (!_isRotating)
        {
            if (Input.GetKeyDown(turnLeft))
            {
                StartCoroutine(RotateRockSmooth(-90f, 0.5f));
            }

            if (Input.GetKeyDown(turnRight))
            {
                StartCoroutine(RotateRockSmooth(90f, 0.5f));
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
}
