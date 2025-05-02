using UnityEngine;
using Rewired;

public class PlayerBrain : MonoBehaviour
{
    public static PlayerBrain Instance;
    
    [Header("References"), Space(5)]
    public Rigidbody playerRigidbody;
    public Animator playerAnimator;
    public GameObject playerGameObject;
    public GameObject cameraGameObject;
    public GameObject CluesInteractPosition;
    
    [Header("Player Scripts"), Space(5)]
    public PlayerMovement playerMovement;
    public CameraRotation cameraRotation;
    public PlayerHandheldCamera playerHandheldCamera;
    
    [Header("Variables"), Space(5)]
    public bool isGrounded;
    public float height;
    public float velocity;
    public float radius;

    [Header("Variables"), Space(5)]
    public bool asAlreadyTalkWhisGumGum = false;
    
    [Header("Rewired"), Space(5)]
    public int playerID;
    public Player player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        
        player = ReInput.players.GetPlayer(playerID);
    }


    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, height))
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            isGrounded = false;
            Debug.Log("Not Grounded!");
        }
    }
}
    