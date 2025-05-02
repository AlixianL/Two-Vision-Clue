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
}