using System;
using UnityEngine;

public class CluesRayCast : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Transform _rayOrigine;
    [SerializeField] private Transform _rayDirection;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ClueInteraction _clueInteraction;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private bool _playerIsInRange;

    private void Update()
    {
        if (_playerIsInRange)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(_rayOrigine.position, _rayDirection.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, _layerMask))
            { 
                Debug.DrawRay(_rayOrigine.position, _rayDirection.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                
                if (hit.collider.gameObject.layer == 7 && PlayerBrain.Instance.player.GetButtonDown("Interact"))
                {
                    GumGumManager.Instance.ActivateGumGum();
                }

                else if (hit.collider.gameObject.layer == 6 && PlayerBrain.Instance.player.GetButtonDown("Interact"))
                {
                    _clueInteraction.InteractWhisClues();
                }
            }
            else
            { 
                Debug.DrawRay(_rayOrigine.position, _rayDirection.TransformDirection(Vector3.forward) * 1000, Color.blue);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIsInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIsInRange = false;
        }
    }
}
