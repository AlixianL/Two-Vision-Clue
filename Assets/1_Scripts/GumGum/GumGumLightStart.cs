using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumGumLightStart : MonoBehaviour
{
    [Header("Lights Settings")]
    [SerializeField] private List<Light> gumGumLights = new List<Light>();
    [SerializeField] private float _tempsEntreLight = 0.5f;
    [SerializeField] private bool _mainUiIsOn;

    [SerializeField] private Pause _pause;



    private bool lightsActivated = false;

    public TriggerSound triggerSound;

    private void Start()
    {
        _mainUiIsOn = true;
        foreach (Light light in gumGumLights)
        {
            if (light != null)
                light.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !lightsActivated)
        {
            lightsActivated = true;
            StartCoroutine(ActivateLightsSequentially());
            // Sera dans un autre script dans le futur mais ça faisais des bugs sinon
            PlayerBrain.Instance.playerInteractionSystem.playerCanInteractWhithMouse = true;
        }

        if (other.CompareTag("Player"))
        {
            _mainUiIsOn = !_mainUiIsOn;
            _pause.SwitchUi(_mainUiIsOn);
        }
        
    }

    private IEnumerator ActivateLightsSequentially()
    {
        foreach (Light light in gumGumLights)
        {
            //triggerSound.PlaySound(); 
            if (light != null)
            {
                light.enabled = true;
                light.gameObject.GetComponent<TriggerSound>().JouerOneShot();
            }

            

            yield return new WaitForSeconds(_tempsEntreLight);
        }
    }
}
