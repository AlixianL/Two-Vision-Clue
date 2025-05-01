using System;
using UnityEngine;
using Unity.Cinemachine;

public class ChangePositionCinemachine : MonoBehaviour
{
    public static ChangePositionCinemachine Instance;
    
    //public CinemachineCamera playerCinemachineCamera;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    /// <summary>
    ///
    /// Permet de swtich en fonction de la valueur bouléenne de "isOnEnigmaCam"
    /// Plus l'indice est élévé, plus la caméra est prioritaire 
    /// 
    /// </summary>
    /// <param name="enigmaCineCame"></param>
    /// <param name="isOnEnigmaCam"></param>
    public void SwitchCam(CinemachineCamera enigmaCineCame, bool isOnEnigmaCam)
    {
        if (isOnEnigmaCam)
        {
            enigmaCineCame.Priority = 10;
        }

        else
        {
            enigmaCineCame.Priority = 0;
        }
    }
}
