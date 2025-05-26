using UnityEngine;
using FMODUnity;
using FMOD;
using FMOD.Studio;

public class HandheldCamera : MonoBehaviour
{
    private BoxCollider _boxCollider;
    [SerializeField] private Camera _camera;

    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, PlayerBrain.Instance.transform.rotation.eulerAngles.y - 90f, 0);

        AudioManager.instance.listenerCamera.cam2 = _camera;
        AudioManager.instance.listenerCamera.tempListenerCam2 = _camera.GetComponent<StudioListener>();
    }
}