using UnityEngine;
using System.Linq;
using System.Reflection;

[System.Serializable]
public class GumGum : MonoBehaviour
{
    public string gumgumName;
    
    [Header("Presentation"), Space(5)]
    [TextArea(3,10)] public string[] gumgumPresentation;
    
    [Header("Interogation"), Space(5)]
    [TextArea(3,10)] public string[] gumgumInterogation;
    
    [Header("Clues"), Space(5)]
    [TextArea(3,10)] public string[] gumgumEnigma_01;
    [TextArea(3,10)] public string[] gumgumEnigma_02;
    [TextArea(3,10)] public string[] gumgumEnigma_03;
    [TextArea(3,10)] public string[] gumgumEnigma_04;
    [TextArea(3,10)] public string[] gumgumEnigma_05;
    [TextArea(3,10)] public string[] gumgumEnigma_06;
    
    /// <summary>
    /// Récupère dynamiquement tous les tableaux d'indices d'énigmes (gumgumEnigma_XX)
    /// et les retourne sous forme d'un tableau de tableaux de chaînes de caractères (string[][]).
    /// Cette méthode utilise la réflexion pour éviter de devoir maintenir manuellement une liste.
    /// </summary>
    public string[][] GetAllEnigmaClues()
    {
        // Récupère tous les champs publics d'instance de cette classe (GumGum)
        var fields = this.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.Instance)

            // Ne garde que les champs de type string[] dont le nom commence par "gumgumEnigma_"
            .Where(f => f.FieldType == typeof(string[]) && f.Name.StartsWith("gumgumEnigma_"))

            // Trie les champs en fonction du numéro extrait du nom (ex: gumgumEnigma_03 → 3)
            .OrderBy(f => 
            {
                // Tente d'extraire le numéro à partir du nom du champ
                if (int.TryParse(f.Name.Replace("gumgumEnigma_", ""), out int index))
                    return index;

                // Si le nom ne contient pas de numéro valide, place-le à la fin
                return int.MaxValue;
            })

            // Convertit le résultat final en tableau
            .ToArray();

        // Récupère la valeur de chaque champ (string[]) pour construire le tableau global
        var enigmaClues = fields
            .Select(f => (string[])f.GetValue(this)) // Cast nécessaire car GetValue() retourne object
            .ToArray();

        // Retourne le tableau complet contenant tous les indices d'énigmes
        return enigmaClues;
    }
}