using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public ListenerCamera listenerCamera;

    private Dictionary<string, EventInstance> activeSounds = new Dictionary<string, EventInstance>();

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlaySound(EventReference sound, string soundKey, Vector3 worldPos)
    {
        if (activeSounds.ContainsKey(soundKey))
        {
            return;
        }

        EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(worldPos));
        instance.start();

        activeSounds.Add(soundKey, instance);
    }

    public void StopSound(string soundKey)
    {
        if (activeSounds.TryGetValue(soundKey, out var instance))
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
            activeSounds.Remove(soundKey);
        }
    }
}