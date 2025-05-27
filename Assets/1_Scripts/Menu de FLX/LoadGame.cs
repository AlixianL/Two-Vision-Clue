using UnityEngine;
using System.Collections.Generic;

public class LoadGame : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    [SerializeField] private List<GameObject> MainMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> loadGamePanel = new List<GameObject>();

    public TriggerSoundMultiple triggerSoundMultiple;

    public void Activate()
    {
        foreach (GameObject Panel in MainMenu)
        {
            Panel.SetActive(false);
            triggerSoundMultiple.PlaySound(0);
        }
        
        foreach (GameObject Panel in loadGamePanel)
        {
            Panel.SetActive(true);
        }
    }
}
