using UnityEngine;
using System.Collections;

public class SimonElement : MonoBehaviour, IActivatable
{
    public int Index; // D�fini dans l'inspecteur pour chaque cube
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private float flashDuration = 0.25f;
    [SerializeField] private SimonsManager simonsManager;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = defaultColor;
    }

    // M�thode pour activer un cube par le joueur (par appui sur E par exemple)
    public void Activate()
    {
        simonsManager.PlayerActivated(this);
        Preview(); // Montre que le joueur a interagi
    }

    // Afficher la couleur de l'�l�ment (quand c'est le tour du joueur)
    public void Preview()
    {
        StopAllCoroutines(); // Arr�te toute animation en cours
        StartCoroutine(FlashColor()); // Lance l'effet de surbrillance
    }

    private IEnumerator FlashColor()
    {
        meshRenderer.material.color = highlightColor; // Surbrillance
        yield return new WaitForSeconds(flashDuration);
        meshRenderer.material.color = defaultColor; // R�initialise la couleur � la normale
    }

    // R�initialiser la couleur � la couleur par d�faut
    public void ResetColor()
    {
        meshRenderer.material.color = defaultColor;
    }
}
