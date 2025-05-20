using UnityEngine;

public class TriggerSceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    
    void Start()
    {
        StartCoroutine(SceneLoader.Instance.LoadScene(sceneToLoad));
    }
}
