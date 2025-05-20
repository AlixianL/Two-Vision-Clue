using System.Reflection;
using Rewired;
using Unity.Cinemachine;
using UnityEngine;

public class MirrorRotation : MonoBehaviour, IActivatable, ISaveAndPullData
{
    [Header("R�f�rences")]
    public Transform pivotHorizontal;              // Pivot de rotation gauche/droite
    public Transform mirrorTilt;                   // Partie du miroir qui s'incline


    [Header("Param�tres")]
    [SerializeField] private int mirrorID;
    public float rotationSpeed = 50f;
    public float verticalMin = -45f;               // Limite minimum d'inclinaison
    public float verticalMax = 45f;                // Limite maximum d'inclinaison

    private bool _interactWithEnigma = false;
    [SerializeField] private bool _enigmaisend = false;

    private float verticalAngle = 0f;              // Stocke l'angle d'inclinaison


    [SerializeField] private CinemachineCamera _enigmaCinemachineCamera;


    public void Activate()
    {
        
        if (!_interactWithEnigma)
        {
            _interactWithEnigma = true;
        }
        else _interactWithEnigma = false;

        GameManager.Instance.ToggleTotalFreezePlayer();
        PlayerBrain.Instance.playerRigidbody.linearVelocity = Vector3.zero;

        if (_enigmaCinemachineCamera != null)
        {
            ChangePositionCinemachine.Instance.SwitchCam(_enigmaCinemachineCamera, _interactWithEnigma);
        }
        
        
        GameManager.Instance.playerUI.SetActive(!_interactWithEnigma);
        GameManager.Instance.mirrorUI.SetActive(_interactWithEnigma);
    }

    void Update()
    {
        if (_interactWithEnigma && !_enigmaisend)
        {
            if (PlayerBrain.Instance.player.GetButton("RightMovement"))
            {
                pivotHorizontal.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
            if (PlayerBrain.Instance.player.GetButton("LeftMovement"))
            {
                pivotHorizontal.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }

            if (PlayerBrain.Instance.player.GetButton("ForwardMovement"))
            {
                verticalAngle -= rotationSpeed * Time.deltaTime;
            }
            if (PlayerBrain.Instance.player.GetButton("BackwardMovement"))
            {
                verticalAngle += rotationSpeed * Time.deltaTime;
            }

            verticalAngle = Mathf.Clamp(verticalAngle, verticalMin, verticalMax);

            mirrorTilt.localEulerAngles = new Vector3(0f, 0f, verticalAngle);

            PushDataToSave();
        }
    }

    public void FreezMirror()
    {
        _enigmaisend = true;
        Debug.Log("Enigme Miroir fini");
    }
    
    public void SetMirrorRotation(int mirrorID, string axis, Vector3 rotation)
    {
        string fieldName = $"rotationMirror{axis.ToUpper()}_{mirrorID:D2}";
        var gameData = SaveData.Instance.gameData;
        var type = gameData.GetType();

        FieldInfo field = type.GetField(fieldName);
        if (field != null && field.FieldType == typeof(Vector3))
        {
            field.SetValue(gameData, rotation);
        }
    }
    
    public Vector3 GetMirrorRotation(int mirrorID, string axis)
    {
        string fieldName = $"rotationMirror{axis.ToUpper()}_{mirrorID:D2}";
        var gameData = SaveData.Instance.gameData;
        var type = gameData.GetType();

        FieldInfo field = type.GetField(fieldName);
        if (field != null && field.FieldType == typeof(Vector3))
        {
            Vector3 rotation = (Vector3)field.GetValue(gameData);
            return rotation;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void PushDataToSave()
    {
        SetMirrorRotation(mirrorID, "Y", pivotHorizontal.localEulerAngles);
        SetMirrorRotation(mirrorID, "X", mirrorTilt.localEulerAngles);
    }
    
    public void PullDataFromSave()
    {
        Vector3 savedTiltRotation = GetMirrorRotation(mirrorID, "X");
        Vector3 savedHorizontalRotation = GetMirrorRotation(mirrorID, "Y");

        // Appliquer les rotations sauvegardées
        mirrorTilt.localEulerAngles = savedTiltRotation;
        pivotHorizontal.localEulerAngles = savedHorizontalRotation;

        // Mettre à jour l'angle vertical pour qu'il corresponde à la rotation actuelle
        verticalAngle = savedTiltRotation.z;
    }

}
