using UnityEngine;

public class LoadAndSave : MonoBehaviour
{
    public void Load()
    {
        GameManager.Instance.saveData.LoadDataFromJsonFile();
    }
    
    public void Save()
    {
        GameManager.Instance.saveData.SaveDataToJsonFile();
    }
}
