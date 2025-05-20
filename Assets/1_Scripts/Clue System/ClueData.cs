using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "D_Enigma_XX_Clue_XX", menuName = "Clue System/New Clue")]
public class ClueData : ScriptableObject
{
    public string enigmaName = "Enigma_0";
    public int clueNumber = 1;
    [TextArea(5,10)] public string clueText;
}