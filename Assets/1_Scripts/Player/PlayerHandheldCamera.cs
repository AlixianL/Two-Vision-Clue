using UnityEngine;

public class PlayerHandheldCamera : MonoBehaviour
{
    // Sound-Design
    public TriggerSoundMultiple triggerSoundMultiple;

    void Update()
    {
        if (PlayerBrain.Instance.player.GetButtonDown("InstallCamera"))
        {
            if (HandheldCameraManager.Instance.cameraIsInstall)
            {
                HandheldCameraManager.Instance.UninstallCamera();
                triggerSoundMultiple.PlaySound(1); // Son pour reprise
            }
            else
            {
                HandheldCameraManager.Instance.InstallCamera();
                triggerSoundMultiple.PlaySound(2); // Son pour pose
            }
        }
    }
}