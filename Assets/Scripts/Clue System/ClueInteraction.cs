using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ClueInteraction : MonoBehaviour
{
    [Header("Player References"), Space(5)]
    [SerializeField] private GameObject _showPoint; 
    [SerializeField] private GameObject currentClue => GumGumManager.Instance.clueinstance;

    /// <summary>
    ///
    /// une méthode pour GumGum qui affiche a l'écran du joueur l'indice donné
    ///
    /// une/plusieurs méthode/s pour que le joueur puisse intéragire avec une pile 
    ///     - intéragir avec une pile fait mettre a l'écran tout les indices posséder par le joueur
    ///     - l'environnement devient flou
    /// 
    /// </summary>

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- PARTIE GUMGUM --------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void ShowClue()
    {
        
    }
    
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- PARTIE JOUEUR -+------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void InteractWhisClues()
    {
        Debug.Log("Clue Interaction");
    }
}
