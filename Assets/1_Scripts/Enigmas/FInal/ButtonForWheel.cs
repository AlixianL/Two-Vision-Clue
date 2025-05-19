using UnityEngine;

public class ButtonForWheel : MonoBehaviour,IActivatable
{

    [SerializeField] private WheelRotation _wheelRotation;

    [Header("Ref")]

    [SerializeField] private int _firstWheel;
    [SerializeField] private int _secondWheel;
    [SerializeField] private int _thirdWheel;
    [SerializeField] private int _fourthWheel;



    public void Activate()
    {
        _wheelRotation.RotateWheels(new int[] { _firstWheel, _secondWheel, _thirdWheel, _fourthWheel });
    }
}
