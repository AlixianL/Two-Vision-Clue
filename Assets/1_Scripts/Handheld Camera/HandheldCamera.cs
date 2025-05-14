using FMODUnity;
using UnityEngine;

public class HandheldCamera : MonoBehaviour
{
    private BoxCollider _boxCollider;
    [SerializeField] private Camera _camera;
    [SerializeField] private StudioListener _audioListener;


    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, PlayerBrain.Instance.transform.rotation.eulerAngles.y - 90f, 0);
        AudioManager.instance.listenerCamera.cam2 = _camera;
        AudioManager.instance.listenerCamera.tempListenerCam2 = _audioListener;
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HandheldCameraManager.Instance.playerCanTakeCamera = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            HandheldCameraManager.Instance.playerCanTakeCamera = false;
        }
    }*/
}
