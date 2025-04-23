using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private void Update()
    {
        if (GumGumManager.Instance._isInRange && PlayerBrain.Instance.player.GetButtonDown("Interact"))
        {
            TriggerDialoque();
        }
    }

    public void TriggerDialoque()
    {
        GumGumManager.Instance.gumGumPanel.SetActive(true);
            
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerBrain.Instance.cameraRotation.useVerticalCameraRotation = false;
        PlayerBrain.Instance.cameraRotation.useHorizontalCameraRotation = false;
        
        if (PlayerBrain.Instance.asAlreadyTalkWhisGumGum)
        {
            GumGumManager.Instance.GumGumAsksThePlayerWhichHesBlockingOn();
        }
        else
        {
            GumGumManager.Instance.GumGumPresentHimself();
            PlayerBrain.Instance.asAlreadyTalkWhisGumGum = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GumGumManager.Instance._isInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GumGumManager.Instance._isInRange = false;
            GumGumManager.Instance.gumGumPanel.SetActive(false);
        
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerBrain.Instance.cameraRotation.useVerticalCameraRotation = true;
            PlayerBrain.Instance.cameraRotation.useHorizontalCameraRotation = true;
        }
    }
}
