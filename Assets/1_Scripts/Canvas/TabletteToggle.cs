using System.Collections;
using UnityEngine;

public class TabletteToggle : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    [SerializeField] private CanvasGroup canvasGroupToFade; // L'élément à faire apparaître en fondu
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private bool _infoIsOn = false;
    
    private Coroutine fadeCoroutine;

    public void Activate()
    {   
        if (_infoIsOn) FadeOut();
        else FadeIn();
    }

    private void FadeIn()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroupToFade, 1f, fadeDuration));
    }

    private void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(canvasGroupToFade, 0f, fadeDuration));
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
