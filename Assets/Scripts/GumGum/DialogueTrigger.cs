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
        }
    }

    void TriggerDialoque()
    {
        GumGumManager.Instance.StartDialogue(gumGum);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
}
