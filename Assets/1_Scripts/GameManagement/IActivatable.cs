using UnityEngine;

/// <summary>
/// Une interface définit un "contrat" : toute classe qui l'implémente s'engage à contenir la ou les méthodes définies ici.
///
/// Cela permet d’écrire du code générique, réutilisable et plus facile à maintenir.
///
/// Exemple : tout objet interactif dans ton jeu peut "s’activer", peu importe son type.
/// </summary>
public interface IActivatable
{
    void Activate(); // Méthode obligatoire à implémenter
}