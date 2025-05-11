using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Keys : MonoBehaviour, IActivatable
{
    [SerializeField] private Keypad _keypad; 
    public UnityEvent clickOnKey;

    

    public void OnMouseDown()
    {
        if (_keypad._isInteractingWhisEnigma)
        {
            clickOnKey.Invoke();
           
        }
    }

    public void Activate()
    {
        _keypad.Activate();
    }
    
    public void WriteNumber(int number)
    {
        if (!_keypad._isClear)
        {
            ClearText();
        }

        if (_keypad.feedBack.text.Length < _keypad._password.ToString().Length)
        {
            _keypad.feedBack.text = _keypad.feedBack.text + number.ToString();
            
            if (_keypad.feedBack.text.Length == _keypad._password.ToString().Length) _keypad.Validate();
        }
        else _keypad.Validate();
    }

    public void ClearText()
    {
        _keypad.Clear();
    }
    
    public void ValidateText()
    {
        _keypad.Validate();
    }
}
