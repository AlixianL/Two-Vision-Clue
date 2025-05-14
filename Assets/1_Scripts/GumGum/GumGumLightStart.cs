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
        
        // Sera dans un autre script dans le futur mais ça faisais des bugs sinon
        PlayerBrain.Instance.playerInteractionSystem.playerCanInteractWhithMouse = true;
    }

}
