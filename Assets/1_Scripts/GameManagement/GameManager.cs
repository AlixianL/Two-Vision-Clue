using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerUI;
    public GameObject digicodeUI;
    public GameObject pillarUI;
    public GameObject labyrintheUI;
    public GameObject mirrorUI;
    public GameObject interactUI;
    
    
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
