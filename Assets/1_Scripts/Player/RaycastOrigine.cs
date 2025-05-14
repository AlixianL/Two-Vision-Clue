using UnityEngine;

public class RaycastOrigine : MonoBehaviour
{
    [SerializeField] private GameObject trackedTarget;
    public bool canTrackTarget;
    
    // Update is called once per frame
    void Update()
    {
        if (canTrackTarget)
        {
            transform.position = trackedTarget.transform.position;
            transform.rotation = trackedTarget.transform.rotation;
        }
    }
}
