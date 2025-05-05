using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class Keypad : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    public TMP_Text feedBack;
    [SerializeField] private List<BoxCollider> _keyBoxColliders = new List<BoxCollider>();
    [SerializeField] private Color _defaultMaterialColor;
    [SerializeField] private Color _validateMaterialColor;
    [SerializeField] private Color _falseMaterialColor;
    [SerializeField] private MeshRenderer _indicatorLight;
    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;

    public Doors doors;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private int _password;
    [Space(5)]
    public string _defaultText;
    [Space(5)]
    public bool _isInteractingWhisEnigma = false;
    public bool _isClear = true;
    private bool _isValidated = false;

    void Start()
    {
        _indicatorLight.material.color = _defaultMaterialColor;
        feedBack.text = _defaultText;
    }

    
    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();

        if (!_isValidated) Reset();
        BoxCollider collider = GetComponent<BoxCollider>();

        Vector3 colliderSize = new(collider.size.x, 1, 1);

        if (colliderSize.x == 1.5f) colliderSize.x = 1;
        else if (colliderSize.x == 1) colliderSize.x = 1.5f;

        collider.size = colliderSize; 
        
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        
        if (Cursor.visible == false) Cursor.visible = true;
        else Cursor.visible = false;
        _isInteractingWhisEnigma = !_isInteractingWhisEnigma;
        
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _isInteractingWhisEnigma);
    }
    
    public void Clear()
    {
        feedBack.text = "";
        _isClear = true;
    }

    public void Validate()
    {
        if (feedBack.text == _password.ToString())
        {
            _indicatorLight.material.color = _validateMaterialColor;
            
            foreach (BoxCollider boxColliders in _keyBoxColliders)
            {
                boxColliders.enabled = false;
                boxColliders.gameObject.layer = 0;
            }
            
            _isValidated = true;
            doors.Interact();
            
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        
            if (Cursor.visible == false) Cursor.visible = true;
            else Cursor.visible = false;
            _isInteractingWhisEnigma = !_isInteractingWhisEnigma;
            
            ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _isInteractingWhisEnigma);
            GameManager.Instance.ToggleTotalFreezePlayer();
        }
        else
        {
            _indicatorLight.material.color = _falseMaterialColor;
            StartCoroutine(Delay());
        }
    }

    public void Reset()
    {
        feedBack.text = "_ _ _ _";
        _isClear = false;
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        Reset();
        _indicatorLight.material.color = _defaultMaterialColor;
    }
}
