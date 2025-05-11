using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Changement_Scene : MonoBehaviour
{
    public UnityEngine.UI.Button bouton;   
    public string sceneName;
    public Image blackFade;  
    public float fadeDuration = 2f;
    public AnimationCurve fadeCurve; 

    void Start()
    {
        bouton.onClick.AddListener(() => StartCoroutine(FadeAndLoad()));
    }

    IEnumerator FadeAndLoad()
    {
        float t = 0f;
        Color color = blackFade.color;

        // Fondu au noir en utilisant la courbe d'animation
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            
            float alpha = fadeCurve.Evaluate(t / fadeDuration); 
            blackFade.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Attendre un peu avant de changer de scène
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }
}
