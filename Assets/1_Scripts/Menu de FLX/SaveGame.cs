using UnityEngine;
using System.Collections.Generic;

public class SaveGame : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    //[SerializeField] private List<GameObject> MainMenu = new List<GameObject>();
    [SerializeField] private GameObject loadGamePanel;
    
    public void Activate()
    {
        //foreach (GameObject Panel in MainMenu)
        //{
        //    Panel.SetActive(false);
        //}
        
        GameManager.Instance.loadAndSave.Save();
    }
}
