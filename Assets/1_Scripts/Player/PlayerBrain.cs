using UnityEngine;
using Rewired;

public class PlayerBrain : MonoBehaviour
{
    public static PlayerBrain Instance;
    
    [Header("References"), Space(5)]
    public Rigidbody playerRigidbody;
    public GameObject playerGameObject;
    public GameObject cinemachineTargetGameObject;

    [Header("UI References"), Space(5)]
    public GameObject cameraBack;
    
    [Header("Player Scripts"), Space(5)]
    public PlayerMovement playerMovement;
    public CameraRotation cameraRotation;
    public PlayerHandheldCamera playerHandheldCamera;
    public PlayerInteractionSystem playerInteractionSystem;
    
    [Header("Variables"), Space(5)]
    public int chewingGumCount;
    [Space(5)]
    public float height;
    public float radius;
    [Space(5)]
    public bool isGrounded;
    public bool asAlreadyTalkWhisGumGum = false;
    [Space(5)]
    public bool playerCanMove = true;
    public bool playerCanLookAround = true;
    
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
        }
        else
        {
            isGrounded = false;
        }
    }
}
    