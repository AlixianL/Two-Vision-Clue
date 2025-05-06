using UnityEngine;
using System.Collections;
using Rewired.Demos;

public class Test : MonoBehaviour
{
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] float resetDelay = .25f;

    void OnMouseDown()
    {
        PressButton();
    }

    public void PressButton()
    {
        GetComponent<MeshRenderer>().material.color = highlightColor;
        Invoke("ResetButton", resetDelay);
    }

    void ResetButton()
    {
        GetComponent<MeshRenderer>().material.color =defaultColor;
    }
}
