using UnityEngine;

public class ActivateTrackPlayer : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _targetCanva;

    void Start()
    {
        _targetCanva.SetActive(false);
    }
    
    void ActivateTracker()
    {
        if (_targetCanva != null)
        {
            if (_targetCanva.activeSelf == false) _targetCanva.SetActive(true);
            else _targetCanva.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateTracker();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateTracker();
        }
    }
}
