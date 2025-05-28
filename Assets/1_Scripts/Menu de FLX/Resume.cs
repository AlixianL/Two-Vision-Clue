using UnityEngine;
using UnityEngine.SceneManagement;

public class Resume : MonoBehaviour,IActivatable
{
    [SerializeField] private Pause _unpause;
    public void Activate()
    {
        _unpause.TogglePauseMenu();
    }
}
