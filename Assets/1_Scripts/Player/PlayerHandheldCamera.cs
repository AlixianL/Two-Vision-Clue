using UnityEngine;

public class PlayerHandheldCamera : MonoBehaviour
{           
    private int installCameraCount = 0;

    //Sound-Design
    //---------------------------------
    public TriggerSoundMultiple triggerSoundMultiple;

    void Update()
    {
        // Poser la cam
        if (PlayerBrain.Instance.player.GetButtonDown("InstallCamera"))
        {
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
        }
    }
}