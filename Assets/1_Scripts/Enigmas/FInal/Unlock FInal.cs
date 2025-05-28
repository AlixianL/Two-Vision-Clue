using System.Collections;
using UnityEngine;

public class UnlockFInal : MonoBehaviour
{
    [SerializeField] public bool _rayonIsEnd = false;
    [SerializeField] public bool _labiryntheIsEnd = false;
    [SerializeField] public bool _goatIsEnd = false;
    [SerializeField] public bool _pillarIsEnd = false;
    [SerializeField] public bool _allIsEnd = false;

    public bool _rayonisOpen = false;
    public bool _labisOpen = false;
    public bool _goatisOpen = false;
    public bool _pillarisOpen = false;


    [SerializeField] private GameObject _lockerRayon;
    [SerializeField] private GameObject _lockerLabyrinthe;
    [SerializeField] private GameObject _lockerGoat;
    [SerializeField] private GameObject _lockerPillar;


    private GameObject _lockerToOpen;



    void Start()
    {
        _rayonIsEnd = false;
        _labiryntheIsEnd = false;
        _goatIsEnd = false;
        _pillarIsEnd = false;
        _allIsEnd = false;

        _rayonisOpen = false;
        _labisOpen = false;
        _goatisOpen = false;
        _pillarisOpen = false;

    }

    void Update()
    {
        if (_rayonIsEnd)
        {
            if (!_rayonisOpen)
            {
                _rayonisOpen = true;
                OpenButton(_lockerRayon);
            }
            
        }

        if (_labiryntheIsEnd)
        {
            if (!_labisOpen)
            {
                _labisOpen = true;
                OpenButton(_lockerLabyrinthe);
            }
        }

        if (_goatIsEnd)
        {
            if (!_goatisOpen)
            {
                _goatisOpen = true;
                OpenButton(_lockerGoat);
            }
        }

        if (_pillarIsEnd)
        {
            if (!_pillarisOpen)
            {
                _pillarisOpen = true;
                OpenButton(_lockerPillar);
            }
        }
    }

    void OpenButton(GameObject _currentEnding)
    {
        _lockerToOpen = _currentEnding;
        StartCoroutine(UnlockButton(-90f,_lockerToOpen));
        
    }

    IEnumerator UnlockButton(float angle,GameObject _lockerToOpen)
    {
        
        float duration = 1f;
        float elapsed = 0f;

        Quaternion initialRotation = _lockerToOpen.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(angle, 0f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            _lockerToOpen.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        _lockerToOpen.transform.rotation = targetRotation;
    }
}
