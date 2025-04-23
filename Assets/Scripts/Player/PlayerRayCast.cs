using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Transform _rayOrigine;
    [SerializeField] private LayerMask _layerMask;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private int _rayRange = 5;

    [Header("System"), Space(5)]
    private RaycastHit _hit;
    void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward), out _hit, _rayRange, _layerMask))
        {
            Debug.DrawRay(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward) * _hit.distance, Color.yellow);
            
            // Si le raycast detecte GumGum
            if (_hit.collider.gameObject.layer == 7 && PlayerBrain.Instance.player.GetButtonDown("Interact"))
            {
                Debug.DrawRay(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward) * _hit.distance, Color.red);
                GumGumManager.Instance.ActivateGumGum();
            }

            // Si le raycast detecte un chewing gum
            else if (_hit.collider.gameObject.layer == 6 && PlayerBrain.Instance.player.GetButtonDown("Interact"))
            {
                Debug.DrawRay(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward) * _hit.distance, Color.blue);
                ChewingGum currentChewingGum = _hit.collider.gameObject.GetComponent<ChewingGum>();
                currentChewingGum.TakeChewingGum();
            }
        }
    }
}
