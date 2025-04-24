using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Transform _rayOrigine;
    [SerializeField] private LayerMask _layerMask;
    
    [Header("Generic Variables"), Space(5)]
    [SerializeField] private int _rayRange = 5;
    
    [Header("Layer Index"), Space(5)]
    [SerializeField] private int _indexLayerEnvironement;
    [SerializeField] private int _indexLayerGumGum;
    [SerializeField] private int _indexLayerChewingGum;

    [Header("System"), Space(5)]
    private RaycastHit _hit;
    
    void Update()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward), out _hit, _rayRange, _layerMask))
        {
            Debug.DrawRay(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward) * _hit.distance, Color.magenta);
            
            // Si le raycast detecte l'environement
            if (_hit.collider.gameObject.layer == _indexLayerEnvironement && PlayerBrain.Instance.player.GetButtonDown("Interact"))
            {
                Debug.DrawRay(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward) * _hit.distance, Color.yellow);
            }
            
            // Si le raycast detecte GumGum
            else if (_hit.collider.gameObject.layer == _indexLayerGumGum && PlayerBrain.Instance.player.GetButtonDown("Interact"))
            {
                Debug.DrawRay(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward) * _hit.distance, Color.red);
                GumGumManager.Instance.ActivateGumGum();
            }

            // Si le raycast detecte un chewing gum
            else if (_hit.collider.gameObject.layer == _indexLayerChewingGum && PlayerBrain.Instance.player.GetButtonDown("Interact"))
            {
                Debug.DrawRay(_rayOrigine.position, _rayOrigine.TransformDirection(Vector3.forward) * _hit.distance, Color.blue);
                ChewingGum currentChewingGum = _hit.collider.gameObject.GetComponent<ChewingGum>();
                currentChewingGum.TakeChewingGum();
            }
        }
    }
}
