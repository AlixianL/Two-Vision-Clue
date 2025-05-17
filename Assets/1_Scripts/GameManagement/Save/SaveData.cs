using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;
    
    [Header("DATA TO SAVE")]
    public GameData gameData = new GameData();
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        Debug.Log(Instance.ToString());
    }
    
    /// <summary>
    /// Permet la sauvegarde des données ayant comme origine la classe GameData
    /// </summary>
    public void SaveDataToJsonFile(int number)
    {
        string gameDataString = JsonUtility.ToJson(gameData); // génère la data a partir de gameData
        string filePath = Application.persistentDataPath + "/GameData_Save_0"+ number + ".json"; // Chemin ou enregistrer le fichier json
        System.IO.File.WriteAllText(filePath, gameDataString); // ecriture du fichier json avec les datas saves
        Debug.Log("Save Complete");
    }
    
    /// <summary>
    /// Permet le chargement des données ayant comme origine la classe GameData
    /// </summary>
    public void LoadDataFromJsonFile(int number)
    {
        string filePath = Application.persistentDataPath + "/GameData_Save_0"+ number + ".json"; // Chemin ou enregistrer le fichier json
        string gameDataString = System.IO.File.ReadAllText(filePath);
        
        gameData = JsonUtility.FromJson<GameData>(gameDataString);
        Debug.Log("Load Complete");
    }
}

[System.Serializable] // Permet d'utilise une classe comme variable
public class GameData
{
    [Header("INT - VARIABLES"), Space(5)]
    public int chewinggumCount;
    
    [Header("BOOL - VARIABLES"), Space(5)]
    public bool digicodeEnigmaIsComplete = false;
    public bool labyrintheEnigmaIsComplete = false;
    public bool mirrorEnigmaIsComplete = false;
    public bool pillarEnigmaIsComplete = false;
    public bool simonEnigmaIsComplete = false;
    public bool finalEnigmaIsComplete = false;
}