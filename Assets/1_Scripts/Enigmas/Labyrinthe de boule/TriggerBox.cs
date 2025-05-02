using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private RotateWeel _rotateWeel;
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "SM_Boule_001")
        {
            _rotateWeel.enigmaIsValidate = true;
            other.attachedRigidbody.freezeRotation = true;
            other.attachedRigidbody.useGravity = false;
        }
    }
}
