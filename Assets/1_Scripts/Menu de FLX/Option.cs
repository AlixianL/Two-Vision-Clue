using UnityEngine;
using System.Collections.Generic;

public class Option : MonoBehaviour, IActivatable
{
    [SerializeField] private List<GameObject> MainMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> OptionMenu = new List<GameObject>();

    //Sound-Design
    //---------------------------------
    public TriggerSoundMultiple triggerSoundMultiple;


    public void Activate()
    {
        //Sound-Design
        //---------------------------------
        triggerSoundMultiple.PlaySound(0);

        foreach (GameObject Panel in MainMenu)
        {
            Panel.SetActive(false);
        }

        foreach (GameObject Panel in OptionMenu)
        {
            Panel.SetActive(true);
        }
    }
}
