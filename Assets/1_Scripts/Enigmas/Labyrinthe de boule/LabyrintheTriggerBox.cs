using UnityEngine;

public class LabyrintheTriggerBox : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private RotateWeel _rotateWeel;
    [SerializeField] private GameObject _validationLight;//-------------------------> Light Sur le pilier central pour valid� l'�nigme

    void Start()
    {
        _validationLight.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "SM_Boule_001")
        {
            _rotateWeel.enigmaIsValidate = true;
            other.attachedRigidbody.freezeRotation = true;
            other.attachedRigidbody.useGravity = false;
            _validationLight.SetActive(true);
            Debug.Log("Labyrinthe Fini");
        }
    }
}
