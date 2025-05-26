using UnityEngine;

public class ButtonOnOff : MonoBehaviour, IActivatable
{
    [SerializeField] private SimonsManager simonsManager;

    [SerializeField] private Color _isOn;//-----------------------------------------> Led on
    [SerializeField] private Color _isOff;//----------------------------------------> Led off
    [SerializeField] private MeshRenderer _verifLight;//----------------------------> Led affichage


    private bool _goatIsOn = false;//----------------------------------------------> Condition Si le lazer est actif
    void Start()
    {
        _verifLight.material.color = _isOff;
    }

    public void Activate()
    {
        
        if (!_goatIsOn)
        {
            _verifLight.material.color = _isOn;
            _goatIsOn = true;
        }
        else if (_goatIsOn)
        {
            _verifLight.material.color = _isOff;
            _goatIsOn = false;
        }

        simonsManager.On();
    }
}
