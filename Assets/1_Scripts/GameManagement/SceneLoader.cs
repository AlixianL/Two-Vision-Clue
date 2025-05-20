using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    
    public string sceneToLoad;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void StartLoading()
    {
        StartCoroutine(LoadScene(sceneToLoad));
    }
    
    public IEnumerator LoadScene(string nameOfSceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nameOfSceneToLoad);

        while (!asyncLoad.isDone)
        {
            // Afficher un loader ou une barre de progression
            yield return null;
        }
    }
}
