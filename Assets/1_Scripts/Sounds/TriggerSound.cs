using UnityEngine;
using FMODUnity;

public class TriggerSound : MonoBehaviour
{

    public FMODUnity.EventReference SoundActivate;

    public void PlaySound()
    {
        AudioManager.instance.PlayOneShot(SoundActivate, this.transform.position);
        Debug.Log("oui");
    }
}