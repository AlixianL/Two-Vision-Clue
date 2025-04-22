using UnityEngine;

public class LaserReflection : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int maxReflections = 5;      // Nombre maximum de rebonds
    public float maxDistance = 100f;    // Distance max d’un rayon

    void Update()
    {
        DrawLaser();
    }

    void DrawLaser()
    {
        Vector3 direction = transform.forward;
        Vector3 startPosition = transform.position;

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPosition);

        int reflections = 0;

        while (reflections < maxReflections)
        {
            Ray ray = new Ray(startPosition, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Ajouter le point d’impact
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                // Si on touche un miroir, on réfléchit
                if (hit.collider.CompareTag("Mirror"))
                {
                    // Nouvelle direction = direction réfléchie
                    direction = Vector3.Reflect(direction, hit.normal);
                    startPosition = hit.point;
                    reflections++;
                }
                else
                {
                    // Si ce n’est pas un miroir, on s’arrête là
                    break;
                }
            }
            else
            {
                // Si rien n’est touché, on trace jusqu’à la distance max
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, startPosition + direction * maxDistance);
                break;
            }
        }
    }
}
