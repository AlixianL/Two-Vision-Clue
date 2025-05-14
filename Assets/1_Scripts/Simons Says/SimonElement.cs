using UnityEngine;
using System.Collections;

public class SimonElement : MonoBehaviour, IActivatable
{
    public int Index; // Défini dans l'inspecteur pour chaque cube
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

    // Méthode pour activer un cube par le joueur (par appui sur E par exemple)
    public void Activate()
    {
        simonsManager.PlayerActivated(this);
        Preview(); // Montre que le joueur a interagi
    }

    // Afficher la couleur de l'élément (quand c'est le tour du joueur)
    public void Preview()
    {
        StopAllCoroutines(); // Arrête toute animation en cours
        StartCoroutine(FlashColor()); // Lance l'effet de surbrillance
    }

    private IEnumerator FlashColor()
    {
        meshRenderer.material.color = highlightColor; // Surbrillance
        yield return new WaitForSeconds(flashDuration);
        meshRenderer.material.color = defaultColor; // Réinitialise la couleur à la normale
    }

    // Réinitialiser la couleur à la couleur par défaut
    public void ResetColor()
    {
        meshRenderer.material.color = defaultColor;
    }
}
