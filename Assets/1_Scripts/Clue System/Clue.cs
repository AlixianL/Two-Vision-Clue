using System;
using TMPro;
using UnityEngine; 

public class Clue : MonoBehaviour, ISaveAndPullData
{
    [Header("References"), Space(5)]
    public ClueData _clueData;
    [SerializeField] private TMP_Text _clueTextRenderer;
    
    [Header("Clue Variables")]
    [SerializeField] private string _enigmaName;
    [SerializeField] private int _clueNumber;
    [SerializeField, TextArea(5,10)] private string _clueText;
    
    public void Initialize(ClueData data)
    {
        _clueData = data;
        _enigmaName = data.enigmaName;
        _clueNumber = data.clueNumber;
        _clueTextRenderer.text = data.clueText;
        
        PushDataToSave();
    }
    
    public void LoadInitialize(ClueData data)
    {
        _clueData = data;
        _enigmaName = data.enigmaName;
        _clueNumber = data.clueNumber;
        _clueTextRenderer.text = data.clueText;
    }

    public void PullDataFromSave()
    {
        
    }

    public void PushDataToSave()
    {
        switch (_enigmaName)
        {
            case "Enigma_01":
                SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma1.Add(_clueData);
                break;
            
            case "Enigma_02":
                SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma2.Add(_clueData);
                break;
            
            case "Enigma_03":
                SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma3.Add(_clueData);
                break;
            
            case "Enigma_04":
                SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma4.Add(_clueData);
                break;
        }
    }
}