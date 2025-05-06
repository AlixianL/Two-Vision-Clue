using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandheldCameraTriggerBox : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private LayerMask _layerMaskToIgnore;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private List<GameObject> _objectsDetected = new List<GameObject>();

    void Start()
    {
        CheckIfListIsNull();
    }

    /// <summary>
    /*
     other.gameObject.layer         => te donne l'index du layer (un int entre 0 et 31)
     (1 << other.gameObject.layer)  => crée un bitmask pour ce layer précis 
     & _layerMask                   => vérifie si ce layer est dans ton LayerMask sélectionné
    */
    /// </summary> 
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet appartient au LayerMask
        if (((1 << other.gameObject.layer) & _layerMaskToIgnore) == 0)
        {
            _objectsDetected.Add(other.gameObject);
        }
        CheckIfListIsNull();
    }

    
    private void OnTriggerExit(Collider other)
    {
        _objectsDetected.Remove(other.gameObject);
        CheckIfListIsNull();
    }

    private void Update()
    {
        CheckIfListIsNull();
    }

    private void CheckIfListIsNull()
    {
        if (_objectsDetected.Count == 0)
        {
            HandheldCameraManager.Instance.cameraCanBeInstalled = true;
        }
        else
        {
            HandheldCameraManager.Instance.cameraCanBeInstalled = false;
        }
    }
}
