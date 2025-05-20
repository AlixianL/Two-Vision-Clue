using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pauseMenu = new List<GameObject>();
    [SerializeField] private List<GameObject> _otherElements = new List<GameObject>();
    public Transform neutralParent;
    private bool isPaused = false;

    void Start()
    {
        foreach (GameObject Panel in _pauseMenu)
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
            foreach (GameObject panel in _pauseMenu)
            {
                panel.SetActive(true);
                panel.transform.SetParent(neutralParent);
            }

            foreach (GameObject panel in _otherElements)
            {
                panel.transform.SetParent(neutralParent);
            }
            Time.timeScale = 0f;
        }
        else
        {
            foreach (var panel in _pauseMenu)
            {
                panel.SetActive(false);
                panel.transform.SetParent(transform);
            }

            foreach (GameObject panel in _otherElements)
            {
                panel.transform.SetParent(transform);
            }
            Time.timeScale = 1f;
        }
    }
}
