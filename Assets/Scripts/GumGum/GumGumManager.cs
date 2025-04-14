using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère les dialogues et les indices donnés par le personnage GumGum.
/// Utilise une file (queue) pour afficher les phrases une par une.
/// Intègre la logique V2 basée sur des ScriptableObjects et Prefabs.
/// </summary>
public class GumGumManager : MonoBehaviour
{
    public static GumGumManager Instance;

    [Header("UI References"), Space(5)]
    [SerializeField] private TMP_Text _gumgumName;//-------------> Nom de GumGum affiché dans l'UI (non utilisé ici)
    [SerializeField] private TMP_Text _gumgumDialogues;//--------> Zone de texte pour afficher les dialogues
    public GameObject gumGumPanel;//-----------------------------> Panneau UI contenant le dialogue
    public GameObject enigmaContainer;//-------------------------> Conteneur UI avec les boutons d’énigmes

    [Header("GumGum Logic"), Space(5)]
    [SerializeField] private GumGum _gumGum;//-------------------> Référence au script contenant les données de dialogues

    [Header("Clue Prefab System"), Space(5)]
    [SerializeField] private GameObject cluePrefab;//------------> Prefab contenant un script "Clue" lié à un ScriptableObject
    
    private Dictionary<int, Transform> enigmaSpawnPoint;//------> Dictionnaire liant une énigme a un point de spawn 
    [SerializeField] private EnigmaSpawn[] spawnPointsArray;//---> Array regroupant les Dictionnaire enigmaSpawnPoint


    private Queue<string> _sentences;//--------------------------> File d'attente contenant les phrases à afficher

    [Header("State Variables"), Space(5)]
    [HideInInspector] public bool _isInRange;//------------------> Indique si le joueur est proche de GumGum

    void Awake()
    {         
        if (Instance == null)
        {
            Instance = this;
        }

        _sentences = new Queue<string>();
        
        // Remplir le dictionnaire de spawn points
        enigmaSpawnPoint = new Dictionary<int, Transform>();
        
        foreach (var entry in spawnPointsArray)
        {
            if (!enigmaSpawnPoint.ContainsKey(entry.enigmaNumber))
                enigmaSpawnPoint.Add(entry.enigmaNumber, entry.spawnPoint);
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- REDIRECTION VERS UNE ÉNIGME --------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Redirige vers l’indice correspondant en fonction du nom reçu.
    /// Exemple : "Enigma_03" → appel de GiveClueForEnigma(3)
    /// </summary>
    public void RedirectTowardEnigmaClue(string name)
    {
        if (name.StartsWith("Enigma_"))
        {
            if (int.TryParse(name.Replace("Enigma_", ""), out int enigmaNumber))
            {
                GiveClueForEnigma(enigmaNumber);
                return;
            }
        }

        Debug.LogWarning("Enigma Clue Not Found");
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- PRÉSENTATION DE GUMGUM -------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- INTERROGATION SUR ÉNIGME ----------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Demande au joueur sur quelle énigme il est bloqué.
    /// Active les boutons de choix.
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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- AFFICHAGE DES INDICES -------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Affiche les indices sous forme de prefabs pour une énigme donnée.
    /// Les données sont récupérées depuis les ScriptableObjects associés.
    /// </summary>
    /// <param name="enigmaNumber">Numéro de l'énigme (1 à N)</param>
    private void GiveClueForEnigma(int enigmaNumber)
    {
        // Récupération du nom de l'énigme sous forme "Enigma_01", "Enigma_02", etc.
        string enigmaKey = $"Enigma_{enigmaNumber:D2}";

        // Récupération des données d’indices associées via le script GumGum
        ClueData[] clues = _gumGum.GetCluesForEnigma(enigmaKey);

        // Vérification s’il y a des indices disponibles
        if (clues == null || clues.Length == 0)
        {
            Debug.LogWarning($"No clue data found for enigma {enigmaNumber}");
            return;
        }

        // Instanciation de chaque prefab et chargement de ses données via le ScriptableObject
        foreach (var clueData in clues)
        {
            Transform targetSpawn;
            if (!enigmaSpawnPoint.TryGetValue(enigmaNumber, out targetSpawn))
            {
                Debug.LogWarning($"No spawn point found for enigma {enigmaNumber}");
                return;
            }

            float factorX = Random.Range(-0.6f, 0.6f);
            float factorZ = Random.Range(-0.6f, 0.6f);
            
            GameObject instance = Instantiate(cluePrefab, new Vector3(targetSpawn.position.x +factorX, targetSpawn.position.y, targetSpawn.position.z + factorZ), targetSpawn.rotation);
            Clue clueComponent = instance.GetComponent<Clue>();
            
            if (clueComponent != null)
            {
                clueComponent.Initialize(clueData);
            }
        }

        // On désactive les boutons d’énigmes après affichage
        enigmaContainer.SetActive(false);
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- AFFICHAGE DIALOGUE CLASSIQUE ------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Affiche la première phrase du dialogue.
    /// </summary>
    void DisplayFirstSentence()
    {
        if (_sentences.Count > 0)
            _gumgumDialogues.text = _sentences.Dequeue();
    }

    /// <summary>
    /// Affiche la phrase suivante dans la file ou termine le dialogue.
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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- FIN DU DIALOGUE -------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Termine le dialogue : désactive l'UI et restaure le contrôle au joueur.
    /// </summary>
    void EndDialogue()
    {
        gumGumPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PlayerBrain.Instance.cameraRotation.useVerticalCameraRotation = true;
        PlayerBrain.Instance.cameraRotation.useHorizontalCameraRotation = true;
    }
}

[System.Serializable]
public class EnigmaSpawn
{
    public int enigmaNumber;
    public Transform spawnPoint;
}

