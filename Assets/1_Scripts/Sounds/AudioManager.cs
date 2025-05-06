using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    //Permet de savoir si il y a plus de 1 AudioManager dans la scène.
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Plus de 1 audio Manager dans la scene");
        }
        instance = this;
    }

    //Permet d'appeler cette fonction pour venir lancer un son.
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
}
