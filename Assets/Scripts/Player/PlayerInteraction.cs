using UnityEngine;

// Ce script permet au joueur d'interagir avec des objets "interactables" dans le jeu.
public class PlayerInteraction : MonoBehaviour
{
    // Portée d'interaction du joueur (distance maximale à laquelle il peut interagir)
    public float playerReach = 3f;

    // Référence à l'objet interactif actuellement ciblé
    Interactable currentInteractable;

    // Appelé à chaque frame
    private void Update()
    {
        // Vérifie si un objet interactif est devant le joueur
        CheckInteraction();

        // Si la touche "F" est pressée et qu'un objet interactif est détecté, interagir avec lui
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    // Vérifie s'il y a un objet interactif devant le joueur
    void CheckInteraction()
    {
        RaycastHit hit;

        // Crée un rayon partant de la position de la caméra du joueur dans la direction où il regarde
        Ray ray = new Ray(PlayerBrain.Instance.cinemachineTargetGameObject.transform.position,
                          PlayerBrain.Instance.cinemachineTargetGameObject.transform.forward);

        // Envoie le rayon et vérifie s'il touche quelque chose dans la portée définie
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            // Si l'objet touché a le tag "Interactable"
            if (hit.collider.tag == "Interactable")
            {
                // Récupère le composant Interactable de l'objet
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                // Si l'objet est actif (enabled), le définir comme interactif courant
                if (newInteractable.enabled)
                {
                    SetNewcurrentInteractable(newInteractable);
                }
                else
                {
                    // Sinon, désactiver l'objet interactif courant
                    DisableCurrentInteractable();
                }
            }
            else
            {
                // Si l'objet touché n'est pas interactif, désactiver l'objet courant
                DisableCurrentInteractable();
            }
        }
        else
        {
            // Si aucun objet n'est touché, désactiver l'objet interactif courant
            DisableCurrentInteractable();
        }
    }

    // Définit un nouvel objet interactif et active son effet visuel (outline)
    void SetNewcurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
    }

    // Désactive l'effet visuel de l'objet interactif courant et le réinitialise
    void DisableCurrentInteractable()
    {
        currentInteractable?.DisableOutline(); // Vérifie si currentInteractable n'est pas nul avant d'appeler la méthode
        currentInteractable = null;
    }
}
