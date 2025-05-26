using System.Collections.Generic;
using UnityEngine;

public class LIGHTON : MonoBehaviour, IActivatable
{
    [SerializeField] private List<Light> LightOfRoom = new List<Light>();


    void Start()
    {
        foreach (Light light in LightOfRoom)
        {
            light.enabled = false;
        }
    }
    public void Activate()
    {
        foreach (Light light in LightOfRoom)
        {
            light.enabled = true;

        }
        GumGumManager.Instance.dialogueTrigger.TriggerDialogue();

        GumGumManager.Instance.isInteracting = true;

    }
    

}
