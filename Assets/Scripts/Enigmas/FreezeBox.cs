using System;
using UnityEngine;

public class FreezeBox : MonoBehaviour
{
    [Header("Variables"), Space(5)]
    [SerializeField] private bool _playerIsInFreezeArea = false;
    [Space(5)]
    [SerializeField] private bool _freezeAll;
    [SerializeField] private bool _freezeMovement;
    [SerializeField] private bool _freezeCameraRotation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInFreezeArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInFreezeArea = false;
        }
    }
    
    private void Update()
    {
        // ~~ FREEZE TOTAL ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (_freezeAll && PlayerBrain.Instance.player.GetButtonDown("Interact"))
        {
            EnigmasManager.Instance.ToggleTotalFreezePlayer();
        }
            
        // ~~ FREEZE DU MOUVEMENT ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (_freezeMovement && PlayerBrain.Instance.player.GetButtonDown("Interact"))
        {
            EnigmasManager.Instance.ToggleMovementFreezePlayer();
        }
            
        // ~~ FREEZE DE LA CAMERA ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (_freezeCameraRotation && PlayerBrain.Instance.player.GetButtonDown("Interact"))
        {
            EnigmasManager.Instance.ToggleCameraFreezePlayer();
        }
    }
}
