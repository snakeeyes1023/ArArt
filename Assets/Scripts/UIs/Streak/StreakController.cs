using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// G�re l'affichage de les suites de coups r�ussis cons�cutifs
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
    /// Met � jour les badges afficher selon la suite de coups r�ussis cons�cutifs
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
