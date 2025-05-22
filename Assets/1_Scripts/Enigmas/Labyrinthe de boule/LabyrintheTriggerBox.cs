using UnityEngine;

public class LabyrintheTriggerBox : MonoBehaviour, ISaveAndPullData
{
    [Header("References"), Space(5)]
    [SerializeField] private RotateWeel _rotateWeel;
    [SerializeField] private GameObject _ball;


    [Header("End Feedback")]
    [SerializeField] private GameObject _validationLight;//-------------------------> Light Sur le pilier central pour valide l'enigme
    [SerializeField] private UnlockFInal _unlock;
    [SerializeField] private GameObject _number;

    void Start()
    {
        _validationLight.SetActive(false);
        _number.SetActive(false);

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "SM_Boule_001")
        {
            _rotateWeel.enigmaIsValidate = true;
            other.attachedRigidbody.freezeRotation = true;
            other.attachedRigidbody.useGravity = false;
            _validationLight.SetActive(true);
            _number.SetActive(true);
            _unlock._labiryntheIsEnd = true;

            PushDataToSave();
        }
    }

    public void PushDataToSave()
    {
        SaveData.Instance.gameData.enigmaIsComplete_labyrinthe = true;
        SaveData.Instance.gameData.labyrintheValidateLightIsActive = true;
        SaveData.Instance.gameData.labyrintheRotation = _rotateWeel.labyrinth.transform.eulerAngles;
        SaveData.Instance.gameData.ballPosition = _ball.transform.position;
    }
    
    public void PullDataFromSave()
    {
        _rotateWeel.enigmaIsValidate = SaveData.Instance.gameData.enigmaIsComplete_labyrinthe;
        _validationLight.SetActive(SaveData.Instance.gameData.labyrintheValidateLightIsActive);
        _rotateWeel.labyrinth.transform.eulerAngles = SaveData.Instance.gameData.labyrintheRotation;
        _ball.transform.position = new Vector3(SaveData.Instance.gameData.ballPosition.x, SaveData.Instance.gameData.ballPosition.y, SaveData.Instance.gameData.ballPosition.z);
    }
}
