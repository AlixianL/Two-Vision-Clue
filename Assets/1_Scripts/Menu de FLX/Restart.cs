using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour,IActivatable
{
    public void Activate()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("je recharge la scène");
    }
}
