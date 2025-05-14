using UnityEngine;
using System.Collections.Generic;

public class ExitPanel : MonoBehaviour, IActivatable
{
    [SerializeField] private List<GameObject> MainMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> OptionMenu = new List<GameObject>();


    public void Activate()
    {
        foreach (GameObject Panel in OptionMenu)
        {
            Panel.SetActive(false);
        }

        foreach (GameObject Panel in MainMenu)
        {
            Panel.SetActive(true);
        }
    }
}
