using UnityEngine;

public class EnigmasManager : MonoBehaviour
{
    public static EnigmasManager Instance;

    //[Header("References"), Space(5)]

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