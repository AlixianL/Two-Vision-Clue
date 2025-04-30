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
    [SerializeField] private MeshRenderer _indicatorLight;
    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private int _password;
    [Space(5)]
    public string _defaultText;
    [Space(5)]
    private bool _isInteractingWhisEnigma = false;

    void Start()
    {
        _indicatorLight.material.color = _defaultMaterialColor;
        feedBack.text = _defaultText;
    }

    
    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();
        
        //Collider collider = GetComponent<Collider>();
        //collider.enabled = !collider.enabled;
        
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        
        if (Cursor.visible == false) Cursor.visible = true;
        else Cursor.visible = false;
        _isInteractingWhisEnigma = !_isInteractingWhisEnigma;
        
        ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, !_isInteractingWhisEnigma);
    }

    private IEnumerator Delay(float seconds)
    {
        Debug.Log("Delay");
        yield return new WaitForSeconds(seconds);
    }
    
    public void Clear()
    {
        feedBack.text = "";
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
        }
    }
}
