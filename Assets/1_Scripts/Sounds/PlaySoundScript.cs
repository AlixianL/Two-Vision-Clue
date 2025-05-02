using UnityEngine;
using FMODUnity;

public class PlaySoundScript : MonoBehaviour
{
    public FMODUnity.EventReference SoundActivate;

    private void Update()
    {
        
    }

    public void PlaySound()
    {
        AudioManager.instance.PlayOneShot(SoundActivate, this.transform.position);
    }
}
