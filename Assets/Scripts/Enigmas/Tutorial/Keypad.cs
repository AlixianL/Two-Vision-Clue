using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour, IActivatable
{
    [Header("References"), Space(5)]
    public TMP_Text feedBack;
    [SerializeField] private List<BoxCollider> _keyBoxColliders = new List<BoxCollider>();
    [SerializeField] private Color _defaultMaterialColor;
    [SerializeField] private Color _validateMaterialColor;
    [SerializeField] private MeshRenderer _indicatorLight;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private int _password;

    void Start()
    {
        _indicatorLight.material.color = _defaultMaterialColor;
    }

    public void Activate()
    {
        GameManager.Instance.ToggleTotalFreezePlayer();
        
        Collider collider = GetComponent<Collider>();

        collider.enabled = !collider.enabled; 
        
        if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        
        if (Cursor.visible == false) Cursor.visible = true;
        else Cursor.visible = false;
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
