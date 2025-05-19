using System.Collections.Generic;
using UnityEngine;

public class VerifForFinal : MonoBehaviour
{
    [SerializeField] private List<GameObject> _checkerList = new List<GameObject>();

    private List<GameObject> _objectsInZone = new List<GameObject>();

    [SerializeField] private WheelRotation _wheelRotation;


    void Start()
    {
        _objectsInZone.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_checkerList.Contains(other.gameObject))
        {
            _objectsInZone.Add(other.gameObject);
            CheckAllPresent();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_objectsInZone.Contains(other.gameObject))
        {
            _objectsInZone.Remove(other.gameObject);
        }
    }

    void CheckAllPresent()
    {
        if (_objectsInZone.Count == _checkerList.Count)
        {
            _wheelRotation.EndEnigme();
        }
    }
}
