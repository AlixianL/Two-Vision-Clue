using Rewired;
using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    public GameObject player;//---------------------> Joueur
    public GameObject reflector;//------------------> La partie du miroire qui tourne

    public KeyCode activationKey = KeyCode.E;
    private bool _isPlayerInRange = false;//--------> Condition d'interaction avec le bouton


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            _isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            _isPlayerInRange = false;
        }
    }
    void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(activationKey))
        {
            
        }
    }
}
