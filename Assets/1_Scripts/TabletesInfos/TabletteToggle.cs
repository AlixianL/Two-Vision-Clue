using System;
using System.Collections;
using UnityEngine;

public class TabletteToggle : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    [SerializeField] private CanvasGroup _interactCanvasGroup;
    [SerializeField] private CanvasGroup _informationsCanvasGroup; // L'élément à faire apparaître en fondu
    private Coroutine _informationsFadeCoroutine;
    private Coroutine _interactFadeCoroutine;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private bool _infoIsOn;

    
    void Start()
    {
        FadeOut();
    }

    public void Activate()
    {   
        if (_infoIsOn) FadeOut();
        else FadeIn();
    }
    
    void FadeIn()
    {
        if (_informationsFadeCoroutine != null) StopCoroutine(_informationsFadeCoroutine);
        _informationsFadeCoroutine = StartCoroutine(FadeCanvasGroup(_informationsCanvasGroup, 1f, fadeDuration));
        _infoIsOn = true;
        
        if (_interactFadeCoroutine != null) StopCoroutine(_interactFadeCoroutine);
        _interactFadeCoroutine = StartCoroutine(FadeCanvasGroup(_interactCanvasGroup, 0f, fadeDuration));
    }

    void FadeOut()
    {
        if (_informationsFadeCoroutine != null) StopCoroutine(_informationsFadeCoroutine);
        _informationsFadeCoroutine = StartCoroutine(FadeCanvasGroup(_informationsCanvasGroup, 0f, fadeDuration));
        _infoIsOn = false;
        
        if (_interactFadeCoroutine != null) StopCoroutine(_interactFadeCoroutine);
        _interactFadeCoroutine = StartCoroutine(FadeCanvasGroup(_interactCanvasGroup, 1f, fadeDuration));
    }
    
    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = (targetAlpha > 0.9f);
        canvasGroup.blocksRaycasts = (targetAlpha > 0.9f);
    }
}
