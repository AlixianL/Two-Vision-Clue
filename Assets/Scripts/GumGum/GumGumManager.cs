using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GumGumManager : MonoBehaviour
{
    public static GumGumManager Instance;

    [Header("References"), Space(5)]
    [SerializeField] private TMP_Text _gumgumName;
    [SerializeField] private TMP_Text _gumgumDialogues;
    public GameObject _GumGumPanel;
    
    private Queue<string> _sentences;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of DialogueManager");
            return; 
        }
        _sentences = new Queue<string>();
    }

    public void StartDialogue(GumGum gumgum)
    {
        _sentences.Clear();

        foreach (string sentence in gumgum.gumgumDilogue)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();
        _gumgumDialogues.text = sentence;
    }

    void EndDialogue()
    {
        _GumGumPanel.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerBrain.Instance.cameraRotation.useVerticalCameraRotation = true;
        PlayerBrain.Instance.cameraRotation.useHorizontalCameraRotation = true;
    }
}