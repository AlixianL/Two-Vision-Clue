using UnityEngine;

public class PlayerHandheldCamera : MonoBehaviour
<<<<<<< HEAD
{           
    private int installCameraCount = 0;

    //Sound-Design
    //---------------------------------
=======
{
    // Sound-Design
>>>>>>> origin/Master-Xian
    public TriggerSoundMultiple triggerSoundMultiple;

    void Update()
    {
        if (PlayerBrain.Instance.player.GetButtonDown("InstallCamera"))
        {
<<<<<<< HEAD
            HandheldCameraManager.Instance.InstallCamera();
             installCameraCount++;
            //Sound-Design
            //---------------------------------
            triggerSoundMultiple.PlaySound(2);

        }
        
        // Reprendre la cam
        if (PlayerBrain.Instance.player.GetButtonDown("DestroyCamera"))
        {
            HandheldCameraManager.Instance.UninstallCamera();
            //Sound-Design
            //---------------------------------
            triggerSoundMultiple.PlaySound(1);
=======
            if (HandheldCameraManager.Instance.cameraIsInstall)
            {
                HandheldCameraManager.Instance.UninstallCamera();
                triggerSoundMultiple.PlaySound(1); // Son pour reprise
                AudioManager.instance.listenerCamera.SwitchCamera();

                AudioManager.instance.listenerCamera.cam2 = null;
                AudioManager.instance.listenerCamera.tempListenerCam2 = null;
            }
            else
            {
                HandheldCameraManager.Instance.InstallCamera();
                triggerSoundMultiple.PlaySound(2); // Son pour pose
                AudioManager.instance.listenerCamera.SwitchCamera();
            }
>>>>>>> origin/Master-Xian
        }
    }
}