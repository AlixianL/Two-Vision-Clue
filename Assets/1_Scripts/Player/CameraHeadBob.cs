using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headbob : MonoBehaviour
{
    public Animator camAnim;

    void Update()
    {
        // Mises à jour des directions basées sur les boutons
        bool _forwardMovement = PlayerBrain.Instance.player.GetButton("ForwardMovement");
        bool _backwardMovement = PlayerBrain.Instance.player.GetButton("BackwardMovement");
        bool _leftMovement = PlayerBrain.Instance.player.GetButton("LeftMovement");
        bool _rightMovement = PlayerBrain.Instance.player.GetButton("RightMovement");

        // Déterminer si le joueur bouge
        bool isMoving = _forwardMovement || _backwardMovement || _leftMovement || _rightMovement;
        
        if (isMoving)
        {
            camAnim.SetTrigger("Walk");
        }
        else
        {
            camAnim.SetTrigger("Idle");
        }
    }
}