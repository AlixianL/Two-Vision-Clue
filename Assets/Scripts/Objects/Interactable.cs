using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Ce script représente un objet avec lequel le joueur peut interagir.
public class Interactable : MonoBehaviour
{
    // Composant Outline utilisé pour mettre en évidence l'objet (effet visuel)
    public Outline outline;

    // Message associé à l'interaction (peut être utilisé pour l'affichage UI par exemple)
    public string message;

    // Initialisation au démarrage
    private void Start()
    {
        // Récupère automatiquement le composant Outline attaché à l'objet
        outline = GetComponent<Outline>();

        // Désactive l'effet de surbrillance (outline) au départ
        DisableOutline();

        // Affiche dans la console si le composant Outline a bien été trouvé
        Debug.Log(outline);
    }

    // Désactive l'effet de surbrillance de l'objet
    public void DisableOutline()
    {
        outline.enabled = false;
    }

    // Active l'effet de surbrillance de l'objet
    public void EnableOutline()
    {
        outline.enabled = true;
    }
}