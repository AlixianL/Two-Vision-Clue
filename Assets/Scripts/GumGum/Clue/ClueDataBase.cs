using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "ClueDatabase", menuName = "Clue System/Clue Database")]
public class ClueDatabase : ScriptableObject
{
    [SerializeField] private List<ClueData> allClues;

    // Retourne tous les indices d'une énigme donnée, triés par numéro d’indice
    public ClueData[] GetCluesForEnigma(string enigmaName)
    {
        return allClues
            .Where(c => c.enigmaName == enigmaName)
            .OrderBy(c => c.clueNumber)
            .ToArray();
    }

    // Facultatif : retourne toutes les énigmes connues
    public string[] GetAllEnigmas()
    {
        return allClues.Select(c => c.enigmaName).Distinct().ToArray();
    }
}