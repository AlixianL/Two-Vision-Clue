/*using UnityEngine;
using System.Collections;

public class SmoothProximityActivator : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroupToFade; // L'élément à faire apparaître en fondu
    [SerializeField] private string targetTag = "Player";   // Tag du joueur
    [SerializeField] private float fadeDuration = 0.5f;     // Durée du fondu

    private Coroutine fadeCoroutine;

    private void Start()
    {
        if (canvasGroupToFade != null)
        {
            canvasGroupToFade.alpha = 0f;
            canvasGroupToFade.interactable = false;
            canvasGroupToFade.blocksRaycasts = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroupToFade, 1f, fadeDuration));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroupToFade, 0f, fadeDuration));
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float targetAlpha, float duration)
    {
        float startAlpha = cg.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        cg.alpha = targetAlpha;
        cg.interactable = (targetAlpha > 0.9f);
        cg.blocksRaycasts = (targetAlpha > 0.9f);
    }
}
*/