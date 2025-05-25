using UnityEngine;
using FMODUnity;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private EventReference soundEvent;

    
    public void PlaySound()
    {
        RuntimeManager.PlayOneShot(soundEvent, transform.position);
    }
}
