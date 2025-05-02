using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class QuitClueCinemachineCamera : MonoBehaviour, IActivatable
{
    [Header("Cinemachine Camera"), Space(5)]
    [SerializeField] private CinemachineCamera _cinemachineCamera;

    public void Activate()
    {
        GumGumManager.Instance.ToggleCollider();
        ReturnToPlayerCinemachineCamera();
        StartCoroutine(Delay());
        GameManager.Instance.ToggleTotalFreezePlayer();
    }

    public void ReturnToPlayerCinemachineCamera()
    {
        _cinemachineCamera.Priority = 0;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
