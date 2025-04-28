using System.Collections;
using UnityEngine;

/// <summary>
/// Gère les rayons de lumière, leur activation et leur rebond sur des miroirs.
/// Ici on a la gestion du bouton on/off de cette énigme la gestion des rebon sur les surface
/// et la condition de victoire de l'énigme.
/// </summary>
public class LaserBeam : MonoBehaviour
{
    public LineRenderer lineRenderer; //------------> Visuel du rayon
    public GameObject startPointObject;//-----------> Point de départ du rayon
    public GameObject player;//---------------------> Joueur

    private bool _isPlayerInRange = false;//--------> Condition d'interaction avec le bouton
    public KeyCode activationKey = KeyCode.E; // remplacer par rewired input
    private bool _lazerIsOn = false;//--------------> Condition Si le lazer est actif
    private bool _puzzleEnd = false;//--------------> Condition de fin de l'énigme


    public float maxDistance = 100f;//--------------> Distance max entre 2 point du line renderer

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Détection du joueur pour le bouton -----------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            _isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            _isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (_puzzleEnd) return;

        if (_isPlayerInRange && Input.GetKeyDown(activationKey) && !_lazerIsOn)
        {
            _lazerIsOn = true;
        }
        else if (_isPlayerInRange && Input.GetKeyDown(activationKey) && _lazerIsOn)
        {
            _lazerIsOn = false;
            lineRenderer.positionCount = 0;
        }

        if (_lazerIsOn)
        {
            DrawLaser();
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction du comportement du rayon ------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Ici on fait apparaitre le rayon et avec le linerenderer
    /// Ensuite on verifie si le rayon rentre en collision.
    /// Soit il touche un "mirror" et il rebondi, soit un "puzzleEnd" et il met fin a l'énigme
    /// </summary>
    void DrawLaser()
    {
        Vector3 direction = startPointObject.transform.forward;
        Vector3 currentPosition = startPointObject.transform.position;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, currentPosition);

        while (true)
        {
            if (_puzzleEnd) break;
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, direction, out hit, maxDistance))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                if (hit.collider.CompareTag("Mirror"))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    currentPosition = hit.point;
                }
                else if (hit.collider.CompareTag("PuzzleEnd"))
                {
                    EndLaserEnigme();
                }
                else
                {
                    break;
                }
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition + direction * maxDistance);
                break;
            }
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction de fin d'énigme ---------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void EndLaserEnigme()
    {
        _puzzleEnd = true;
    }
}
