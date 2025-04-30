using UnityEngine;
using Rewired;

public class PlayerBrain : MonoBehaviour
{
    public static PlayerBrain Instance;
    
    [Header("References"), Space(5)]
    public Rigidbody playerRigidbody;
    public GameObject playerGameObject;
    public GameObject cinemachineTargetGameObject;
    
    [Header("Player Scripts"), Space(5)]
    public PlayerMovement playerMovement;
    public CameraRotation cameraRotation;
    public PlayerHandheldCamera playerHandheldCamera;
    
    [Header("Variables"), Space(5)]
    public int chewingGumCount;
    [Space(5)]
    public bool isAlive;
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
}
    