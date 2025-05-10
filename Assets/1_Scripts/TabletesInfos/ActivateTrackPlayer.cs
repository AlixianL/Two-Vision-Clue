using System.Collections;
using UnityEngine;

public class ActivateTrackPlayer : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private CanvasGroup _targetCanva;
    private Coroutine _informationsFadeCoroutine;
    private Coroutine _interactFadeCoroutine;

    [Header("Variables"), Space(5)]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private bool _playerInRange = false;
    
    void Start()
    {
        _targetCanva.alpha = 0f;
        
        if (_playerInRange) FadeInTracker();
        else FadeOutTracker();
    }

    void FadeInTracker()
    {
        if (_informationsFadeCoroutine != null) StopCoroutine(_informationsFadeCoroutine);
        _informationsFadeCoroutine = StartCoroutine(FadeCanvasGroup(_targetCanva, 1f, fadeDuration));
    }

    void FadeOutTracker()
    {
        if (_informationsFadeCoroutine != null) StopCoroutine(_informationsFadeCoroutine);
        _informationsFadeCoroutine = StartCoroutine(FadeCanvasGroup(_targetCanva, 0f, fadeDuration));
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            FadeInTracker();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            FadeOutTracker();
        }
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
