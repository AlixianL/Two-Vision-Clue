using UnityEngine;
using Rewired;

public class CameraRotation : MonoBehaviour
{
    [Header("Variables CameraRotation"), Space(5)] [SerializeField]
    private float _rotationOnX;

    [SerializeField] private float _rotationOnY;
    [SerializeField, Range(0f, 1f)] private float _sensibility = 0.5f; // Sensibilité

    [Space(5)] [SerializeField, Range(-75f, 0)]
    private float _limitVerticalCameraRotationMin = -45f; // Limite minimale

    [SerializeField, Range(0, 75f)] private float _limitVerticalCameraRotationMax = 45f; // Limite maximale
    [Space(5)]
    [SerializeField] private float currentVerticalRotation;

    
    
    void Update()
    {
        if (PlayerBrain.Instance.playerCanLookAround)
        {
            // ## ROTATION AXE Y #######################################################################################
            _rotationOnX = PlayerBrain.Instance.player.GetAxis("RotateOnX");
            _rotationOnY = PlayerBrain.Instance.player.GetAxis("RotateOnY");

            PlayerBrain.Instance.playerGameObject.transform.localEulerAngles += Vector3.up * (_rotationOnY * _sensibility);

            // ## ROTATION AXE X #######################################################################################
            currentVerticalRotation += (_rotationOnX * _sensibility);
            currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, _limitVerticalCameraRotationMin, _limitVerticalCameraRotationMax);

            // Appliquer la rotation avec clamp
            PlayerBrain.Instance.cinemachineTargetGameObject.transform.localEulerAngles = new Vector3(-currentVerticalRotation, 0f, 0f);
        }
    }
}

