using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Ce script représente un objet avec lequel le joueur peut interagir.
public class Interactable : MonoBehaviour
{
    // Composant Outline utilisé pour mettre en évidence l'objet (effet visuel)
    [SerializeField] private Outline outline;
    
    // Message associé à l'interaction (peut être utilisé pour l'affichage UI par exemple)
    public string message;

    // Initialisation au démarrage
    private void Start()
    {
        // Récupère automatiquement le composant Outline attaché à l'objet
        if (outline == null) outline = GetComponent<Outline>();

        // Désactive l'effet de surbrillance (outline) au départ
        DisableOutline();
    }

    public void EnableOutline()
    {
        if (outline != null) outline.enabled = true;
    }

    public void DisableOutline()
    {
        if (outline != null) outline.enabled = false;
    }
}