using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private List<GameObject> PauseMenu = new List<GameObject>();
    public Transform neutralParent;
    private bool isPaused = false;

    void Start()
    {
        foreach (GameObject Panel in PauseMenu)
        {
            Panel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        GameManager.Instance.ToggleMovementFreezePlayer();
        isPaused = !isPaused;
        if (isPaused)
        {
            foreach (GameObject panel in PauseMenu)
            {
                panel.SetActive(true);
                panel.transform.SetParent(neutralParent);
            }
            Time.timeScale = 0f;
        }
        else
        {
            foreach (var panel in PauseMenu)
            {
                panel.SetActive(false);
                panel.transform.SetParent(transform);
            }
            Time.timeScale = 1f;
        }
        
        

    }
}
