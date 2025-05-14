using UnityEngine;


public class PlayerHandheldCamera : MonoBehaviour
{

    public bool _cam;
    private void Start()
    {
       _cam = false;
    }
    void Update()
    {
        // Poser la cam (Space)
        if (PlayerBrain.Instance.player.GetButtonDown("InstallCamera"))
        {
            HandheldCameraManager.Instance.InstallCamera();

            _cam = true;
           
        }
        
        
        // Reprendre la cam (R)
        if (PlayerBrain.Instance.player.GetButtonDown("DestroyCamera"))
        {
            HandheldCameraManager.Instance.UninstallCamera();
           
            _cam = false;
        }

        if (_cam)
        {
            //Debug.Log("Est actif");
        }

        else
        {
            //Debug.Log("n'est pas actif");
        }
        
    }
}