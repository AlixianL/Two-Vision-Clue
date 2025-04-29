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
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
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
}
