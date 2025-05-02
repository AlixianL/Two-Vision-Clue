using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class ChangePositionCinemachine : MonoBehaviour
{
    public static ChangePositionCinemachine Instance;
    
    [Header("Renfereces"), Space(5)]
    [SerializeField] private CinemachineCamera _cinemachineCameraClue_01;
    [SerializeField] private CinemachineCamera _cinemachineCameraClue_02;
    [SerializeField] private CinemachineCamera _cinemachineCameraClue_03;
    [SerializeField] private CinemachineCamera _cinemachineCameraClue_04;

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
    public void SwitchCam(CinemachineCamera enigmaCineCame, bool isNotOnPlayerCineCam)
    {
        if (isNotOnPlayerCineCam)
        {
            enigmaCineCame.Priority = 2;
        }

        else
        {
            enigmaCineCame.Priority = 0;
        }
    }
}
