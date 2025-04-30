using UnityEngine;

public class HandheldCamera : MonoBehaviour
{
    private BoxCollider _boxCollider;
    
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, PlayerBrain.Instance.transform.rotation.eulerAngles.y - 90f, 0);
    }

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
    }
}
