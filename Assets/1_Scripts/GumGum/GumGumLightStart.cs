using System.Collections.Generic;
using UnityEngine;

public class GumGumLightStart : MonoBehaviour
{
    [SerializeField] private Light LightOfGumgum;
    

    void Start()
    {
        LightOfGumgum.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LightOfGumgum.enabled = true;
            
        }
    }

}
