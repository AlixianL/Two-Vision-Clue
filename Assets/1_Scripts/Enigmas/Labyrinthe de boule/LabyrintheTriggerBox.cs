using UnityEngine;

public class LabyrintheTriggerBox : MonoBehaviour, ISaveAndPullData
{
    [Header("References"), Space(5)]
    [SerializeField] private RotateWeel _rotateWeel;
    [SerializeField] private GameObject _validationLight;//-------------------------> Light Sur le pilier central pour valid� l'�nigme
    [SerializeField] private GameObject _ball;

    void Start()
    {
        _validationLight.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "SM_Boule_001")
        {
            _rotateWeel.enigmaIsValidate = true;
            other.attachedRigidbody.freezeRotation = true;
            other.attachedRigidbody.useGravity = false;
            _validationLight.SetActive(true);
            Debug.Log("Labyrinthe Fini");

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
