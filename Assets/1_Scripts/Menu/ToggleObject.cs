using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject objectToToggle;

    // Méthode publique pour activer ou désactiver l'objet
    public void Toggle()
    {
        if (objectToToggle != null)
        {
            // Inverse l'état actuel de l'objet
            objectToToggle.SetActive(!objectToToggle.activeSelf);
        }
    }
}
