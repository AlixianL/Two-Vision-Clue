using System;
using UnityEngine;

public class HandheldCameraManager : MonoBehaviour
{
    public static HandheldCameraManager Instance;
    
    [Header("Camera References"), Space(5)]
    private GameObject handheldCamera;
    public GameObject handheldCameraPrefab;
    public GameObject spawnPoint;

    [SerializeField] public GameObject _cameraToDestroy;


    [Header("Variables"), Space(5)]
    public bool cameraIsInstall;
    public bool playerCanTakeCamera;
    public bool cameraCanBeInstalled;
    
    //SoundDesign

    

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    public void InstallCamera()
    {
        if (cameraCanBeInstalled && !cameraIsInstall)
        {
            if (handheldCamera != null) Destroy(handheldCamera);
            handheldCamera = Instantiate(handheldCameraPrefab);
            cameraCanBeInstalled = false;
            handheldCamera.transform.position = spawnPoint.transform.position;
            cameraIsInstall = true;
            PlayerBrain.Instance.cameraBack.SetActive(true);
            AudioManager.instance.listenerCamera.SwitchCamera();

        }
    }
    
    public void UninstallCamera()
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
            cameraCanBeInstalled = true;
            PlayerBrain.Instance.cameraBack.SetActive(false);
            AudioManager.instance.listenerCamera.cam2 = null;
            AudioManager.instance.listenerCamera.SwitchCamera();
        }
    }
}
