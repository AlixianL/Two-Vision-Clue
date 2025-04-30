using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headbob : MonoBehaviour
{
    public Animator camAnim;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            camAnim.SetTrigger("Walk");
        }
        else
        {
            camAnim.SetTrigger("Idle");
        }
    }
}