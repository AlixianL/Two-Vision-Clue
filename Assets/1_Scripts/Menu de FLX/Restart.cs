using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour,IActivatable
{
    [SerializeField] private Pause _unpause;
    [SerializeField] private HandheldCameraManager _handheldCameraManager;
    [SerializeField] private GameObject _cameraPanel;
    
    public void Activate()
    {
        GameManager.Instance.ToggleMovementFreezePlayer();
        _cameraPanel.SetActive(false);
        _handheldCameraManager.cameraCanBeInstalled = false;
        
        StartCoroutine(SceneLoader.Instance.LoadScene(SceneLoader.Instance.sceneToLoad));
        _unpause.TogglePauseMenu();
        Debug.Log("je recharge la sc�ne");
    }
}
