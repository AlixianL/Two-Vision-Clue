using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CluePosition : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    public List<GameObject> clues = new List<GameObject>();
    public CinemachineCamera clueCinemachineCamera;

    [Header("Settings"), Space(5)]
    public float distanceFromCenter;
    
    [Header("Variables"), Space(5)]
    public bool _playerIsInteracting = false;
    

    /// <summary>
    /// Appelle cette méthode pour répartir les indices autour du centre
    /// </summary>
    public void UpdatePosition()
    {
        if (clues == null || clues.Count == 0)
            return;

        float angleStep = 360f / clues.Count;

        for (int i = 0; i < clues.Count; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(
                Mathf.Cos(rad) * distanceFromCenter,
                0f,
                Mathf.Sin(rad) * distanceFromCenter
            );

            clues[i].transform.position = transform.position + offset;
        }
    }
    
    public void Activate()
    {
        _playerIsInteracting = !_playerIsInteracting;
        ChangePositionCinemachine.Instance.SwitchCam(clueCinemachineCamera, _playerIsInteracting);
        GameManager.Instance.ToggleTotalFreezePlayer();
    }
    
    // Pour tester directement depuis l'inspecteur (facultatif)
    private void OnValidate()
    {
        UpdatePosition();
    }
}