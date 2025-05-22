using UnityEngine;
using UnityEngine.SceneManagement;

public class ENDGAME : MonoBehaviour
{
    [SerializeField] private string sceneForEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("oui");
            SceneManager.LoadScene(sceneForEnd);
        }
    }
}

