using System.Collections;
using UnityEngine;


/// <summary>
/// Gère Les rayon de lumière, de leur activation a leur condition de reussite. 
/// Avec l'utilisation des rebon de miroire.
/// </summary>
public class LaserBeam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject startPointObject;
    public GameObject player;

    private bool _isPlayerInRange = false;
    public KeyCode activationKey = KeyCode.E;


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
        if (_isPlayerInRange && Input.GetKeyDown(activationKey))
        {
            StartCoroutine(DrawLaser());
        }
    }

    IEnumerator DrawLaser()
    {
        Vector3 direction = startPointObject.transform.forward;
        Vector3 currentPosition = startPointObject.transform.position;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, currentPosition);

        // Boucle infinie tant que le rayon touche un miroir
        while (true)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, direction, out hit))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                if (hit.collider.CompareTag("Mirror"))
                {

                    direction = Vector3.Reflect(direction, hit.normal);
                    currentPosition = hit.point;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
            yield return null;
        }
    }
}
