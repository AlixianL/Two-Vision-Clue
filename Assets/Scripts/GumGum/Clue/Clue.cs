using System;
using TMPro;
using UnityEngine;

public class Clue : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private ClueData clueData;
    [SerializeField] private TMP_Text _clueTextRenderer;
    
    [Header("Clue Variables")]
    [SerializeField] private string _enigmaName;
    [SerializeField] private int _clueNumber;
    [SerializeField, TextArea(5,10)] private string _clueText;
    
    public void Initialize(ClueData data)
    {
        clueData = data;
        _enigmaName = data.enigmaName;
        _clueNumber = data.clueNumber;
        _clueText = data.clueText;
        _clueTextRenderer.text = data.clueText;
    }
}