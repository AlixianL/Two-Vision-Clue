using UnityEngine;
using Rewired;

public class CameraRotation : MonoBehaviour
{ 
    [Header("Variables CameraRotation"), Space(5)]
    public float rotationOnX;
    public float rotationOnY;
    [Range(0f, 1f)]
    public float sensibility = 0.5f; // Sensibilité
    [Space(5)]
    public bool useHorizontalCameraRotation = true;
    public bool useVerticalCameraRotation = true;
    [Space(5)]
    [Range(-75f, 0)] public float limitVerticalCameraRotationMin = -45f; // Limite minimale
    [Range(0, 75f)] public float limitVerticalCameraRotationMax = 45f;  // Limite maximale
    public float currentVerticalRotation;
    
    void Update()
    {
        rotationOnX = PlayerBrain.Instance.player.GetAxis("RotateOnX");
        rotationOnY = PlayerBrain.Instance.player.GetAxis("RotateOnY");
        
        if (useHorizontalCameraRotation)
        {
            PlayerBrain.Instance.playerGameObject.transform.localEulerAngles += Vector3.up * (rotationOnY * sensibility);
        }
    
        if (useVerticalCameraRotation)
        {
            currentVerticalRotation += (rotationOnX * sensibility);
            currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, limitVerticalCameraRotationMin, limitVerticalCameraRotationMax);

            // Appliquer la rotation avec clamp
            PlayerBrain.Instance.cameraGameObject.transform.localEulerAngles = new Vector3(-currentVerticalRotation, 0f, 0f);
        }
        
    }
}

