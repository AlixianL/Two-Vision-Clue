using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CluePosition : MonoBehaviour, IActivatable, ISaveAndPullData
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject cluePrefab;
    public Transform targetPosition;
    private Outline tempOutline => GetComponent<Outline>();
    [Space(5)]
    public List<GameObject> clues = new List<GameObject>();
    public CinemachineCamera clueCinemachineCamera;

    [Header("Settings"), Space(5)]
    public float distanceFromCenter;
    
    [Header("Variables"), Space(5)]
    public bool playerIsInteracting = false;
    [SerializeField] private bool _isForEnigma1;
    [SerializeField] private bool _isForEnigma2;
    [SerializeField] private bool _isForEnigma3;
    [SerializeField] private bool _isForEnigma4;
    

    void Start()
    {
        tempOutline.enabled = false;
    }
    
    /// <summary>
    /// Appelle cette méthode pour répartir les indices autour du centre
    /// </summary>
    public void UpdatePosition()
    {
        if (clues == null || clues.Count == 0)
            return;

        float angleStep = 360f / clues.Count;

        for (int i = 0; i < clues.Count; i++)
        {
            float angle = angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(
                Mathf.Cos(rad) * distanceFromCenter,
                0f,
                Mathf.Sin(rad) * distanceFromCenter
            );

            clues[i].transform.position = targetPosition.position + offset;
        }
    }
    
    public void Activate()
    {
        playerIsInteracting = !playerIsInteracting;
        ChangePositionCinemachine.Instance.SwitchCam(clueCinemachineCamera, playerIsInteracting);
        GameManager.Instance.ToggleTotalFreezePlayer();
        GameManager.Instance.clueUI.SetActive(!GameManager.Instance.clueUI.activeSelf);
        GameManager.Instance.playerMainRoom.SetActive(!GameManager.Instance.clueUI.activeSelf);
    }

    public void ActivateByGumGum()
    {
        GameManager.Instance.clueUI.SetActive(true);
        GameManager.Instance.playerMainRoom.SetActive(false);
    }
    
    public void PushDataToSave()
    {
        
    }
    public void PullDataFromSave()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 1 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma1.Count > 0 && _isForEnigma1)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma1)
            {
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition1);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 2 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma2.Count > 0 && _isForEnigma2)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma2)
            {
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition2);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 3 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma3.Count > 0 && _isForEnigma3)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma3)
            {
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition3);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 4 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma4.Count > 0 && _isForEnigma4)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma4)
            {
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition4);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
        }
    }
}