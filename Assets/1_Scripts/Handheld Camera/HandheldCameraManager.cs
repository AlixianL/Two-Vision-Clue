using System;
using UnityEngine;

public class HandheldCameraManager : MonoBehaviour
{
    public static HandheldCameraManager Instance;
    
    [Header("Camera References"), Space(5)]
    [SerializeField] private HandhledCameraShake _handhledCameraShake;
    private GameObject handheldCamera;
    public GameObject handheldCameraPrefab;
    public GameObject spawnPoint;
    public GameObject _cameraToDestroy;
    

    [Header("Variables"), Space(5)]
    public bool cameraIsInstall;
    public bool playerCanTakeCamera;
    public bool cameraCanBeInstalled;
    public bool isPlaying;



    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    public void InstallCamera()
    {
        if (isPlaying)
        {
            if (cameraCanBeInstalled && !cameraIsInstall)
            {
                if (_cameraToDestroy != null)
                {
                    Destroy(_cameraToDestroy);
                    _cameraToDestroy = null;
                }
                if (handheldCamera != null) Destroy(handheldCamera);
                handheldCamera = Instantiate(handheldCameraPrefab);

                handheldCamera.transform.position = spawnPoint.transform.position;
                cameraIsInstall = true;
                PlayerBrain.Instance.cameraBack.SetActive(true);
                
                playerCanTakeCamera = cameraIsInstall;
            }
            
            if (!cameraCanBeInstalled && !cameraIsInstall) _handhledCameraShake.TriggerAnimation();
        }
        
    }
    
    public void UninstallCamera()
    {
        if (isPlaying)
        {
            if (_cameraToDestroy != null)
            {
                Destroy(_cameraToDestroy);
                _cameraToDestroy = null;
            }
            if (cameraIsInstall)
            {
                Destroy(handheldCamera);
                cameraIsInstall = false;
                playerCanTakeCamera = false;
                cameraCanBeInstalled = true;
                PlayerBrain.Instance.cameraBack.SetActive(false);
            }
        }
        
    }
}
