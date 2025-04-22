using System;
using UnityEngine;

public class ClueInteraction : MonoBehaviour
{
    [Header("uiflbk"), Space(5)]
    [SerializeField] private GameObject clue;

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
    private void Start()
    {
        Debug.Log(clue.name);
    }
    
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- PARTIE JOUEUR -+------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
