using System;
using UnityEngine;

public class CluesRayCast : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Transform _rayOrigine;
    [SerializeField] private Transform _rayDirection;
    [SerializeField] private LayerMask _layerMask => LayerMask.GetMask("Player", "Environement");
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(_rayOrigine.position, _rayDirection.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, _layerMask))

            { 
                Debug.DrawRay(_rayOrigine.position, _rayDirection.TransformDirection(Vector3.forward) * hit.distance, Color.red); 
                Debug.Log("Did Hit"); 
            }
            else
            { 
                Debug.DrawRay(_rayOrigine.position, _rayDirection.TransformDirection(Vector3.forward) * 1000, Color.blue); 
                Debug.Log("Did not Hit"); 
            }
        }
    }
}
