using System.Collections;
using UnityEngine;

public class RotateWeel : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _labyrinth;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private float _rotationSpeed;
    [Space(5)]
    [SerializeField] private bool _interactWhisEnigma;

    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();

        if (!_interactWhisEnigma)
        {
            _interactWhisEnigma = true;
        }
    }
    
    void Update()
    {
        if (_interactWhisEnigma)
        {
            // Rotation droite
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
            }

            // Rotation gauche
            else if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                transform.Rotate(0f, 0f, -_rotationSpeed * Time.deltaTime);
            }
            
            _labyrinth.transform.Rotate(0f, 0f, transform.rotation.z);
        }
    }
}
