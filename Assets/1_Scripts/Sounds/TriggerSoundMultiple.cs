using UnityEngine;
using FMODUnity;
using System.Collections.Generic;

public class TriggerSoundMultiple : MonoBehaviour
{
    // Liste de sons à jouer, assignables dans l'inspecteur
    public List<EventReference> SoundList = new List<EventReference>();

    /// <summary>
    /// Joue un son spécifique par index.
    /// </summary>
    public void PlaySound(int index)
    {
        if (index >= 0 && index < SoundList.Count)
        {
            AudioManager.instance.PlayOneShot(SoundList[index], transform.position);
            Debug.Log("Son joué : " + SoundList[index].ToString());
        }
        else
        {
            Debug.LogWarning("Index de son invalide : " + index);
        }
    }

}