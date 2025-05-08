using UnityEditor.UI;
using UnityEngine;

public class PlayerHandheldCamera : MonoBehaviour
{
    void Update()
    {
        // Poser la cam
        if (PlayerBrain.Instance.player.GetButtonDown("InstallCamera"))
        {
            HandheldCameraManager.Instance.InstallCamera();
            PlayerBrain.Instance.cameraBack.SetActive(true);
        }
        
        // Reprendre la cam
        if (PlayerBrain.Instance.player.GetButtonDown("DestroyCamera"))
        {
            HandheldCameraManager.Instance.UninstallCamera();
            PlayerBrain.Instance.cameraBack.SetActive(false);
        }
    }
}