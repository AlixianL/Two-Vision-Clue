using System.Collections;
using UnityEngine;

public class UnlockFInal : MonoBehaviour
{
    [SerializeField] public bool _rayonIsEnd = false;
    [SerializeField] public bool _labiryntheIsEnd = false;
    [SerializeField] public bool _goatIsEnd = false;
    [SerializeField] public bool _pillarIsEnd = false;
    [SerializeField] public bool _allIsEnd = false;
    [SerializeField] public bool _isOpen = false;


    [SerializeField] private GameObject _locker;



    void Start()
    {
        _rayonIsEnd = false;
        _labiryntheIsEnd = false;
        _goatIsEnd = false;
        _pillarIsEnd = false;
        _allIsEnd = false;
    }

    void Update()
    {
        if (_rayonIsEnd && _labiryntheIsEnd && _goatIsEnd && _pillarIsEnd)
        {
            _allIsEnd = true;
        }

        if (_allIsEnd && !_isOpen)
        {
            OpenButton();
            _isOpen = true;
        }
        
    }

    void OpenButton()
    {
        StartCoroutine(UnlockButton(90f));
        
    }

    IEnumerator UnlockButton(float angle)
    {
        float duration = 1f;
        float elapsed = 0f;

        Quaternion initialRotation = _locker.transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(angle, 0f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            _locker.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        _locker.transform.rotation = targetRotation;
    }
}
