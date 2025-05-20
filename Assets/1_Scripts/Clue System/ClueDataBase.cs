using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "D_ClueDatabase", menuName = "Clue System/Clue Database")]
public class ClueDataBase : ScriptableObject
{
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- LISTES D'INDICES PAR ÉNIGME ------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // Chaque liste contient les ClueData associés à une énigme spécifique
    [SerializeField] private List<ClueData> cluesEnigma1;
    [SerializeField] private List<ClueData> cluesEnigma2;
    [SerializeField] private List<ClueData> cluesEnigma3;
    [SerializeField] private List<ClueData> cluesEnigma4;

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- RÉCUPÉRATION D'INDICES POUR UNE ÉNIGME -------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Retourne tous les indices d'une énigme donnée, triés par numéro d’indice.
    /// Exemple d’entrée : "Enigma_01"
    /// </summary>
    public ClueData[] GetCluesForEnigma(string enigmaName)
    {
        // Sélectionne dynamiquement la bonne liste d’indices selon le nom de l’énigme
        List<ClueData> clues = enigmaName switch
        {
            "Enigma_01" => cluesEnigma1,
            "Enigma_02" => cluesEnigma2,
            "Enigma_03" => cluesEnigma3,
            "Enigma_04" => cluesEnigma4,
            _ => null
        };

        // Si aucune liste n’est trouvée ou vide, log une alerte et retourne une liste vide
        if (clues == null || clues.Count == 0)
        {
            Debug.LogWarning($"Aucun indice trouvé pour {enigmaName} dans la ClueDatabase.");
            return new ClueData[0];
        }

        // Trie les indices par numéro d’ordre défini dans ClueData
        return clues.OrderBy(c => c.clueNumber).ToArray();
    }
}
