using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimonsManager : MonoBehaviour
{
    [SerializeField] private SimonElement[] elements;
    [SerializeField] private float pickDelay = 0.5f;
    private List<int> colorOrder = new List<int>(); // Séquence des couleurs
    private int pickNumber = 0; // Indice actuel dans la séquence que le joueur doit suivre

    private void Start()
    {
        StartCoroutine(PlayGame());
    }

    // Cette coroutine gère l'affichage de la séquence
    IEnumerator PlayGame()
    {
        pickNumber = 0;

        // Réinitialiser toutes les couleurs des éléments avant de commencer une nouvelle séquence
        foreach (var element in elements)
        {
            element.ResetColor();
        }

        // Afficher la séquence (seuls les éléments de la séquence s'illuminent un par un)
        yield return new WaitForSeconds(pickDelay); // Attente avant de commencer

        // Affichage de chaque élément de la séquence un par un
        foreach (int index in colorOrder)
        {
            elements[index].Preview(); // Seule la couleur de l'élément actif devient visible
            yield return new WaitForSeconds(pickDelay); // Attendre avant d'activer le suivant
        }

        // Ajouter un nouvel élément à la séquence
        AddRandomElementToSequence();
    }

    // Ajouter un élément aléatoire à la séquence
    private void AddRandomElementToSequence()
    {
        int rnd = Random.Range(0, elements.Length);
        colorOrder.Add(rnd);
        Debug.Log("Nouvel index ajouté à la séquence : " + rnd);
    }

    // Gère l'interaction du joueur avec un élément (quand il active un cube)
    public void PlayerActivated(SimonElement element)
    {
        int expectedIndex = colorOrder[pickNumber];

        if (element.Index == expectedIndex)
        {
            Debug.Log("Correct!");
            pickNumber++;

            // Si le joueur a suivi toute la séquence correctement
            if (pickNumber == colorOrder.Count)
            {
                StartCoroutine(PlayGame()); // Lancer une nouvelle séquence
            }
        }
        else
        {
            Debug.Log("Erreur! Reset du jeu.");
            colorOrder.Clear(); // Réinitialiser la séquence
            StartCoroutine(PlayGame()); // Recommencer avec une nouvelle séquence
        }
    }
}
