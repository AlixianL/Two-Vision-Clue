using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;

    Button currentInteractable;

    private void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null )
        {
            currentInteractable.Interact();
            
        }

        
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Button")
            {
                Button newInteractable = hit.collider.GetComponent<Button>();
                

                if (newInteractable.enabled)

                {

                    SetNewcurrentInteractable(newInteractable);
                    
                }
                else
                {
                    DisableCurrentInteractable();
                    
                }
            }
            else
            {
                DisableCurrentInteractable();
                
            }
        }
        else
        {
            DisableCurrentInteractable();
           
        }
    }

    void SetNewcurrentInteractable(Button newInteractable)
    {

        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        
        
    }

    void DisableCurrentInteractable()
    {

        currentInteractable?.DisableOutline();
        currentInteractable=null;
        
    }
}