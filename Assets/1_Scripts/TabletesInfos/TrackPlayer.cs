using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(PlayerBrain.Instance.cinemachineTargetGameObject.transform);
    }
}
