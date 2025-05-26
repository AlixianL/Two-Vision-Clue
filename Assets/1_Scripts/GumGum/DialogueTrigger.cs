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
        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;
        GameManager.Instance.playerUI.gameObject.SetActive(false);
        PlayerBrain.Instance.playerInteractionSystem.playerCanInteractWhithMouse = false;
        
        if (PlayerBrain.Instance.asAlreadyTalkWhisGumGum)
        {
            GumGumManager.Instance.isInteracting = true;
            ChangePositionCinemachine.Instance.SwitchCam(GumGumManager.Instance.gumgumCinemachineCamera, GumGumManager.Instance.isInteracting);
            GumGumManager.Instance.GumGumAsksThePlayerWhichHesBlockingOn();
        }
        else
        {
            GumGumManager.Instance.isInteracting = true;
            ChangePositionCinemachine.Instance.SwitchCam(GumGumManager.Instance.gumgumCinemachineCamera, GumGumManager.Instance.isInteracting);
            GumGumManager.Instance.GumGumPresentHimself();
            PlayerBrain.Instance.asAlreadyTalkWhisGumGum = true;
        }
    }
}
