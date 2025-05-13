using Unity.Cinemachine;
using UnityEngine;

public class GumGum : MonoBehaviour, IActivatable
{   
    [Header("Animation")]
    [SerializeField] private Animator _GumGumAnimator;

    [SerializeField] private string _TalkAnimationTrigger = "Talk";
    public string gumgumName;
    
    [Header("Presentation"), Space(5)]
    [TextArea(3,10)] public string[] gumgumPresentation;
    
    [Header("Interogation"), Space(5)]
    [TextArea(3,10)] public string[] gumgumInterogation;
    
    [Header("Clues"), Space(5)]
    [Tooltip("Base de données contenant tous les indices, classés par énigme.")]
    public ClueDataBase clueDataBase;
    
    /// <summary>
    /// Retourne tous les indices disponibles pour une énigme donnée,
    /// via la base de données d’indices (ScriptableObject).
    /// </summary>
    public ClueData[] GetClues(string enigmaName)
    {
        if (clueDataBase == null)
        {
            Debug.LogWarning("ClueDatabase n’est pas assignée dans le composant GumGum.");
            return new ClueData[0];
        }

        return clueDataBase.GetCluesForEnigma(enigmaName);
    }

    public void Activate()
    {
        GumGumManager.Instance.dialogueTrigger.TriggerDialogue();
        _GumGumAnimator.SetTrigger(_TalkAnimationTrigger);
        GumGumManager.Instance.isInteracting = true;
        
    }
}