using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class Keypad : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    public TMP_Text feedBack;
    [SerializeField] private List<BoxCollider> _keyBoxColliders = new List<BoxCollider>();
    [SerializeField] private Color _defaultMaterialColor;
    [SerializeField] private Color _validateMaterialColor;
    [SerializeField] private Color _falseMaterialColor;
    [SerializeField] private MeshRenderer _indicatorLight;
    [SerializeField] private CinemachineCamera _digicodeCinemachineCamera;
    [SerializeField] private CinemachineCamera _doorCinemachineCamera;
    [SerializeField] private Transform _playerTransform;

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
        _isInteractingWhisEnigma = !_isInteractingWhisEnigma;
        
        ChangePositionCinemachine.Instance.SwitchCam(_digicodeCinemachineCamera, _isInteractingWhisEnigma);
        GameManager.Instance.ToggleTotalFreezePlayer();

        //Vector3 direction = new Vector3(gameObject.transform.position.x, PlayerBrain.Instance.playerGameObject.transform.position.y, gameObject.transform.position.z + 2f);
        //PlayerBrain.Instance.playerGameObject.transform.position = new Vector3(_playerTransform.position.x, PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.y, _playerTransform.position.z);
        //PlayerBrain.Instance.playerGameObject.transform.rotation = Quaternion.Euler(0, _digicodeCinemachineCamera.transform.eulerAngles.y, 0);
        //PlayerBrain.Instance.cinemachineTargetGameObject.transform.LookAt(direction);
        
        
        if (!_isValidated && _isClear) Reset();
        
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 colliderSize = new(collider.size.x, 1, 1);

        if (colliderSize.x == 1.5f) colliderSize.x = 1;
        else if (colliderSize.x == 1) colliderSize.x = 1.5f;

        collider.size = colliderSize; 
        
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        
        if (Cursor.visible == false) Cursor.visible = true;
        else Cursor.visible = false;
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
            
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        
            if (Cursor.visible == false) Cursor.visible = true;
            else Cursor.visible = false;
            _isInteractingWhisEnigma = !_isInteractingWhisEnigma;

            
            ChangePositionCinemachine.Instance.SwitchIntoDoorCinemachineCamera(ChangePositionCinemachine.Instance._digicodeCinemachineCamera, ChangePositionCinemachine.Instance._doorCinemachineCamera);
            
            doors.Interact();
            
            GameManager.Instance.ToggleTotalFreezePlayer();
        }
        else
        {
            _indicatorLight.material.color = _falseMaterialColor;
            StartCoroutine(Delay(1f));
        }
    }

    public void Reset()
    {
        feedBack.text = "_ _ _ _";
        _isClear = false;
    }

    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        Reset();
        _indicatorLight.material.color = _defaultMaterialColor;
    }
}
