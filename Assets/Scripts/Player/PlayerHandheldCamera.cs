using UnityEditor.UI;
using UnityEngine;

public class PlayerHandheldCamera : MonoBehaviour
{
    private bool _lookInHandheldCamera;

    void Start()
    {
        
    }
    
    void Update()
    {
        // Poser la cam
        if (PlayerBrain.Instance.player.GetButtonDown("InteractWhisCamera"))
        {
            HandheldCameraManager.Instance.InstallCamera();
        }

        // Reprendre la cam
        if (HandheldCameraManager.Instance.playerCanTakeCamera &&
            PlayerBrain.Instance.player.GetButtonDown("InteractWhisCamera"))
        {
            HandheldCameraManager.Instance.UnInstallCamera();
        }
    }
}