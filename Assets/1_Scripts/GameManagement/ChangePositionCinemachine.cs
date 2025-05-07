using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class ChangePositionCinemachine : MonoBehaviour
{
    public static ChangePositionCinemachine Instance;
    
    [Header("Renfereces"), Space(5)]
    public CinemachineCamera _playerCinemachineCamera;
    public CinemachineCamera _gumgumCinemachineCamera;
    public CinemachineCamera _digicodeCinemachineCamera;
    public CinemachineCamera _doorCinemachineCamera;
    public CinemachineCamera _labyrinthCinemachineCamera;
    public CinemachineCamera _pilerCinemachineCamera;
    public CinemachineCamera _laserCinemachineCamera;
    public CinemachineCamera _clue_01CinemachineCamera;
    public CinemachineCamera _clue_02CinemachineCamera;
    public CinemachineCamera _clue_03CinemachineCamera;
    public CinemachineCamera _clue_04CinemachineCamera;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    /// <summary>
    ///
    /// Permet de swtich en fonction de la valeur bouléenne de "isNotOnPlayerCineCam"
    /// Plus l'indice est élévé, plus la caméra est prioritaire
    /// La cinémachine du joueur a une priorité de 1
    /// 
    /// </summary>
    /// <param name="enigmaCineCame"></param> 
    /// <param name="isOnEnigmaCam"></param>
    public void SwitchCam(CinemachineCamera targetCineCame, bool isNotOnPlayerCineCam)
    {
        if (isNotOnPlayerCineCam)
        {
            targetCineCame.Priority = 2;
        }

        else
        {
            targetCineCame.Priority = 0;
        }
    }

    public void SwitchIntoClueCinemachineCamera(CinemachineCamera gumgumCinemachineCamera, CinemachineCamera targetCinemachineCamera)
    {
        gumgumCinemachineCamera.Priority = 0;
        targetCinemachineCamera.Priority = 2;
    }

    public void SwitchIntoDoorCinemachineCamera(CinemachineCamera digicodeCinemachineCamera, CinemachineCamera doorCinemachineCamera)
    {
        digicodeCinemachineCamera.Priority = 0;
        doorCinemachineCamera.Priority = 2;
    }
    
    public void ReturnOnPlayerCinemachineCamera(CinemachineCamera currentCinemachineCamera)
    {
        currentCinemachineCamera.Priority = 0;
    }
}
