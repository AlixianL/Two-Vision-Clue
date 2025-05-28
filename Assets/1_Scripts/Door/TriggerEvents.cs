using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    public void SwitchCameraOnPlayer()
    {
        ChangePositionCinemachine.Instance.ReturnOnPlayerCinemachineCamera(ChangePositionCinemachine.Instance._doorCinemachineCamera);
    }
}
