using Unity.Cinemachine;
using UnityEngine;

public class ExtincteurEasterEgg : MonoBehaviour,IActivatable
{
    private bool _interactWithEnigma = false;
    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;


    public void Activate()
    {

        if (!_interactWithEnigma)
        {
            _interactWithEnigma = true;
        }
        else _interactWithEnigma = false;

        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWithEnigma);

        GameManager.Instance.playerUI.SetActive(!_interactWithEnigma);
        GameManager.Instance.mirrorUI.SetActive(_interactWithEnigma);
    }
}
