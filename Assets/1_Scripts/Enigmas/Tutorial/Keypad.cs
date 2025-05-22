using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class Keypad : MonoBehaviour, IActivatable, ISaveAndPullData
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
    [SerializeField] private GameObject _raycastOrigineGameObject;
    private RaycastOrigine _raycastOrigine;
    public Doors doors;
    
    [Header("Variables"), Space(5)]
    public int _password;
    [Space(5)]
    public string _defaultText;
    [Space(5)]
    public bool _isInteractingWhisEnigma = false;
    public bool _isClear = true;
    private bool _isValidated = false;

    /// <summary>
    /// 
    /// </summary>
    

    void Start()
    {
        _indicatorLight.material.color = _defaultMaterialColor;
        feedBack.text = _defaultText;
        _raycastOrigine = _raycastOrigineGameObject.GetComponent<RaycastOrigine>();
    }

    
    public void Activate()
    {
        _isInteractingWhisEnigma = !_isInteractingWhisEnigma;

        PlayerBrain.Instance.raycastOrigine.canTrackTarget = !PlayerBrain.Instance.raycastOrigine.canTrackTarget;
        
        ChangePositionCinemachine.Instance.SwitchCam(_digicodeCinemachineCamera, _isInteractingWhisEnigma);
        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;
        
        GameManager.Instance.playerUI.SetActive(!_isInteractingWhisEnigma);
        GameManager.Instance.digicodeUI.SetActive(_isInteractingWhisEnigma);
        
        _raycastOrigine.canTrackTarget = !_isInteractingWhisEnigma;
        
        if (_isInteractingWhisEnigma) PlaceRaycastOrigineForDigicode();
        else PlaceRaycastOrigineToPlayerCamera();
        
        PlayerBrain.Instance.playerInteractionSystem.playerCanInteractWhithMouse = !_isInteractingWhisEnigma;
        
        if (!_isValidated && _isClear) Reset();
        
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 colliderSize = new(collider.size.x, 1, 1);

        if (colliderSize.x == 1.5f) colliderSize.x = 0.25f;
        else if (colliderSize.x == 0.25f) colliderSize.x = 1.5f;

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
        if (feedBack.text == _password.ToString() || _isValidated)
        {
            _indicatorLight.material.color = _validateMaterialColor;
            
            foreach (BoxCollider boxColliders in _keyBoxColliders)
            {
                boxColliders.enabled = false;
                boxColliders.gameObject.layer = 0;
            }
            
            _isValidated = true;
            Debug.Log("Tuto fini");
            if (Cursor.lockState == CursorLockMode.Locked) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;
        
            if (Cursor.visible == false) Cursor.visible = true;
            else Cursor.visible = false;
            
            _isInteractingWhisEnigma = !_isInteractingWhisEnigma;

            GameManager.Instance.playerUI.SetActive(!_isInteractingWhisEnigma);
            GameManager.Instance.digicodeUI.SetActive(false);
            
            ChangePositionCinemachine.Instance.SwitchIntoDoorCinemachineCamera(ChangePositionCinemachine.Instance._digicodeCinemachineCamera, ChangePositionCinemachine.Instance._doorCinemachineCamera);
            
            doors.Interact();
            
            GameManager.Instance.ToggleTotalFreezePlayer();
            PlayerBrain.Instance.raycastOrigine.canTrackTarget = !PlayerBrain.Instance.raycastOrigine.canTrackTarget;
            PushDataToSave();

        }
        else
        {
            _indicatorLight.material.color = _falseMaterialColor;
            StartCoroutine(Delay(1f));
        }
    }
    
    public void PushDataToSave()
    {
        SaveData.Instance.gameData.enigmaIsComplete_digicode = true;
        SaveData.Instance.gameData.codeText = feedBack.text;
        SaveData.Instance.gameData.doorsAreOpen = true;
    }

    public void PullDataFromSave()
    {
        _isValidated = SaveData.Instance.gameData.enigmaIsComplete_digicode;
        feedBack.text = SaveData.Instance.gameData.codeText;
        if (SaveData.Instance.gameData.doorsAreOpen)
        {
            doors.Interact();
            doors._isOpen = SaveData.Instance.gameData.doorsAreOpen;
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

    void PlaceRaycastOrigineForDigicode()
    {
        _raycastOrigineGameObject.transform.position = new Vector3(PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.x, 
            PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.y - 0.08f, PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.z);
    }
    
    void PlaceRaycastOrigineToPlayerCamera()
    {
        _raycastOrigineGameObject.transform.position = new Vector3(PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.x, 
            PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.y, PlayerBrain.Instance.cinemachineTargetGameObject.transform.position.z);
    }
    
    
}
