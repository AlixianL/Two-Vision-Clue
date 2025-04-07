using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère les dialogues et les indices donnés par le personnage GumGum.
/// Utilise une file (queue) pour afficher les phrases une par une.
/// </summary>
public class GumGumManager : MonoBehaviour
{
    // Singleton pour accéder facilement à ce manager dans la scène
    public static GumGumManager Instance;

    [Header("References"), Space(5)]
    [SerializeField] private TMP_Text _gumgumName; // Nom de GumGum affiché dans l'UI (non utilisé ici)
    [SerializeField] private TMP_Text _gumgumDialogues;  // Zone de texte pour afficher les dialogues
    [SerializeField] private GumGum _gumGum; // Référence au scriptable object contenant les dialogues
    public GameObject gumGumPanel; // Panneau UI contenant le dialogue
    public GameObject enigmaContainer; // Conteneur UI avec les boutons d’énigmes

    private Queue<string> _sentences; // File d'attente contenant les phrases à afficher

    [Header("Variables"), Space(5)]
    [HideInInspector] public bool _isInRange; // Indique si le joueur est proche de GumGum

    void Awake()
    {
        // Initialise le singleton
        if (Instance == null)
        {
            Instance = this;
        }

        // Initialise la file de dialogue
        _sentences = new Queue<string>();
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- SWITCH DE SELECTION (appelé via bouton UI) ---------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Redirige vers l’indice correspondant en fonction du nom reçu.
    /// Exemple : "Enigma_03" → appel de GiveClueForEnigma(3)
    /// </summary>
    public void RedirectTowardEnigmaClue(string name)
    {
        // Vérifie que le nom commence bien par "Enigma_"
        if (name.StartsWith("Enigma_"))
        {
            // Essaie d’extraire le numéro à partir du nom
            if (int.TryParse(name.Replace("Enigma_", ""), out int enigmaNumber))
            {
                GiveClueForEnigma(enigmaNumber);
                return;
            }
        }

        Debug.Log("Enigma Clue Not Found");
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- PRÉSENTATION DE GUMGUM -----------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Présente GumGum au joueur avec son dialogue d’introduction.
    /// </summary>
    public void GumGumPresentHimself()
    {
        foreach (string sentence in _gumGum.gumgumPresentation)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- INTERROGATION SUR L’ÉNIGME BLOQUANTE ---------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Demande au joueur sur quelle énigme il est bloqué.
    /// Affiche les boutons de choix.
    /// </summary>
    public void GumGumAsksThePlayerWhichHesBlockingOn()
    {
        foreach (string sentence in _gumGum.gumgumInterogation)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayFirstSentence();
        enigmaContainer.SetActive(true);
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- AFFICHAGE D'INDICE POUR UNE ÉNIGME -----------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Affiche les indices correspondant à l'énigme demandée.
    /// Les indices sont récupérés dynamiquement via l'index passé en paramètre.
    /// </summary>
    /// <param name="enigmaNumber">Numéro de l'énigme (1 à N)</param>
    private void GiveClueForEnigma(int enigmaNumber)
    {
        var allClues = _gumGum.GetAllEnigmaClues();

        // Vérifie que le numéro est dans les bornes
        if (enigmaNumber < 1 || enigmaNumber > allClues.Length)
        {
            Debug.LogWarning($"No clue data found for enigma {enigmaNumber}");
            return;
        }

        // Récupère la liste de phrases correspondant à cette énigme
        var clueList = allClues[enigmaNumber - 1]; // -1 car les indices commencent à 0

        // Alimente la file de dialogue avec chaque phrase de l'indice
        foreach (string sentence in clueList)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayFirstSentence();
        enigmaContainer.SetActive(false);
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- AFFICHAGE DU DIALOGUE ------------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Affiche la première phrase du dialogue courant.
    /// </summary>
    void DisplayFirstSentence()
    {
        _gumgumDialogues.text = _sentences.Dequeue();
    }

    /// <summary>
    /// Affiche la phrase suivante dans la file de dialogue.
    /// Si plus de phrase, le dialogue se termine.
    /// </summary>
    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            _sentences.Clear();
            EndDialogue();
            return;
        }

        _gumgumDialogues.text = _sentences.Dequeue();
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- FIN DU DIALOGUE -------------------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Termine le dialogue : désactive le panneau et redonne le contrôle au joueur.
    /// </summary>
    void EndDialogue()
    {
        gumGumPanel.SetActive(false);

        // Restaure le contrôle caméra et cache le curseur
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerBrain.Instance.cameraRotation.useVerticalCameraRotation = true;
        PlayerBrain.Instance.cameraRotation.useHorizontalCameraRotation = true;
    }
}