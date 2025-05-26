using UnityEngine;
using FMODUnity;

public class CameraAudioController : MonoBehaviour
{
    public Camera playerCamera;
    public StudioListener playerListener;

    public Camera surveillanceCamera;
    public StudioListener surveillanceListener;

    void Start()
    {
        // Activer la caméra joueur par défaut
        SwitchToPlayer();
    }

    public void SwitchToPlayer()
    {
        playerCamera.enabled = true;
        surveillanceCamera.enabled = false;

        playerListener.enabled = true;
        surveillanceListener.enabled = false;
    }

    public void SwitchToSurveillance()
    {
        playerCamera.enabled = false;
        surveillanceCamera.enabled = true;

        playerListener.enabled = false;
        surveillanceListener.enabled = true;
    }
}
