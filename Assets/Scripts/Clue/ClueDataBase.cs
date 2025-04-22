using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "D_ClueDatabase", menuName = "Clue System/Clue Database")]
public class ClueDatabase : ScriptableObject
{
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- LISTES D'INDICES PAR ÉNIGME ------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // Chaque liste contient les ClueData associés à une énigme spécifique
    [SerializeField] private List<ClueData> cluesEnigma1;
    [SerializeField] private List<ClueData> cluesEnigma2;
    [SerializeField] private List<ClueData> cluesEnigma3;
    [SerializeField] private List<ClueData> cluesEnigma4;
    [SerializeField] private List<ClueData> cluesEnigma5;
    [SerializeField] private List<ClueData> cluesEnigma6;

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
            "Enigma_05" => cluesEnigma5,
            "Enigma_06" => cluesEnigma6,
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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- RETOUR DE TOUTES LES ÉNIGMES DISPONIBLES -----------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Retourne les noms des énigmes disponibles dans la base de données.
    /// Exemple : ["Enigma_01", "Enigma_03", "Enigma_05"]
    /// </summary>
    public string[] GetAllEnigmas()
    {
        List<string> availableEnigmas = new();

        // Vérifie chaque liste : si elle contient des indices, on ajoute son nom à la liste finale
        if (cluesEnigma1?.Count > 0) availableEnigmas.Add("Enigma_01");
        if (cluesEnigma2?.Count > 0) availableEnigmas.Add("Enigma_02");
        if (cluesEnigma3?.Count > 0) availableEnigmas.Add("Enigma_03");
        if (cluesEnigma4?.Count > 0) availableEnigmas.Add("Enigma_04");
        if (cluesEnigma5?.Count > 0) availableEnigmas.Add("Enigma_05");
        if (cluesEnigma6?.Count > 0) availableEnigmas.Add("Enigma_06");

        return availableEnigmas.ToArray();
    }
}
