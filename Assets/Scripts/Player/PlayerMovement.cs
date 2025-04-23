using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables PlayerMovement"), Space(5)]
    public float playerSpeed;
    [Space(5)]
    private bool _forwardMovement;
    private bool _backwardMovement;
    private bool _leftMovement;
    private bool _rightMovement;
    public bool isMoving;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- MOVEMENT -------------------------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Update()
    {
        // Mises à jour des directions basées sur les boutons
        _forwardMovement = PlayerBrain.Instance.player.GetButton("ForwardMovement");
        _backwardMovement = PlayerBrain.Instance.player.GetButton("BackwardMovement");
        _leftMovement = PlayerBrain.Instance.player.GetButton("LeftMovement");
        _rightMovement = PlayerBrain.Instance.player.GetButton("RightMovement");

        // Déterminer si le joueur bouge
        isMoving = _forwardMovement || _backwardMovement || _leftMovement || _rightMovement;
    }
    
    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;

        if (_forwardMovement) direction += transform.forward;
        if (_backwardMovement) direction -= transform.forward;
        if (_rightMovement) direction += transform.right;
        if (_leftMovement) direction -= transform.right;

        direction.Normalize();

        Vector3 velocity = direction * playerSpeed;
        velocity.y = PlayerBrain.Instance.playerRigidbody.linearVelocity.y;

        PlayerBrain.Instance.playerRigidbody.linearVelocity = velocity;
    }
}
