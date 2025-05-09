using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimonsManager : MonoBehaviour
{
    [SerializeField] private SimonElement[] elements;
    [SerializeField] private float pickDelay = 0.5f;
    private List<int> colorOrder = new List<int>(); // S�quence des couleurs
    private int pickNumber = 0; // Indice actuel dans la s�quence que le joueur doit suivre

    private void Start()
    {
        StartCoroutine(PlayGame());
    }

    // Cette coroutine g�re l'affichage de la s�quence
    IEnumerator PlayGame()
    {
        pickNumber = 0;

        // R�initialiser toutes les couleurs des �l�ments avant de commencer une nouvelle s�quence
        foreach (var element in elements)
        {
            element.ResetColor();
        }

        // Afficher la s�quence (seuls les �l�ments de la s�quence s'illuminent un par un)
        yield return new WaitForSeconds(pickDelay); // Attente avant de commencer

        // Affichage de chaque �l�ment de la s�quence un par un
        foreach (int index in colorOrder)
        {
            elements[index].Preview(); // Seule la couleur de l'�l�ment actif devient visible
            yield return new WaitForSeconds(pickDelay); // Attendre avant d'activer le suivant
        }

        // Ajouter un nouvel �l�ment � la s�quence
        AddRandomElementToSequence();
    }

    // Ajouter un �l�ment al�atoire � la s�quence
    private void AddRandomElementToSequence()
    {
        int rnd = Random.Range(0, elements.Length);
        colorOrder.Add(rnd);
        Debug.Log("Nouvel index ajout� � la s�quence : " + rnd);
    }

    // G�re l'interaction du joueur avec un �l�ment (quand il active un cube)
    public void PlayerActivated(SimonElement element)
    {
        int expectedIndex = colorOrder[pickNumber];

        if (element.Index == expectedIndex)
        {
            Debug.Log("Correct!");
            pickNumber++;

            // Si le joueur a suivi toute la s�quence correctement
            if (pickNumber == colorOrder.Count)
            {
                StartCoroutine(PlayGame()); // Lancer une nouvelle s�quence
            }
        }
        else
        {
            Debug.Log("Erreur! Reset du jeu.");
            colorOrder.Clear(); // R�initialiser la s�quence
            StartCoroutine(PlayGame()); // Recommencer avec une nouvelle s�quence
        }
    }
}
