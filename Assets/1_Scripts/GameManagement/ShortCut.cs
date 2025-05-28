using UnityEngine;

public class ShortCut : MonoBehaviour
{
    [SerializeField] private TurnPillar _turnPillar;
    [SerializeField] private SimonsManager _simonsManager;
    [SerializeField] private LabyrintheTriggerBox _labyrinthe;
    [SerializeField] private LaserBeam _laserBeam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            _turnPillar.EndEnigme();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _simonsManager.EndEnigme();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            _labyrinthe.EndEnigma();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            _laserBeam.EndLaserEnigme();
        }
    }
}
