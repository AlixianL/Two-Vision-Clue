using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("References"), Space(5)]
    public GumGum gumGum;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private bool _isInRange;
    
    
    void Update()
    {
        if (_isInRange && PlayerBrain.Instance.player.GetButtonDown("Interact"))
        {
            TriggerDialoque();
            GumGumManager.Instance._GumGumPanel.SetActive(true);
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerBrain.Instance.cameraRotation.useVerticalCameraRotation = false;
            PlayerBrain.Instance.cameraRotation.useHorizontalCameraRotation = false;
        }
    }

    void TriggerDialoque()
    {
        GumGumManager.Instance.StartDialogue(gumGum);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isInRange = false;
            GumGumManager.Instance._GumGumPanel.SetActive(false);
        
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerBrain.Instance.cameraRotation.useVerticalCameraRotation = true;
            PlayerBrain.Instance.cameraRotation.useHorizontalCameraRotation = true;
        }
    }
}
