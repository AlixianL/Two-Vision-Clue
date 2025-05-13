using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class ListenerCamera : MonoBehaviour
{
    private Bus cam1Bus;
    private Bus cam2Bus;

    public Camera cam1;
    public Camera cam2;
    private Camera activeCam;

    public StudioListener tempListenerCam1;
    public StudioListener tempListenerCam2;

    void Start()
    {
        cam1Bus = RuntimeManager.GetBus("bus:/Cam1");
        cam2Bus = RuntimeManager.GetBus("bus:/Cam2");

        // Caméra active au lancement (ex: cam1)
        SetActiveCamera(cam1);
    }

    public void SetActiveCamera(Camera newCam)
    {
        activeCam = newCam;

        if (activeCam == cam1)
        {
            cam1Bus.setMute(false);
            cam2Bus.setMute(true);
        }
        else
        {
            cam1Bus.setMute(true);
            cam2Bus.setMute(false);
        }
    }

    // Exemple : switcher via une touche
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            /// <summary>
            /// 
            /// activeCam == cam1	    Condition : est-ce que cam1 est la caméra active ?
            /// ? cam2	                Si la condition est vraie → utilise cam2
            /// : cam1	                Sinon → utilise cam1
            /// SetActiveCamera(...)	Appelle la méthode SetActiveCamera avec le résultat de la condition
            ///
            /// </summary>
            SetActiveCamera(activeCam == cam1 ? cam2 : cam1);
        }
    }

    public void SwitchCamera()
    {
        /// <summary>
        /// 
        /// activeCam == cam1	    Condition : est-ce que cam1 est la caméra active ?
        /// ? cam2	                Si la condition est vraie → utilise cam2
        /// : cam1	                Sinon → utilise cam1
        /// SetActiveCamera(...)	Appelle la méthode SetActiveCamera avec le résultat de la condition
        ///
        /// </summary>
        SetActiveCamera(activeCam == cam1 ? cam2 : cam1);

        if (activeCam == cam1) tempListenerCam1.enabled = true;
        else tempListenerCam1.enabled = false;

        if (activeCam == cam2) tempListenerCam2.enabled = true;
        else tempListenerCam2.enabled = false;
    }
}
