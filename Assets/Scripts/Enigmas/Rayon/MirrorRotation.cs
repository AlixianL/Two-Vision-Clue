using Rewired;
using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    public GameObject player;//---------------------> Joueur
    public GameObject reflector;//------------------> La partie du miroire qui tourne

    public KeyCode activationKey = KeyCode.E;
    public KeyCode _turnRight = KeyCode.E;
    public KeyCode _turnLeft = KeyCode.A;
    private bool _isPlayerInRange = false;//--------> Condition d'interaction avec le bouton


    void Update()
    {
        if (Input.GetKey(_turnRight))
        {
            RotateReflector(Vector3.up);
        }
        if (Input.GetKey(_turnLeft))
        {
            RotateReflector(-Vector3.up); 
        }
    }

    void RotateReflector(Vector3 direction)
    {
        float rotationSpeed = 100f;
        reflector.transform.Rotate(direction * rotationSpeed * Time.deltaTime);
    }
}
