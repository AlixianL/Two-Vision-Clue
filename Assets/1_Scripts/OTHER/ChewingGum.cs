using UnityEngine;

public class ChewingGum : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        TakeChewingGum();
    }
    public void TakeChewingGum()
    {
        PlayerBrain.Instance.chewingGumCount++;
        Destroy(gameObject);
    }
}
