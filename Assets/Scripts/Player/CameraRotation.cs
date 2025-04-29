using UnityEngine;
using Rewired;

public class CameraRotation : MonoBehaviour
{ 
    [Header("Variables CameraRotation"), Space(5)]
    [SerializeField] private float _rotationOnX;
    [SerializeField] private float _rotationOnY;
    [SerializeField, Range(0f, 1f)] private float _sensibility = 0.5f; // Sensibilité
    [Space(5)]
    public bool useHorizontalCameraRotation = true;
    public bool useVerticalCameraRotation = true;
    [Space(5)]
    [SerializeField, Range(-75f, 0)] private float _limitVerticalCameraRotationMin = -45f; // Limite minimale
    [SerializeField, Range(0, 75f)] private float _limitVerticalCameraRotationMax = 45f;  // Limite maximale
    [SerializeField] private float currentVerticalRotation;
    
    void Update()
    {
        _rotationOnX = PlayerBrain.Instance.player.GetAxis("RotateOnX");
        _rotationOnY = PlayerBrain.Instance.player.GetAxis("RotateOnY");
        
        if (useHorizontalCameraRotation)
        {
            PlayerBrain.Instance.playerGameObject.transform.localEulerAngles += Vector3.up * (_rotationOnY * _sensibility);
        }
    
        if (useVerticalCameraRotation)
        {
            currentVerticalRotation += (_rotationOnX * _sensibility);
            currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, _limitVerticalCameraRotationMin, _limitVerticalCameraRotationMax);

            // Appliquer la rotation avec clamp
            PlayerBrain.Instance.cinemachineGameObject.transform.localEulerAngles = new Vector3(-currentVerticalRotation, 0f, 0f);
        }
        
    }
}

