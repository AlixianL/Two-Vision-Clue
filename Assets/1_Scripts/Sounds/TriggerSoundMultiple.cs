using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public class TriggerSoundMultiple : MonoBehaviour
{
    // Liste de sons � jouer, assignables dans l'inspecteur
    public List<EventReference> SoundList = new List<EventReference>();

    /// <summary>
    /// Joue un son sp�cifique par index.
    /// </summary>
    public void PlaySound(int index)
    {
        if (index >= 0 && index < SoundList.Count)
        {
            AudioManager.instance.PlayOneShot(SoundList[index], transform.position);
        }
    }

}