using UnityEngine;
using System.Collections.Generic;

public class SaveGame : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    [SerializeField] private List<GameObject> pauseMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> saveGamePanel = new List<GameObject>();
    
    public void Activate()
    {
        foreach (GameObject Panel in pauseMenu)
        {
            Panel.SetActive(false);
        }
        
        foreach (GameObject Panel in saveGamePanel)
        {
            Panel.SetActive(true);
        }
    }
}
