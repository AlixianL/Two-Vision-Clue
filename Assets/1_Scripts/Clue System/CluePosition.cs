using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CluePosition : MonoBehaviour, IActivatable, ISaveAndPullData
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject cluePrefab;
    [SerializeField] private Transform _targetPosition;
    private Outline tempOutline => GetComponent<Outline>();
    [Space(5)]
    public List<GameObject> clues = new List<GameObject>();
    public CinemachineCamera clueCinemachineCamera;

    [Header("Settings"), Space(5)]
    public float distanceFromCenter;
    
    [Header("Variables"), Space(5)]
    public bool playerIsInteracting = false;
    [SerializeField] private bool isForEnigma1;
    [SerializeField] private bool isForEnigma2;
    [SerializeField] private bool isForEnigma3;
    [SerializeField] private bool isForEnigma4;
    

    void Start()
    {
        tempOutline.enabled = false;
        //StartCoroutine(CheckIfCluesIsEmpty());
    }
    /*
    IEnumerator CheckIfCluesIsEmpty()
    {
        while (clues.Count == 0)
        {
            yield return new WaitForEndOfFrame();
            if (clues.Count != 0)
            {
                tempOutline.enabled = true;
            }
        }
    }
    */
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

            clues[i].transform.position = _targetPosition.position + offset;
        }
    }
    
    public void Activate()
    {
        playerIsInteracting = !playerIsInteracting;
        ChangePositionCinemachine.Instance.SwitchCam(clueCinemachineCamera, playerIsInteracting);
        GameManager.Instance.ToggleTotalFreezePlayer();
        GameManager.Instance.clueUI.SetActive(!GameManager.Instance.clueUI.activeSelf);
    }
    
    public void PushDataToSave()
    {
        
    }
    public void PullDataFromSave()
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 1 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma1.Count > 0 && isForEnigma1)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma1)
            {
                Debug.Log("Instanciation d'un indice : " + clue.name);
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition1);
                temp.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
            Debug.Log("Tout les indices de l'énigme 1 ont été recharger");
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 2 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma2.Count > 0 && isForEnigma2)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma2)
            {
                Debug.Log("Instanciation d'un indice : " + clue.name);
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition2);
                temp.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
            Debug.Log("Tout les indices de l'énigme 2 ont été recharger");
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 3 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma3.Count > 0 && isForEnigma3)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma3)
            {
                Debug.Log("Instanciation d'un indice : " + clue.name);
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition3);
                temp.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
            Debug.Log("Tout les indices de l'énigme 3 ont été recharger");
        }
        
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- INDICES ENIGME 4 -----------------------------------------------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma4.Count > 0 && isForEnigma4)
        {
            foreach (var clue in SaveData.Instance.gameData.clueDatasAlreadyGivesForEnigma4)
            {
                Debug.Log("Instanciation d'un indice : " + clue.name);
                GameObject temp = Instantiate(cluePrefab, SaveData.Instance.gameData.cluePosition4);
                temp.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                Clue tempScript = temp.GetComponent<Clue>();
                tempScript._clueData = clue;
                tempScript.LoadInitialize(clue);
                clues.Add(temp);
            }
            UpdatePosition();
            Debug.Log("Tout les indices de l'énigme 4 ont été recharger");
        }
    }
}