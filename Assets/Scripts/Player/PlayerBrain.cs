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
    
    [Header("Variables"), Space(5)]
    public bool isAlive;
    
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
