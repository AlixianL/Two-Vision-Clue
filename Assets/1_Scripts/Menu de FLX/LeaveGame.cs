using UnityEngine;

public class LeaveGame : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        Application.Quit();
        Debug.Log("cafermelejeux");
    }
}
