using UnityEngine;

public class UnlockFInal : MonoBehaviour
{
    [SerializeField] public bool _rayonIsEnd = false;
    [SerializeField] public bool _labiryntheIsEnd = false;
    [SerializeField] public bool _goatIsEnd = false;
    [SerializeField] public bool _pillarIsEnd = false;
    [SerializeField] public bool _allIsEnd = false;

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

        if (_allIsEnd)
        {
            OpenButton();
        }
        
    }

    void OpenButton()
    {
        _locker.transform.position = Vector3.zero;
    }
}
