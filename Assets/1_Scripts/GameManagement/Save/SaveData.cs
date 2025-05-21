using System;
using System.Collections.Generic;
using TMPro;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            LoadDataFromJsonFile(1);
        }
    }

    /// <summary>
    /// Permet la sauvegarde des données ayant comme origine la classe GameData
    /// </summary>
    public void SaveDataToJsonFile(int number)
    {
        string gameDataString = JsonUtility.ToJson(gameData); // génère la data a partir de gameData
        string filePath = Application.persistentDataPath + "/GameData_Save_0"+ number + ".json"; // Chemin ou enregistrer le fichier json
        System.IO.File.WriteAllText(filePath, gameDataString); // ecriture du fichier json avec les datas saves
        Debug.Log("Save Complete, Location : " + filePath);
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
    [Header("=== INT - VARIABLES =============================="), Space(5)]
    public int chewinggumCount;
    
    [Header("=== BOOL - VARIABLES ============================="), Space(5)]
    public bool enigmaIsComplete_digicode = false;
    public bool enigmaIsComplete_labyrinthe = false;
    public bool enigmaIsComplete_mirror = false;
    public bool enigmaIsComplete_pillar = false;
    public bool enigmaIsComplete_simon = false;
    public bool enigmaIsComplete_final = false;

    [Header("=== ENIGME - DIGICODE ============================"), Space(5)]
    public bool digicodeValidateLightIsActive;
    public string codeText;
    public bool doorsAreOpen;
    
    [Header("=== ENIGME - LABYRINTHE =========================="), Space(5)]
    public bool labyrintheValidateLightIsActive;
    public Vector3 labyrintheRotation;
    public Vector3 ballPosition;
    
    [Header("=== ENIGME - MIRROIRS ============================"), Space(5)]
    public bool mirorsValidateLightIsActive;
    [Space(5)]
    public Vector3 rotationMirrorY_01;
    public Vector3 rotationMirrorX_01;
    [Space(5)]
    public Vector3 rotationMirrorY_02;
    public Vector3 rotationMirrorX_02;
    [Space(5)]
    public Vector3 rotationMirrorY_03;
    public Vector3 rotationMirrorX_03;
    [Space(5)]
    public Vector3 rotationMirrorY_04;
    public Vector3 rotationMirrorX_04;
    
    [Header("=== ENIGME - PILLIER ============================"), Space(5)]
    public bool pillarValidateLightIsActive;
    [Space(5)]
    public Vector3 rotationCubeYTop;
    [Space(5)]
    public Vector3 rotationCubeYMid;
    [Space(5)]
    public Vector3 rotationCubeYBot;

    [Header("=== ENIGME - Simon =============================="), Space(5)]
    public bool simonEnigmaIsEnd;

    [Header("=== GUMGUM ======================================"), Space(5)]
    public Transform cluePosition1;
    public List<ClueData> clueDatasAlreadyGivesForEnigma1 = new List<ClueData>();
    [Space(5)]
    public Transform cluePosition2;
    public List<ClueData> clueDatasAlreadyGivesForEnigma2 = new List<ClueData>();
    [Space(5)]
    public Transform cluePosition3;
    public List<ClueData> clueDatasAlreadyGivesForEnigma3 = new List<ClueData>();
    [Space(5)]
    public Transform cluePosition4;
    public List<ClueData> clueDatasAlreadyGivesForEnigma4 = new List<ClueData>();
}