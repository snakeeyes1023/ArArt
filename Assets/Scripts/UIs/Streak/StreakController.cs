using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère l'affichage de les suites de coups réussis consécutifs
/// </summary>
public class StreakController : MonoBehaviour
{
    [SerializeField]
    private StreakBadge[] streakBadges;

    [SerializeField]
    private GameController gameController;

    void Start()
    {
        GameController_OnStreakChanged(this, gameController.LongestStreak);
        gameController.OnStreakChanged += GameController_OnStreakChanged;
    }

    /// <summary>
    /// Met à jour les badges afficher selon la suite de coups réussis consécutifs
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The e.</param>
    private void GameController_OnStreakChanged(object sender, int e)
    {
        int maxStreak = gameController.LongestStreak;
        
        foreach (var streakBadge in streakBadges)
        {
            streakBadge.VerifyVisibility(maxStreak);
        }
    }
}
