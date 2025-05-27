using UnityEngine;
using FMODUnity;

public class TriggerSound: MonoBehaviour
{
    [SerializeField] private EventReference sonBoucle;
    private string cleSon = "boucleZone";

    public void LancerSon()
    {
        AudioManager.instance.PlaySound(sonBoucle, cleSon, transform.position);
    }

    public void ArreterSon()
    {
        AudioManager.instance.StopSound(cleSon);
    }

    public void JouerOneShot()
    {
        AudioManager.instance.PlayOneShot(sonBoucle, transform.position);
    }
}
