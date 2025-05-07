using Rewired;
using System.Collections;
using UnityEngine;

public class MirrorRotation : MonoBehaviour, IActivatable
{
    public GameObject player;//----------------------------> Joueur
    public GameObject reflector;//-------------------------> La partie du miroire qui tourne

    [SerializeField] private bool _interactWhisEnigma = false;
    [SerializeField] private bool _enigmaisend;


    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();

        if (!_interactWhisEnigma)
        {
            _interactWhisEnigma = true;
        }
        else _interactWhisEnigma = false;
    }
    void Update()
    {

        if (_interactWhisEnigma && !_enigmaisend)
        {
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                RotateReflector(Vector3.up);
            }
            if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                RotateReflector(-Vector3.up);
            }
        }
        
    }

    void RotateReflector(Vector3 direction)
    {
        float rotationSpeed = 100f;
        reflector.transform.Rotate(direction * rotationSpeed * Time.deltaTime);
    }

    public void FreezMirror()
    {
        Debug.Log("ca freez");
        _enigmaisend = true;
    }
}
