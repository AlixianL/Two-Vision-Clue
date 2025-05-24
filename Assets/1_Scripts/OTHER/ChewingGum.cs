using UnityEngine;

public class ChewingGum : MonoBehaviour, IActivatable, ISaveAndPullData
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
        
        PushDataToSave();
        
        // Détruit le chewing-gum
        Destroy(gameObject);
    }
    
    public void PushDataToSave()
    {
        SaveData.Instance.gameData.chewinggumCount++;
        SaveData.Instance.gameData.chewinggumsAlreadyTooked.Add(gameObject.name);
    }

    public void PullDataFromSave() {} // Ne sert pas dans ce script
}