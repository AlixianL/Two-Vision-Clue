using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GumUIManager : MonoBehaviour
{
    [Header("References UI")]
    public TMP_Text gumText;
    public CanvasGroup gumCanvasGroup;
    
    [Header("Paramètres d'affichage")]
    public float fadeInDuration = 0.3f;
    public float fadeOutDuration = 0.5f;
    public float displayDuration = 1.5f;

    private Coroutine currentAnimation;

    void Awake()
    {
        // Initialisation en transparent
        if (gumCanvasGroup != null)
        {
            gumCanvasGroup.alpha = 0;
            gumCanvasGroup.interactable = false;
            gumCanvasGroup.blocksRaycasts = false;
        }
    }

    public void ShowGumCount(int count)
    {
        // Mise à jour du texte
        if (gumText != null)
            gumText.text = $"{count}x";

        // Démarrage de l'animation
        if (gumCanvasGroup != null)
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);
            
            currentAnimation = StartCoroutine(FadeAnimation());
        }
    }

    private IEnumerator FadeAnimation()
    {
        // Fade In
        yield return FadeTo(1f, fadeInDuration);
        
        // Temps d'affichage
        yield return new WaitForSeconds(displayDuration);
        
        // Fade Out
        yield return FadeTo(0f, fadeOutDuration);
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = gumCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            gumCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gumCanvasGroup.alpha = targetAlpha;
        gumCanvasGroup.interactable = (targetAlpha > 0.5f);
        gumCanvasGroup.blocksRaycasts = (targetAlpha > 0.5f);
    }
}