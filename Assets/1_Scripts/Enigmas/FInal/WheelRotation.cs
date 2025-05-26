using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    [SerializeField] private List<Transform> wheels;//----> liste des roue a tourné

    public float stepAngle = -36f;//-----------------------> angle de rotation d'1 pas
    public float rotationDuration = 0.5f;//---------------> temps de rotation d'1 pas

    private bool isRotating = false;//--------------------> boolen pour verifier si la rotation est en cour
    private bool _enigmeisend = false;//------------------> booleen qui verifie si l'enigme est fini


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction appelé par les bouton ---------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void RotateWheels(int[] steps)
    {
        if (!isRotating && !_enigmeisend)
            StartCoroutine(RotateAllWheelsSmooth(steps));
    }


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- logique de rotation de l'enigme --------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private IEnumerator RotateAllWheelsSmooth(int[] steps)
    {
        isRotating = true;

        List<Quaternion> startRotations = new();
        List<Quaternion> endRotations = new();

        for (int i = 0; i < wheels.Count; i++)
        {
            startRotations.Add(wheels[i].rotation);
            float angle = stepAngle * steps[i];
            endRotations.Add(startRotations[i] * Quaternion.Euler(0, angle, 0)); // ou X/Z selon axe
        }

        float animationTime = 0f;

        while (animationTime < rotationDuration)
        {
            float t = animationTime / rotationDuration;
            for (int i = 0; i < wheels.Count; i++)
            {
                wheels[i].rotation = Quaternion.Slerp(startRotations[i], endRotations[i], t);
            }

            animationTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < wheels.Count; i++)
        {
            wheels[i].rotation = endRotations[i];
        }

        isRotating = false;
    }
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- fin de l'enigme  -----------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void EndEnigme()
    {
        _enigmeisend = true;
        Debug.Log("FINI fini");
    }
}
