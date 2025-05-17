using UnityEngine;

public class LoadAndSave : MonoBehaviour
{
    public void Load()
    {
        GameManager.Instance.saveData.LoadDataFromJsonFile(1);
    }
    
    public void Save()
    {
        GameManager.Instance.saveData.SaveDataToJsonFile(1);
    }
}
