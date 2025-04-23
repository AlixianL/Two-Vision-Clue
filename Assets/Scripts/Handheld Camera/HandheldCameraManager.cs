using UnityEngine;

public class HandheldCameraManager : MonoBehaviour
{
    public static HandheldCameraManager Instance;
    
    [Header("Camera References"), Space(5)]
    public GameObject handheldCameraPrefab;
    private GameObject handheldCamera;
    public GameObject spawnPoint;
    
    [Header("Variables"), Space(5)]
    public bool cameraIsInstall;
    public bool playerCanTakeCamera;
    

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void InstallCamera()
    {
        if (!cameraIsInstall)
        {
            handheldCamera = Instantiate(handheldCameraPrefab);
            handheldCamera.transform.position = spawnPoint.transform.position;
            
            cameraIsInstall = true;
        }
    }
    
    public void UnInstallCamera()
    {
        if (cameraIsInstall)
        {
            Destroy(handheldCamera);
            cameraIsInstall = false;
            playerCanTakeCamera = false;
        }
    }
}
