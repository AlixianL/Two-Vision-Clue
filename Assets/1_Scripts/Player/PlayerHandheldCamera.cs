using UnityEditor.UI;
using UnityEngine;

public class PlayerHandheldCamera : MonoBehaviour
{           
    private int installCameraCount = 0;
    void Update()
    {
        // Poser la cam
        if (PlayerBrain.Instance.player.GetButtonDown("InstallCamera"))
        {
            HandheldCameraManager.Instance.InstallCamera();
             installCameraCount++;
            Debug.Log("Caméra posée. Total posées: " + installCameraCount);
        }
        
        // Reprendre la cam
        if (PlayerBrain.Instance.player.GetButtonDown("DestroyCamera"))
        {
            HandheldCameraManager.Instance.UninstallCamera();
            Debug.Log("Caméra reprise.");
        }
    }
}