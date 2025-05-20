using UnityEngine;

public class ChewingGum : MonoBehaviour, IActivatable
{
    [Header("Références")]
    public GumUIManager gumUIManager;
    
    public void Activate()
    {
        TakeChewingGum();
    }

    public void TakeChewingGum()
    {
        // Incrémente le compteur
        PlayerBrain.Instance.chewingGumCount++;
        
        // Trouve le UIManager si non assigné
        if (gumUIManager == null)
            gumUIManager = FindFirstObjectByType<GumUIManager>();
        
        // Met à jour l'UI
        gumUIManager?.ShowGumCount(PlayerBrain.Instance.chewingGumCount);
        
        // Détruit le chewing-gum
        Destroy(gameObject);
    }
}