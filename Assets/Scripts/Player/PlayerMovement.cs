using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables PlayerMovement"), Space(5)]
    public int playerHealth;
    [Space(5)]
    public float lateralMovement;
    public float forwardMovement;
    public float playerSpeed;
    public float playerJumpForce;
    [Space(5)]
    public bool isGrounded;
    public bool isMoving;
    public bool isCrouching;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- MOVEMENT -------------------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        forwardMovement = PlayerBrain.Instance.player.GetAxis("ForwardMovement");
        lateralMovement = PlayerBrain.Instance.player.GetAxis("LateralMovement");

        if (forwardMovement != 0 || lateralMovement != 0)
        {
            isMoving = true;
        }
        else isMoving = false;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // Calculer la direction de déplacement en fonction de l'orientation locale
            Vector3 moveDirection = (transform.right * lateralMovement + transform.forward * forwardMovement).normalized * playerSpeed;

            // Conserver la vélocité verticale pour éviter de perturber le saut ou la gravité
            moveDirection.y = PlayerBrain.Instance.playerRigidbody.linearVelocity.y;

            // Appliquer la vélocité basée sur la direction locale et la vitesse de déplacement
            PlayerBrain.Instance.playerRigidbody.linearVelocity = moveDirection;
        }
        else if (!isMoving)
        {
            PlayerBrain.Instance.playerRigidbody.linearVelocity = new Vector3(0, PlayerBrain.Instance.playerRigidbody.linearVelocity.y, 0);
        }
    }
}
