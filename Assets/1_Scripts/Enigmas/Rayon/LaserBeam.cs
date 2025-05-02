using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// G�re les rayons de lumi�re, leur activation et leur rebond sur des miroirs.
/// Ici on a la gestion du bouton on/off de cette �nigme la gestion des rebon sur les surface
/// et la condition de victoire de l'�nigme.
/// </summary>
public class LaserBeam : MonoBehaviour, IActivatable
{
    [SerializeField] private LineRenderer _lineRenderer; //------------> Visuel du rayon
    [SerializeField] private GameObject _startPointObject;//-----------> Point de d�part du rayon
    [SerializeField] private GameObject _player;//---------------------> Joueur

    private bool _lazerIsOn = false;//---------------------------------> Condition Si le lazer est actif
    private bool _puzzleEnd = false;//---------------------------------> Condition de fin de l'�nigme


    public float maxDistance = 100f;//---------------------------------> Distance max entre 2 point du line renderer

    [SerializeField] private List<GameObject> _mirror = new List<GameObject>();

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- D�tection du joueur pour le bouton -----------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Activate()
    {
        if (!_lazerIsOn)
        {
            _lazerIsOn = true;
        }
        else if (_lazerIsOn)
        {
            _lazerIsOn = false;
            _lineRenderer.positionCount = 0;
        }
    }
    

    void Update()
    {
        if (_puzzleEnd) return;


        if (_lazerIsOn)
        {
            DrawLaser();
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction du comportement du rayon ------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    /// <summary>
    /// Ici on fait apparaitre le rayon et avec le lineRenderer
    /// Ensuite on verifie si le rayon rentre en collision.
    /// Soit il touche un "mirror" et il rebondi, soit un "puzzleEnd" et il met fin a l'�nigme
    /// </summary>
    void DrawLaser()
    {
        Vector3 direction = _startPointObject.transform.forward;
        Vector3 currentPosition = _startPointObject.transform.position;

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, currentPosition);

        while (true)
        {
            if (_puzzleEnd) break;
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, direction, out hit, maxDistance))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

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
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, currentPosition + direction * maxDistance);
                break;
            }
        }
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction de fin d'�nigme ---------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void EndLaserEnigme()
    {
        _puzzleEnd = true;
        foreach (GameObject mirrorObject in _mirror)
        {
            MirrorRotation mirror = mirrorObject.GetComponent<MirrorRotation>();
            if (mirror != null)
            {
                mirror.FreezMirror();
            }
            else
            {
                Debug.LogWarning("Un objet de la liste _mirror n'a pas de script MirrorRotation !");
            }
        }
    }
}
