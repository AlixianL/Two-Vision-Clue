using UnityEngine;

public class LoadAndSave : MonoBehaviour
{
    [SerializeField, Range(1,2)] private int GameDataSaveTarget;
    public void Load()
    {
        GameManager.Instance.saveData.LoadDataFromJsonFile(GameDataSaveTarget);
    }
    
    public void Save()
    {
        GameManager.Instance.saveData.SaveDataToJsonFile(GameDataSaveTarget);
    }
}
