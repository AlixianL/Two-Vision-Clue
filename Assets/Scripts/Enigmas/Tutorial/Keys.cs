using UnityEngine;
using UnityEngine.Events;

public class Keys : MonoBehaviour
{
    [SerializeField] private Keypad _keypad; 
    public UnityEvent clickOnKey;

    public void OnMouseDown()
    {
        clickOnKey.Invoke();
        Debug.Log(gameObject.name + " clicked");
    }

    public void WriteNumber(int number)
    {
        _keypad.feedBack.text = _keypad.feedBack.text + number.ToString();
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
