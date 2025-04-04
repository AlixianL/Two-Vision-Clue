using UnityEngine;

[System.Serializable]
public class GumGum : MonoBehaviour
{
    public string NPCName;
    [TextArea(3,10)] public string[] NPCDilogue;
}