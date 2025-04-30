using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimonsManager : MonoBehaviour
{
    [SerializeField] Button[] button;
    [Header("Color Order")]
    [SerializeField] List<int> colorOrder;

    private void Start()
    {
        PlayGame();


    }

    void PlayGame()
    {
        for (int cnt = 0; cnt < button.Length; cnt++)
        {
            Debug.Log(button[cnt]);
            RandomColorOrder();
        }
    }
    

    void RandomColorOrder()
    {
        int rnd = Random.Range(0, button.Length);
        button[rnd].PressButton();
        colorOrder.Add(rnd);
    }
}
