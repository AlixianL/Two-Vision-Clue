using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    public static OutlineManager Instance;

    private Interactable currentOutline;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void SetOutline(Interactable newTarget)
    {
        if (currentOutline == newTarget) return;

        if (currentOutline != null)
            currentOutline.DisableOutline();

        currentOutline = newTarget;

        if (currentOutline != null)
            currentOutline.EnableOutline();
    }

    public void ClearOutline()
    {
        if (currentOutline != null)
            currentOutline.DisableOutline();
        currentOutline = null;
    }
}