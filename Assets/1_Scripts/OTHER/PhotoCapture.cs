using System.Collections;
using UnityEngine;

public class PhotoCapture : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Camera photoCamera; // La caméra qui capture
    [SerializeField] private bool hasCaptured = false;

    public void Start()
    {
        if (hasCaptured) return;

        StartCoroutine(CaptureOnce());
    }

    private IEnumerator CaptureOnce()
    {
        photoCamera.enabled = true;
        yield return new WaitForEndOfFrame(); // attendre la fin de frame pour rendre

        photoCamera.Render(); // capture manuelle
        photoCamera.enabled = false;

        hasCaptured = true;
    }
}