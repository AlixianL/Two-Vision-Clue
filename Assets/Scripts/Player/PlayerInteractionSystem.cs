using UnityEngine;

/// <summary>
/// Ce script combine la détection par Raycast et l'interaction avec des objets Interactable ou IActivatable.
/// </summary>
public class PlayerInteractionSystem : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Transform _rayOrigin;                 // Origine du rayon
    [SerializeField] private LayerMask _layerMaskToCheck;          // Couches à considérer dans le raycast
    [SerializeField] private GameObject hitObject;
    private RaycastHit hit;                                         // Stocke les infos du dernier Raycast
    private Interactable currentInteractable;                      // Référence à l'interactable détecté
    
    [Header("Varaibles"), Space(5)]
    [SerializeField] private float _rayRange;                 // Portée du rayon
    
    
    private void Update()
    {
        HandleRaycast();
    }

    /// <summary>
    /// Effectue le raycast et gère les interactions.
    /// </summary>
    private void HandleRaycast()
    {
        // Lancer un rayon depuis l'origine vers l'avant
        if (Physics.Raycast(_rayOrigin.position, _rayOrigin.forward, out hit, _rayRange, _layerMaskToCheck))
        {
            Debug.DrawRay(_rayOrigin.position, _rayOrigin.forward * hit.distance, Color.magenta);

            hitObject = hit.collider.gameObject;

            // Gestion du surlignage
            
            Interactable newInteractable = hitObject.GetComponent<Interactable>();
            
            if (newInteractable != null && newInteractable.enabled)
            {
                SetNewCurrentInteractable(newInteractable);
            }
            else
            {
                DisableCurrentInteractable();
            }

            // Interaction si le joueur appuie sur le bouton
            if (PlayerBrain.Instance.player.GetButtonDown("Interact"))
            {
                IActivatable activatable = hitObject.GetComponent<IActivatable>();
                if (activatable != null)
                {
                    activatable.Activate();
                }
            }
        }
        else
        {
            // Aucun objet touché : désactiver highlight
            DisableCurrentInteractable();
        }
    }

    /// <summary>
    /// Active le contour et met à jour la référence actuelle.
    /// </summary>
    /// <param name="newInteractable">Nouveau composant interactif détecté</param>
    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        if (currentInteractable != newInteractable)
        {
            DisableCurrentInteractable();
            currentInteractable = newInteractable;
            currentInteractable.EnableOutline();
        }
    }

    /// <summary>
    /// Désactive le contour de l'interactable courant.
    /// </summary>
    private void DisableCurrentInteractable()
    {
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
