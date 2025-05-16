using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("UI Elements"), Space(5)]
    public GameObject playerUI;
    public GameObject digicodeUI;
    public GameObject pillarUI;
    public GameObject labyrintheUI;
    public GameObject mirrorUI;
    public GameObject interactUI;
    public GameObject clueUI;
    public GameObject gumgumUI;
    
    [Header("References"), Space(5)]
    public GumUIManager gumUIManager;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ToggleTotalFreezePlayer()
    {
        PlayerBrain.Instance.playerCanMove = !PlayerBrain.Instance.playerCanMove;
        PlayerBrain.Instance.playerCanLookAround = !PlayerBrain.Instance.playerCanLookAround;
    }

    public void ToggleMovementFreezePlayer()
    {
        PlayerBrain.Instance.playerCanMove = !PlayerBrain.Instance.playerCanMove;
    }

    public void ToggleCameraFreezePlayer()
    {
        PlayerBrain.Instance.playerCanLookAround = !PlayerBrain.Instance.playerCanLookAround;
    }
}
