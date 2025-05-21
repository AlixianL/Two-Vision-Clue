using UnityEngine;
using static PlaqueManager;

public class Button : MonoBehaviour, IActivatable
{
    [SerializeField] private PlaqueManager _plaqueManager;

    [SerializeField] private bool _forSwapping;


    [Header("Positions à swap (enum index)")]
    [SerializeField] private int _pos1;
    [SerializeField] private int _pos2;

    public void Activate()
    {

        if (_forSwapping)
        {
            PositionID id1 = (PositionID)_pos1;
            PositionID id2 = (PositionID)_pos2;

            _plaqueManager.Swap(id1, id2);
        }
        else
        {
            _plaqueManager.Turnup();
        }
        
    }
}