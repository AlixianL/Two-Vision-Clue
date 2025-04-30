using UnityEngine;
using UnityEngine.Events;

public class Keys : MonoBehaviour
{
    [SerializeField] private Keypad _keypad; 
    public UnityEvent clickOnKey;

    public void OnMouseDown()
    {
        _keypad.feedBack.text = "";
        clickOnKey.Invoke();
    }

    public void WriteNumber(int number)
    {
        _keypad.feedBack.text = _keypad.feedBack.text + number.ToString();
    }

    public void ClearText()
    {
        _keypad.Clear();
        _keypad.feedBack.text = _keypad._defaultText;
    }
    
    public void ValidateText()
    {
        _keypad.Validate();
    }
}
