using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SimonsManager : MonoBehaviour
{
    [SerializeField] Button[] button;
    [Header("Color Order")]
    [SerializeField] List<int> colorOrder;
    [SerializeField] float PickDelay = .4f;
    [SerializeField] int pickNumber = 0;

    private void Start()
    {
        SetButtonIndex();
        StartCoroutine("PlayGame");
    }

    void SetButtonIndex()
    {
        for (int cnt = 0; cnt < button.Length; cnt++)
            button[cnt].ButtonIndex = cnt;
    }

    IEnumerator PlayGame()
    {

        pickNumber = 0;
        yield return new WaitForSeconds(PickDelay);

        foreach(int colorIndex in colorOrder)
        {
            button[colorIndex].PressButton();
            yield return new WaitForSeconds(PickDelay);
        }

        RandomColorOrder();


      //  for (int cnt = 0; cnt < 5; cnt++)
        //{
          //  yield return new WaitForSeconds(PickDelay);
            //RandomColorOrder();
       // }
    }


    void RandomColorOrder()
    {
        int rnd = Random.Range(0, button.Length);
        button[rnd].PressButton();
        colorOrder.Add(rnd);
    }

    public void PlayersPick(int pick)
    {
        Debug.Log("Ouaissss" + pick);

        if (pick == colorOrder[pickNumber])
        {
            Debug.Log("Correct");
            pickNumber++;
            if(pickNumber == colorOrder.Count)
            {

                StartCoroutine(PlayGame());
            }
        }
        else
        {
            Debug.Log("Fail");
        }




     }

}
