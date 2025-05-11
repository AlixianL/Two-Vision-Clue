using UnityEngine;

public class PlayerHandheldCamera : MonoBehaviour
{
    void Update()
    {
        // Poser la cam
        if (PlayerBrain.Instance.player.GetButtonDown("InstallCamera"))
        {
            HandheldCameraManager.Instance.InstallCamera();
        }
        
        // Reprendre la cam
        if (PlayerBrain.Instance.player.GetButtonDown("DestroyCamera"))
        {
            HandheldCameraManager.Instance.UninstallCamera();
        }
    }
}