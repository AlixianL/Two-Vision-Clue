using UnityEngine;

public class ChewingGum : MonoBehaviour
{
    public void TakeChewingGum()
    {
        PlayerBrain.Instance.chewingGumCount++;
        Destroy(gameObject);
    }
}
