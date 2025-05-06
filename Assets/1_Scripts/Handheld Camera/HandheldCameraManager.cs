using System;
using UnityEngine;

public class HandheldCameraManager : MonoBehaviour
{
    public static HandheldCameraManager Instance;
    
    [Header("Camera References"), Space(5)]
    private GameObject handheldCamera;
    public GameObject handheldCameraPrefab;
    public GameObject spawnPoint;
    
    [Header("Variables"), Space(5)]
    public bool cameraIsInstall;
    public bool playerCanTakeCamera;
    public bool cameraCanBeInstalled;
    

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    public void InstallCamera()
    {
        if (cameraCanBeInstalled && !cameraIsInstall)
        {
            handheldCamera = Instantiate(handheldCameraPrefab);
            handheldCamera.transform.position = spawnPoint.transform.position;
            cameraIsInstall = true;
        }
    }
    
    public void UninstallCamera()
    {
        if (cameraIsInstall)
        {
            Destroy(handheldCamera);
            cameraIsInstall = false;
            playerCanTakeCamera = false;
            cameraCanBeInstalled = true;
        }
    }
}
