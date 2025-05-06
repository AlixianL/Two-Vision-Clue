using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;
using Random = UnityEngine.Random;

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
    public DialogueTrigger dialogueTrigger;//-> Référence du script pour les conditions de dialogue

    [Header("Clue Prefab System"), Space(5)]
    [HideInInspector] public GameObject clueInstance;//----------> Variable de stockage de l'instance
    [SerializeField] private GameObject cluePrefab;//------------> Prefab contenant un script "Clue" lié à un ScriptableObject
    [SerializeField] private CluePosition _cluePosition;//-------> Reference au script pour le positionnement des indices
    private Transform targetSpawn;//-----------------------------> transform du point de spawn
    private int _clueIndexEnigma1;//-------------------------> Index pour instancier les indices au fur et a mesure
    private int _clueIndexEnigma2;//-------------------------> Index pour instancier les indices au fur et a mesure
    private int _clueIndexEnigma3;//-------------------------> Index pour instancier les indices au fur et a mesure
    private int _clueIndexEnigma4;//-------------------------> Index pour instancier les indices au fur et a mesure
    private int _clueIndexEnigma5;//-------------------------> Index pour instancier les indices au fur et a mesure
    private int _clueIndexEnigma6;//-------------------------> Index pour instancier les indices au fur et a mesure
    
    private Dictionary<int, Transform> enigmaSpawnPoint;//------> Dictionnaire liant une énigme a un point de spawn 
    [SerializeField] private EnigmaSpawn[] spawnPointsArray;//---> Array regroupant les Dictionnaire enigmaSpawnPoint

    private Queue<string> _sentences;//--------------------------> File d'attente contenant les phrases à afficher

    [Header("State Variables"), Space(5)]
    [HideInInspector] public bool _isInRange;//------------------> Indique si le joueur est proche de GumGum
    
    [Header("Cinemachine Variables"), Space(5)]
    public bool isInteracting;
    public CinemachineCamera gumgumCinemachineCamera;

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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- REDIRECTION VERS UNE ÉNIGME ----------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Redirige vers l’indice correspondant en fonction du nom reçu.
    /// Exemple : "Enigma_03" → appel de GiveClueForEnigma(3)
    /// </summary>
    public void RedirectTowardEnigmaClue(string name)
    {
        if (PlayerBrain.Instance.chewingGumCount > 0)
        {
            if (name.StartsWith("Enigma_"))
            {
                if (int.TryParse(name.Replace("Enigma_", ""), out int enigmaNumber))
                {
                    GiveClueForEnigma(enigmaNumber);
                    PlayerBrain.Instance.chewingGumCount--;
                }
            }
        }
        else
        {
            EndDialogue();
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- PRÉSENTATION DE GUMGUM ---------------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- INTERROGATION SUR ÉNIGME -------------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- AFFICHAGE DES INDICES ----------------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Affiche les indices sous forme de prefabs pour une énigme donnée.
    /// Les données sont récupérées depuis les ScriptableObjects associés.
    /// </summary>
    /// <param name="enigmaNumber">Numéro de l'énigme (1 à N)</param>
    private void GiveClueForEnigma(int enigmaNumber)
    {
        // Format attendu : "Enigma_01", "Enigma_02", etc.
        string enigmaKey = $"Enigma_{enigmaNumber:D2}";
        
        // Récupère l’index courant pour cette énigme
        int clueIndex = GetCurrentClueIndex(enigmaKey);

        // Récupère les données d’indices depuis le ScriptableObject
        ClueData[] clues = _gumGum.GetClues(enigmaKey);

        if (clues == null || clues.Length == 0)
        {
            Debug.LogWarning($"Aucun indice trouvé pour {enigmaKey}");
            return;
        }
        
        // Si on a déjà montré tous les indices, ne rien faire
        if (clueIndex >= clues.Length)
        {
            Debug.Log($"Tous les indices de {enigmaKey} ont déjà été montrés.");
            return;
        }

        // Récupère le bon spawn point
        if (!enigmaSpawnPoint.TryGetValue(enigmaNumber, out targetSpawn))
        {
            Debug.LogWarning($"Pas de point de spawn défini pour l’énigme {enigmaNumber}");
            return;
        }

        // Instancie l’indice correspondant à l’index courant
        IntanciateClue();

        Clue clueComponent = clueInstance.GetComponent<Clue>();
        if (clueComponent != null)
        {
            clueComponent.Initialize(clues[clueIndex]);
        }

        // Incrémente l’index pour cette énigme
        IncrementClueIndex(enigmaKey);

        // Cache les boutons et termine le dialogue
        enigmaContainer.SetActive(false);
        EndDialogue();
    }

    /// <summary>
    /// Instancie un indice à une position aléatoire.
    /// </summary>
    private void IntanciateClue()
    {
        clueInstance = Instantiate(cluePrefab,targetSpawn.position + new Vector3(Random.Range(-0.15f, 0.15f), 0, Random.Range(-0.15f, 0.15f)), targetSpawn.rotation);
        clueInstance.transform.SetParent(targetSpawn);
        _cluePosition = targetSpawn.GetComponent<CluePosition>();
        
        _cluePosition.clues.Add(clueInstance);
        _cluePosition.UpdatePosition();
        
        ChangePositionCinemachine.Instance.SwitchIntoClueCinemachineCamera(gumgumCinemachineCamera, _cluePosition.clueCinemachineCamera);
        ChangePositionCinemachine.Instance._gumgumCinemachineCamera.Priority = 0;
        
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        
        if (Cursor.visible == false) Cursor.visible = true;
        else Cursor.visible = false;
        
        GameManager.Instance.ToggleTotalFreezePlayer(); 
        PlayerBrain.Instance.playerGameObject.transform.position = new Vector3(targetSpawn.position.x, PlayerBrain.Instance.playerGameObject.transform.position.y, targetSpawn.position.z - 1.5f);
        PlayerBrain.Instance.playerGameObject.transform.rotation = Quaternion.Euler(0, targetSpawn.rotation.eulerAngles.y, 0);
        PlayerBrain.Instance.cinemachineTargetGameObject.transform.LookAt(targetSpawn.position);
        CluePosition tempVar = targetSpawn.GetComponent<CluePosition>();
        tempVar._playerIsInteracting = true;
    }

    /// <summary>
    /// Incrémente l’index de l’indice à afficher pour une énigme donnée.
    /// </summary>
    private void IncrementClueIndex(string clueName)
    {
        switch (clueName)
        {
            case "Enigma_01": _clueIndexEnigma1++; break; // Si clueName == "Enigma_01" alors l'index de l'énigme 1 augmente de 1
            case "Enigma_02": _clueIndexEnigma2++; break; // Si clueName == "Enigma_02" alors l'index de l'énigme 2 augmente de 1
            case "Enigma_03": _clueIndexEnigma3++; break; // Si clueName == "Enigma_03" alors l'index de l'énigme 3 augmente de 1
            case "Enigma_04": _clueIndexEnigma4++; break; // Si clueName == "Enigma_04" alors l'index de l'énigme 4 augmente de 1
            case "Enigma_05": _clueIndexEnigma5++; break; // Si clueName == "Enigma_05" alors l'index de l'énigme 5 augmente de 1
            case "Enigma_06": _clueIndexEnigma6++; break; // Si clueName == "Enigma_06" alors l'index de l'énigme 6 augmente de 1
        }
    }

    /// <summary>
    /// Récupère l’index actuel pour savoir quel indice afficher.
    /// </summary>
    private int GetCurrentClueIndex(string clueName)
    {
        return clueName switch
        {
            "Enigma_01" => _clueIndexEnigma1, // Si clueName == "Enigma_01" alors on prend l'index pour l'énigme 1
            "Enigma_02" => _clueIndexEnigma2, // Si clueName == "Enigma_02" alors on prend l'index pour l'énigme 2
            "Enigma_03" => _clueIndexEnigma3, // Si clueName == "Enigma_03" alors on prend l'index pour l'énigme 3
            "Enigma_04" => _clueIndexEnigma4, // Si clueName == "Enigma_04" alors on prend l'index pour l'énigme 4
            "Enigma_05" => _clueIndexEnigma5, // Si clueName == "Enigma_05" alors on prend l'index pour l'énigme 5
            "Enigma_06" => _clueIndexEnigma6, // Si clueName == "Enigma_06" alors on prend l'index pour l'énigme 6
            _ => 0
        };
    }


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- AFFICHAGE DIALOGUE CLASSIQUE ---------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- FIN DU DIALOGUE ----------------------------------------------------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Termine le dialogue : désactive l'UI et restaure le contrôle au joueur.
    /// </summary>
    void EndDialogue()
    {
        gumGumPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameManager.Instance.ToggleTotalFreezePlayer();
        
        isInteracting = false;
        ChangePositionCinemachine.Instance.SwitchCam(gumgumCinemachineCamera, isInteracting);
    }
}

[System.Serializable]
public class EnigmaSpawn
{
    public int enigmaNumber;
    public Transform spawnPoint;
}